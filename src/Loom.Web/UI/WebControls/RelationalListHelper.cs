#region Using Directives

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    ///     Summary description for RelationalListHelper.
    /// </summary>
    internal class RelationalListHelper
    {
        private readonly ListControl control;
        private readonly ListControl parentControl;

        internal RelationalListHelper(ListControl control)
        {
            this.control = control;
            IRelationalListControl relationalList = control as IRelationalListControl;
            if (null != relationalList)
                parentControl = relationalList.ParentListControl;
        }

        internal string InitializationScript
        {
            get
            {
                string script = string.Empty;
                if (null != parentControl)
                    script = string.Format("\r\n<script language='javascript' type='text/javascript'>\r\n"
                                           + "<!--\nwindow.onload=addCommandTo(window.onload,'document.forms[0][\""
                                           + "{0}\"].onchange();');\r\n//-->\r\n</script>", parentControl.ClientID);
                return script;
            }
        }

        internal Dictionary<string, NameValueCollection> CreateRelationships(DataRelation relation)
        {
            Argument.Assert.IsNotNull(relation, "relation");

            Dictionary<string, NameValueCollection> relationships = new Dictionary<string, NameValueCollection>();

            foreach (DataRow parentRow in relation.ParentTable.Rows)
            {
                string parentValue = parentRow[parentControl.DataValueField].ToString();

                NameValueCollection childItems =
                    GetChildCollectionFromParentRow(parentRow, relation);
                relationships.Add(parentValue, childItems);
            }

            return relationships;
        }

        internal DataRelation GetDataRelationFromDataSet(DataSet dataSet)
        {
            Argument.Assert.IsNotNull(dataSet, "dataSet");

            DataRelation returnRelation = null;
            foreach (DataRelation testRelation in dataSet.Relations)
            {
                if (!testRelation.ChildTable.Equals(dataSet.Tables[control.DataMember]))
                    continue;

                returnRelation = testRelation;
                break;
            }
            return returnRelation;
        }

        internal NameValueCollection GetChildCollectionFromParentRow(DataRow parentRow, DataRelation relation)
        {
            Argument.Assert.IsNotNull(parentRow, "parentRow");

            NameValueCollection returnItems = new NameValueCollection();

            foreach (DataRow childRow in parentRow.GetChildRows(relation))
                returnItems.Add(childRow[control.DataValueField].ToString(), childRow[control.DataTextField].ToString());

            return returnItems;
        }

        internal string GetRelationScript(Dictionary<string, NameValueCollection> relationships, bool keepFirstItem)
        {
            Argument.Assert.IsNotNull(relationships, "relationships");

            StringBuilder relationScript = new StringBuilder();
            relationScript.Append("\r\n<script language='javascript' type='text/javascript'>\r\n<!--\r\nif (!" + control.ClientID + "AssocArray) " +
                                  "var " + control.ClientID + "AssocArray = new Object();\r\n");

            foreach (string parentValue in relationships.Keys)
            {
                relationScript.Append(control.ClientID + "AssocArray['" +
                                      parentControl.ClientID + "=" +
                                      parentValue + "'] = new Array(");

                NameValueCollection childItems = relationships[parentValue];
                if (childItems.Count > 0)
                {
                    if (keepFirstItem)
                    {
                        relationScript.Append("\"");
                        relationScript.Append(control.Items[0].Value.Replace(@"\", @"\\").Replace(@"""", @"\"""));
                        relationScript.Append("\",\"");
                        relationScript.Append(control.Items[0].Text.Replace(@"\", @"\\").Replace(@"""", @"\"""));
                        relationScript.Append("\",");
                    }

                    for (int i = 0; i < childItems.Count - 1; i++)
                    {
                        relationScript.Append(GetChildScript(childItems, i, keepFirstItem));
                        relationScript.Append("\",");
                    }

                    relationScript.Append(GetChildScript(childItems, childItems.Count - 1, keepFirstItem));
                    relationScript.Append("\"");
                }
                relationScript.Append(");\r\n");
            }
            relationScript.Append("//-->\r\n</script>");
            return relationScript.ToString().Replace(",\");", ");");
        }

        private string GetChildScript(NameValueCollection childItems, int index, bool keepFirstItem)
        {
            string relationScript = string.Empty;
            if (keepFirstItem)
            {
                if (control.Items[0].Value != childItems.Keys[index])
                    relationScript = GetScriptFragment(childItems, index);
            }
            else
            {
                relationScript = GetScriptFragment(childItems, index);
            }

            return relationScript;
        }

        private static string GetScriptFragment(NameValueCollection childItems, int index)
        {
            StringBuilder scriptFragment = new StringBuilder();

            scriptFragment.Append("\"");
            scriptFragment.Append(childItems.Keys[index].Replace(@"\", @"\\").Replace(@"""", @"\"""));
            scriptFragment.Append("\",\"");
            scriptFragment.Append(childItems[index].Replace(@"\", @"\\").Replace(@"""", @"\"""));

            return scriptFragment.ToString();
        }
    }
}