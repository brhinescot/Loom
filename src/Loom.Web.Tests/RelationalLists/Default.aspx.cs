#region Using Directives

using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI;

#endregion

namespace Demo
{
    public partial class Default : Page
    {
        protected RelationalDataSet relationalDataSet1;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeComponent();
            if (!Page.IsPostBack)
            {
                relationalDataSet1.ReadXml(Server.MapPath("RelationalDataSet.xml"));
                ParentList.DataBind();
                ChildList.DataBind();
                GrandChildList1.DataBind();
                GrandChildList2.DataBind();
                Label7.Text = "<p>The only additional property that a developer \r\n\t\t\t\t\tneeds to set in a <strong>RelationalListControl</strong> is the \r\n\t\t\t\t\t<strong>ParentListID</strong> property.  The property is easily \r\n\t\t\t\t\tset with a dropdown that lists all compatible server controls on \r\n\t\t\t\t\tthe page that inherit from <strong>ListControl</strong>. \r\n\t\t\t\t\t</p><p>If you have assigned a <strong>DataSet</strong> as a data \r\n\t\t\t\t\tsource with data relations defined between multiple tables , \r\n\t\t\t\t\tthe control automatically finds the correct \r\n\t\t\t\t\t<strong>DataRelation</strong> and sets up the \r\n\t\t\t\t\trelationships between all the list controls.</p><p>You then have \r\n\t\t\t\t\tfully functional, client-side updating, linked list controls from \r\n\t\t\t\t\tsetting only one property.</p><p>Any ListControl server control can \r\n\t\t\t\t\tact as the top level parent, including a <strong>ListBox</strong> \r\n\t\t\t\t\tand a <strong>DropDownList</strong>. A \r\n\t\t\t\t\t<strong>RelationalListBox</strong> and a <strong>RelationalDropDownList</strong> \r\n\t\t\t\t\tcan of course also be \r\n\t\t\t\t\tused.  You can also mix and match RelationalListBoxes and \r\n\t\t\t\t\tRelationalDropDownLists in the same grouping.</p><p>The download includes\r\n\t\t\t\t\ta sample site using the same data as this demo and a compiled help file.<br><br></p>";
            }
        }

        private void InitializeComponent()
        {
            relationalDataSet1 = new RelationalDataSet();
            ((ISupportInitialize) relationalDataSet1).BeginInit();
            // 
            // relationalDataSet1
            // 
            relationalDataSet1.DataSetName = "RelationalDataSet";
            relationalDataSet1.EnforceConstraints = false;
            relationalDataSet1.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            ((ISupportInitialize) relationalDataSet1).EndInit();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = string.Format("Parent List = {0}<br>Child List = {1}<br>GrandChild1 List = {2}<br>GrandChild2 List = {3}", ParentList.SelectedItem.Text, ChildList.SelectedItem.Text, GrandChildList1.SelectedItem.Text, GrandChildList2.SelectedItem.Text);
        }
    }
}