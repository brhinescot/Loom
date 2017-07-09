#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;

#endregion

namespace Loom.Web.Tests
{
    public partial class Controls : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<RepeaterTestItem> repeaterTestItems = new List<RepeaterTestItem>();
            repeaterTestItems.Add(new RepeaterTestItem {Name = "Item01", Description = "Description01"});
            repeaterTestItems.Add(new RepeaterTestItem {Name = "Item02", Description = "Description02"});
            repeaterTestItems.Add(new RepeaterTestItem {Name = "Item03", Description = "Description03"});
            repeaterTestItems.Add(new RepeaterTestItem {Name = "Item04", Description = "Description04"});
            repeaterTestItems.Add(new RepeaterTestItem {Name = "Item05", Description = "Description05"});
            repeaterTestItems.Add(new RepeaterTestItem {Name = "Item06", Description = "Description06"});
            repeaterTestItems.Add(new RepeaterTestItem {Name = "Item07", Description = "Description07"});
            repeaterTestItems.Add(new RepeaterTestItem {Name = "Item08", Description = "Description08"});
            repeaterTestItems.Add(new RepeaterTestItem {Name = "Item09", Description = "Description09"});
            repeaterTestItems.Add(new RepeaterTestItem {Name = "Item10", Description = "Description10"});
            repeaterTestItems.Add(new RepeaterTestItem {Name = "Item11", Description = "Description11"});
            repeaterTestItems.Add(new RepeaterTestItem {Name = "Item12", Description = "Description12"});

            RepeaterEx1.DataSource = repeaterTestItems;
            RepeaterEx1.DataBind();

            RepeaterTestItem item = new RepeaterTestItem {Name = "Item01", Description = "Description01"};
            item.Things.Add("Thing 1");
            item.Things.Add("Thing 2");
            item.Things.Add("Thing 3");
            item.Things.Add("Thing 4");

            cmsEntityView1.DataSource = item;
            cmsEntityView1.DataBind();

            entityView1.DataSource = item;
            entityView1.DataBind();
        }

        protected void ButtonEx1_Click(object sender, EventArgs e)
        {
            Response.Write("1 Clicked");
        }

        protected void ButtonEx2_Click(object sender, EventArgs e)
        {
            Response.Write("2 Clicked");
        }
    }

    public class RepeaterTestItem
    {
        public RepeaterTestItem()
        {
            Things = new Collection<string>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public Collection<string> Things { get; }
    }
}