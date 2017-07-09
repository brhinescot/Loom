#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    ///     Represents a control that allows the user to select a single item from a drop-down list.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The control can be linked to a parent <see cref="ListControl" />. This enables the control
    ///         to show a subset of data based on the selection in the parent <b>ListControl</b>.  The
    ///         relationships between the lists are automatically built based on the <b>DataRelations</b>
    ///         in the <see cref="DataSet" /> identified by the control's
    ///         <see cref="BaseDataBoundControl.DataSource" /> property.  The control's
    ///         <b>DataSource</b> must be a <b>DataSet</b> with at least one <b>DataRelation</b>.
    ///     </para>
    ///     <para>
    ///         The control may be used like any other list control and will in fact, behave
    ///         like any other list control until the <see cref="RelationalDropDownList.ParentListId" /> property is set.
    ///         The <b>ParentListID</b> property is the only additional property that needs to be set in
    ///         order to have a client side updating, relational list control.
    ///     </para>
    /// </remarks>
    /// <example>
    ///     The following example demonstrates how to bind data to and select the parent list of the control
    ///     <code><![CDATA[
    ///  <%@ Page language="c#" %>
    ///  <%@ Register 
    /// 		TagPrefix="DevInterop" 
    /// 		Namespace="DevInterop.WebControls.UI" 
    /// 		Assembly="DevInterop.WebControls.UI.RelationalListControls" %>
    /// 		
    ///  <script runat="server"> 
    ///  protected RelationalDataSet relationalDataSet1;
    ///  potected void Page_Load(object sender, System.EventArgs e)
    ///  {
    /// 		if(!Page.IsPostBack)
    /// 		{
    /// 			this.relationalDataSet1.ReadXml(Server.MapPath("RelationalDataSet.xml"));
    /// 			this.ParentList.DataBind();
    /// 			this.ChildList.DataBind();
    /// 		}
    /// 	}
    ///  </script>
    ///  
    ///  <html><body><form runat="server"> 
    /// 		<DevInterop:RelationalDropDownList 
    /// 			id=ParentList 
    /// 			runat="server" 
    /// 			DataValueField="ID" 
    /// 			DataTextField="Name" 
    /// 			DataMember="Parent" 
    /// 			DataSource="<%# relationalDataSet1 %>" 
    /// 		</DevInterop:RelationalDropDownList>
    /// 		<DevInterop:RelationalDropDownList 
    /// 			id=ChildList 
    /// 			runat="server" 
    /// 			KeepFirstItem="True" 
    /// 			ParentListID="ParentList">
    /// 			DataValueField="ID" 
    /// 			DataTextField="Name" 
    /// 			DataMember="Child" 
    /// 			DataSource="<%# relationalDataSet1 %>" 
    /// 		</DevInterop:RelationalDropDownList>
    ///  </form></body></html>
    ///  ]]></code>
    /// </example>
    [ValidationProperty("SelectedItem")]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:RelationalDropDownList runat=server></{0}:RelationalDropDownList>")]
    [ToolboxBitmap(typeof(RelationalDropDownList), "Loom.Web.UI.WebControls.RelationalDropDownList.bmp")]
    public class RelationalDropDownList : DropDownList, IRelationalListControl, ILocalizable, ISpamGuardian
    {
        private const string ScriptArrayName = "ListRelations";

        #region Designer generated code

        /// <summary>
        ///     This object is required for custom localization and anti spam functionality.
        /// </summary>
        private readonly ControlExtender<RelationalDropDownList> extender;

        #endregion

        private ListControl parentListControl;
        private string parentListId;
        private RelationalListHelper relationalListHelper;
        private Dictionary<string, NameValueCollection> relationships;

        public RelationalDropDownList()
        {
            extender = new ControlExtender<RelationalDropDownList>(this);
        }

        /// <summary>
        ///     Gets or sets if the first item in the <see cref="RelationalDropDownList" /> will exist for all
        ///     selections of the controls parent <b>ListControl</b>.
        /// </summary>
        /// <remarks>
        ///     Set to <true /> to persist the first selection in the list across multiple selections of the parent.
        /// </remarks>
        [Description("Gets or sets if the first item will will exist for all selections of the controls parent list.")]
        [Category("Behavior")]
        [Bindable(true)]
        [DefaultValue(false)]
        public virtual bool KeepFirstItem
        {
            get
            {
                object savedState = ViewState["KeepFirstItem"];
                if (savedState != null)
                    return (bool) savedState;
                return false;
            }
            set => ViewState["KeepFirstItem"] = value;
        }

        private RelationalListHelper RelationalListHelper
        {
            get
            {
                if (null == relationalListHelper)
                    relationalListHelper = new RelationalListHelper(this);
                return relationalListHelper;
            }
        }

        #region ILocalizable Members

        [Bindable(true)]
        [Category("Misc")]
        [DefaultValue("")]
        public string ResourceKey
        {
            get
            {
                object o = ViewState["ResourceKey"];
                return o == null ? string.Empty : (string) o;
            }
            set => ViewState["ResourceKey"] = value;
        }

        #endregion

        #region IRelationalListControl Members

        /// <summary>
        ///     Gets a <b>boolean</b> value indicating if the control has a parent
        ///     <see cref="System.Web.UI.WebControls.ListControl" /> defined.
        /// </summary>
        /// <remarks>
        ///     <para>A top level <b>ListControl</b> will not have a parent defined.</para>
        ///     <runtimeonly />
        /// </remarks>
        [Browsable(false)]
        public bool HasParent => !Compare.IsNullOrEmpty(ParentListId);

        /// <summary>
        ///     Gets or sets the ID of the parent <see cref="System.Web.UI.WebControls.ListControl" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The control automatically sets up the relationship between it and the
        ///         parent <b>ListControl</b>.
        ///     </para>
        ///     <para>
        ///         A design time list of compatible controls is exposed as a list in the controls property
        ///         inspector.  The list is provided by the <see cref="ListControlConverter" /> class.
        ///     </para>
        /// </remarks>
        [Description("Gets or sets the ID of the parent control.")]
        [TypeConverter(typeof(ListControlConverter))]
        [Category("Behavior")]
        [Bindable(true)]
        [DefaultValue("")]
        public string ParentListId
        {
            get
            {
                if (null == parentListId)
                    parentListId = ViewState["ParentListID"] as string;
                return parentListId;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value", string.Format("Null Argument Exception: Null value for argument '{0}' passed into method '{1}'.", "value",
                        "DevInterop.Web.UI.WebControls.RelationalDropDownList.ParentListID"));

                parentListId = value.Trim();
                ViewState["ParentListID"] = parentListId;
            }
        }

        /// <summary>
        ///     Gets a referance to the controls parent <see cref="System.Web.UI.WebControls.ListControl" />.
        /// </summary>
        /// <remarks>
        ///     <runtimeonly />
        /// </remarks>
        [Browsable(false)]
        public ListControl ParentListControl
        {
            get
            {
                if (null == parentListControl)
                    ValidateParentListControl();
                return parentListControl;
            }
            set => parentListControl = value;
        }

        #endregion

        #region ISpamGuardian Members

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool AntiSpam
        {
            get
            {
                object o = ViewState["AntiSpam"];
                return o != null ? (bool) o : false;
            }
            set => ViewState["AntiSpam"] = value;
        }

        #endregion

        /// <summary>
        ///     Overrides <see cref="Control.CreateControlCollection" /> to disable adding child controls to this control.
        /// </summary>
        /// <remarks>
        ///     As implemented, returns an <b>EmptyControlCollection</b> to disable adding child controls to this control.
        /// </remarks>
        /// <returns>Returns an <see cref="EmptyControlCollection" />.</returns>
        protected override ControlCollection CreateControlCollection()
        {
            return new EmptyControlCollection(this);
        }

        /// <summary>
        ///     Overrides <see cref="Control.OnPreRender" /> to register the client script for the
        ///     <see cref="RelationalDropDownList">
        ///         RelationalDropDownList
        ///     </see>
        ///     .
        /// </summary>
        /// <remarks>
        ///     As implemented, this method registers the client script
        ///     by calling the <see cref="RegisterClientScript" /> method.
        /// </remarks>
        /// <param name="e">An <see cref="System.EventArgs" /> class.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //Need to set on every page load
            //
            RegisterClientScript();
        }

        /// <summary>
        ///     Overrides <see cref="Control.DataBind()" /> to create the relationship between the the
        ///     control identified by the <see cref="ParentListId" /> property and this control.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use this method to bind data from a source to a server control. This method
        ///         is commonly used after retrieving a data set through a database query.
        ///     </para>
        ///     <note>
        ///         When called on a server control, this method resolves all data-binding
        ///         expressions in the server control and in any of its child controls.
        ///     </note>
        /// </remarks>
        public override void DataBind()
        {
            OnDataBinding(EventArgs.Empty);

            if (HasParent & Enabled)
            {
                ValidateParentListControl();
                SetListRelationship(ValidateDataRelation());
                AddAttributesToParent();
            }

            if (!EnableViewState)
                SelectedValue = Page.Request.Form[UniqueID];
        }

        /// <summary>
        ///     Creates the parent/child relationship between the control identified by the <see cref="ParentListId" />
        ///     property and this control.
        /// </summary>
        /// <remarks>The </remarks>
        private void SetListRelationship(DataRelation relation)
        {
            relationships = RelationalListHelper.CreateRelationships(relation);
        }

        /// <summary>
        ///     Ensures that the <see cref="ParentListId">ParentListID</see>
        ///     is a valid <see cref="ListControl" />.
        /// </summary>
        /// <remarks>
        ///     The control specified by the <b>ParentListID</b> property must inherit
        ///     from the <see cref="ListControl" /> class.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        ///     A list control is not found with
        ///     the ID specified by the <see cref="ParentListId" /> property.
        /// </exception>
        protected virtual void ValidateParentListControl()
        {
            //Checked in case a client calls this directly
            //
            if (!(HasParent & Enabled))
                return;

            ListControl parentList = NamingContainer.FindControl(ParentListId) as ListControl;
            if (parentList != null)
                ParentListControl = parentList;
            else
                throw new InvalidOperationException("A ListControl for " + ID + "'s parent named '" + ParentListId + "' cannot be found.");
        }

        /// <summary>
        ///     Ensures the <see cref="BaseDataBoundControl.DataSource">DataSource</see> is valid and contains at least one
        ///     valid <see cref="DataRelation" />.
        /// </summary>
        /// <remarks>
        ///     A <b>DataRelation</b> is required to link the child list's items to the selections
        ///     in the parent list.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        ///     The datasource is not a <b>DataSet</b> or does not
        ///     contain at least one valid <see cref="DataRelation" />.
        /// </exception>
        /// <returns>A valid <b>DataRelation</b>.</returns>
        protected virtual DataRelation ValidateDataRelation()
        {
            //Checked in case a client calls this directly
            //
            if (HasParent & Enabled)
            {
                DataSet dataSource = DataSource as DataSet;
                if (null == dataSource)
                    throw new InvalidOperationException("The RelationalDropDownList requires a DataSet as its DataSource.");

                if (dataSource.Relations.Count == 0)
                    throw new InvalidOperationException("The datasource must contain at least one DataRelation.");

                DataRelation returnRelation = RelationalListHelper.GetDataRelationFromDataSet(dataSource);

                if (null == returnRelation)
                    throw new InvalidOperationException("A valid DataRelation was not found in the datasource");

                return returnRelation;
            }
            return null;
        }

        /// <summary>
        ///     Adds the onChange event to the List Control.
        /// </summary>
        /// <remarks>
        ///     If the control has a valid parent <b>ListControl</b>, this method adds the necessary script to
        ///     update the control when the selection in the parent <b>ListControl</b> changes.
        /// </remarks>
        protected virtual void AddAttributesToParent()
        {
            //Checked in case a client calls this directly
            //
            if (HasParent & Enabled)
                ParentListControl.Attributes["onchange"] += @"listItemSelected(this,this.form['" + ClientID + "']);";
        }

        /// <summary>
        ///     Registers all the client side script for the
        ///     <see cref="RelationalDropDownList">RelationalDropDownList</see>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method registers the client script to hold the control's values in an array,
        ///         to manage client side updating, and to initialize the control's values.
        ///     </para>
        ///     <para>
        ///         <b>Control Initialization Script</b>
        ///     </para>
        ///     <para>
        ///         The initialization script is only registered if the control's parent is not
        ///         an <see cref="IRelationalListControl" /> or the control's parent does not have a parent itself.
        ///         If one of these conditions exist, the control's parent control is the top level control and
        ///         the initialization script is registered for the parent control.
        ///     </para>
        ///     <para>
        ///         The initialization script causes an onChange event during page load on the top level control.
        ///         The event updates the top level control and cascades an update to all child controls.
        ///         The child controls then load their values based on the selection in their parent.
        ///     </para>
        ///     <para>
        ///         <b>Client Update Management Script</b>
        ///     </para>
        ///     <para>
        ///         The client update management script controls the client side updating of the control
        ///         and is emitted on each page by default.
        ///     </para>
        /// </remarks>
        protected virtual void RegisterClientScript()
        {
            if (!(HasParent & Enabled))
                return;

            RegisterResourceScript();
            RegisterParentRelationScript();
            RegisterInitializationScript();
        }

        /// <summary>
        ///     Registers the resource script for the <see cref="RelationalDropDownList">RelationalDropDownList</see> control.
        /// </summary>
        private void RegisterResourceScript()
        {
            Page.ClientScript.RegisterClientScriptResource(typeof(IRelationalListControl), WebResourcePath.RelationalListControls);
        }

        /// <summary>
        ///     Creates and registers the array script that holds the values for
        ///     the <see cref="RelationalDropDownList">RelationalDropDownList</see>
        ///     control.
        /// </summary>
        private void RegisterParentRelationScript()
        {
            string relationScript = RelationalListHelper.GetRelationScript(relationships, KeepFirstItem);
            Page.ClientScript.RegisterClientScriptBlock(typeof(IRelationalListControl), ScriptArrayName + UniqueID, relationScript);
        }

        /// <summary>
        ///     Creates and registers the script to initialize the top level control.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The script is only registered if the controls parent is not
        ///         an <see cref="IRelationalListControl" /> or the controls parent does not have a parent itself.
        ///         If one of these conditions exist, then the parent control is the top level control.
        ///     </para>
        ///     <para>
        ///         The script causes an update to be fired on the top level control, which will cascade an update
        ///         to all child controls.  This causes the child controls to load values based on the selection
        ///         in their parent.
        ///     </para>
        /// </remarks>
        private void RegisterInitializationScript()
        {
            IRelationalListControl parent = ParentListControl as IRelationalListControl;
            if (null != parent && parent.HasParent)
                return;

            if (Page.ClientScript.IsClientScriptBlockRegistered(typeof(IRelationalListControl), ParentListControl.ClientID + "onload"))
                return;

            string startupScript = RelationalListHelper.InitializationScript;
            Page.ClientScript.RegisterStartupScript(typeof(IRelationalListControl), ParentListControl.ClientID + "onload", startupScript);
        }

        /// <summary>
        ///     Overrides <see cref="Control.LoadViewState" />.
        /// </summary>
        /// <remarks>
        ///     Loads the ViewState for the relationship between this control and
        ///     the <see cref="ParentListId">ParentListID</see>.
        /// </remarks>
        /// <param name="savedState">The ViewState for this control.</param>
        protected override void LoadViewState(object savedState)
        {
            Pair combo = (Pair) savedState;
            base.LoadViewState(combo.First);
            if (HasParent)
                LoadRelationViewState(combo.Second);
        }

        /// <summary>
        ///     Loads the ViewState for the relationship between this control and
        ///     the <see cref="ParentListId">ParentListID</see>.
        /// </summary>
        /// <param name="savedState">
        ///     The stored ViewState for the relationship to
        ///     the <see cref="ParentListId">ParentListID</see>
        /// </param>
        private void LoadRelationViewState(object savedState)
        {
            Pair relations = savedState as Pair;
            if (relations == null)
                return;

            string[] parentValues = (string[]) relations.First;
            Pair[] childItems = (Pair[]) relations.Second;
            int itemCount = parentValues.Length;

            relationships = new Dictionary<string, NameValueCollection>(itemCount);
            for (int i = 0; i < parentValues.Length; i++)
            {
                string parentValue = parentValues[i];
                Pair childrenBag = childItems[i];
                NameValueCollection children = LoadItemsViewState(childrenBag);
                relationships.Add(parentValue, children);
            }
        }

        /// <summary>
        ///     Loads the ViewState for the child items.
        /// </summary>
        private static NameValueCollection LoadItemsViewState(Pair savedState)
        {
            string[] valueValues = (string[]) savedState.First;
            string[] textValues = (string[]) savedState.Second;
            int itemCount = valueValues.Length;

            NameValueCollection result = new NameValueCollection(itemCount);
            for (int i = 0; i < textValues.Length; i++)
                result.Add(valueValues[i], textValues[i]);

            return result;
        }

        /// <summary>
        ///     Overrides <see cref="Control.SaveViewState" />.
        /// </summary>
        /// <remarks>
        ///     View state is the accumulation of the values of a server control's
        ///     properties.
        /// </remarks>
        /// <returns>
        ///     Returns the server control's current view state. If there
        ///     is no view state associated with the control, this method returns
        ///     <null />.
        /// </returns>
        protected override object SaveViewState()
        {
            Pair pair = new Pair();
            pair.First = base.SaveViewState();
            if (HasParent && relationships != null)
                pair.Second = SaveRelationalViewState();

            return pair;
        }

        /// <summary>
        ///     Saves the ViewState for the relationship between this control and
        ///     the
        ///     <see cref="ParentListId">
        ///         ParentListID
        ///     </see>
        ///     .
        /// </summary>
        /// <returns>
        ///     The ViewState for the relationship to
        ///     the
        ///     <see cref="ParentListId">
        ///         ParentListID
        ///     </see>
        ///     .
        /// </returns>
        private Pair SaveRelationalViewState()
        {
            int itemCount = relationships.Count;
            string[] parentValues = new string[itemCount];
            Pair[] childItems = new Pair[itemCount];

            int index = 0;
            foreach (string parentKey in relationships.Keys)
            {
                parentValues[index] = parentKey;
                childItems[index] = SaveItemsViewState(parentKey);
                index++;
            }
            return new Pair(parentValues, childItems);
        }

        /// <summary>
        ///     Saves the ViewState for the child items.
        /// </summary>
        /// <param name="parentKey"></param>
        /// <returns></returns>
        private Pair SaveItemsViewState(string parentKey)
        {
            NameValueCollection children = relationships[parentKey];

            int itemCount = children.Count;
            string[] textValues = new string[checked((uint) itemCount)];
            string[] valueValues = new string[checked((uint) itemCount)];

            for (int i = 0; i < itemCount; i++)
            {
                textValues[i] = children[i];
                valueValues[i] = children.Keys[i];
            }
            return new Pair(valueValues, textValues);
        }
    }
}