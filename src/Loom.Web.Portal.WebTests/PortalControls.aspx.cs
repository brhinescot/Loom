#region Using Directives

using System;
using System.Collections.Generic;
using AdventureWorks.Purchasing;
using Loom.Web.Portal.UI;
using Loom.Web.Portal.UI.Controls;

#endregion

namespace Loom.Web.Portal.WebTest
{
    public partial class PortalControls : PortalView
    {
        protected Repeater Repeater;

        protected override void OnLoad(EventArgs e)
        {
            BindRepeaterTest();

            propertyGrid.DataSource = typeof(TestModule);
            propertyGrid.DataBind();

            propertyValues.Text = propertyGrid.ToJsonString();

            ViewData.PropertyGrid = typeof(TestModule);
            ViewData.EnumList = typeof(ShipMethod);
            ViewData.Form = new {Name = "Brian", Date = DateTime.Today};
            ViewData.Name = "Brian";
            ViewData.DownloadData = new {Url = "http://dl.google.com/googletalk/googletalk-setup.exe", Message = "click this link to manually download the file", Icon = ""};

            base.OnLoad(e);
        }

        private void BindRepeaterTest()
        {
            List<RepeaterTester> dataSource = new List<RepeaterTester>
            {
                new RepeaterTester {Name = "Brian", Id = "542664"},
                new RepeaterTester {Name = "Charlie", Id = "903544"},
                new RepeaterTester {Name = "Jeremy", Id = "13724435"},
                new RepeaterTester {Name = "Brian", Id = "542664"},
                new RepeaterTester {Name = "Charlie", Id = "903544"},
                new RepeaterTester {Name = "Jeremy", Id = "13724435"},
                new RepeaterTester {Name = "Brian", Id = "542664"},
                new RepeaterTester {Name = "Charlie", Id = "903544"},
                new RepeaterTester {Name = "Jeremy", Id = "13724435"},
                new RepeaterTester {Name = "Brian", Id = "542664"},
                new RepeaterTester {Name = "Charlie", Id = "903544"},
                new RepeaterTester {Name = "Jeremy", Id = "13724435"}
            };

            ViewData.RepeaterData = dataSource;
        }
    }

    public class RepeaterTester
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class TestModule : PortalModule
    {
        [ModuleProperty(Description = "Boolean property without a name.")]
        public bool ShowTitle { get; set; }

        [ModuleProperty(Name = "ShowTitle Named", DefaultValue = true, Description = "Named boolean property with a default of true.")]
        public bool ShowTitleNamed { get; set; }

        [ModuleProperty(PropertyEditorTypeName = "Loom.Web.Portal.UI.FileList", EditControlData = "{'Directory':'~/content/images'}", Description = "File list with JSON editor data.")]
        public string Images { get; set; }

        [ModuleProperty(Description = "Plain old string property with no extra attribute settings.")]
        public string Name { get; set; }

        [ModuleProperty(Description = "Plain old int property with no extra attribute settings.")]
        public string Number { get; set; }

        [ModuleProperty(DefaultValue = "Default Text", Description = "String property with default value.")]
        public string NameDefault { get; set; }

        [ModuleProperty(DefaultValue = 999, Description = "Int property with default value.")]
        public string NumberDefault { get; set; }
    }
}