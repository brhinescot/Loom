#region Using Directives

using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:CalendarEx runat=server></{0}:CalendarEx>")]
    public class CalendarEx : Calendar, ILocalizable
    {
        #region Designer generated code

        /// <summary>
        ///     This object is required for custom localization and anti spam functionality.
        /// </summary>
        private readonly ControlExtender<CalendarEx> extender;

        #endregion

        public CalendarEx()
        {
            extender = new ControlExtender<CalendarEx>(this);
        }

        [Category("Behavior")]
        [Bindable(true)]
        [Description("The minimum selectable date. Earlier dates will be disabled.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(DateTime), "1/1/0001")]
        public DateTime MinDate
        {
            get
            {
                object dateObj = ViewState["minDate"];
                if (dateObj != null)
                    return (DateTime) dateObj;
                return DateTime.MinValue.Date;
            }
            set => ViewState["minDate"] = value.Date;
        }

        [Category("Behavior")]
        [Bindable(true)]
        [Description("The maximum selectable date. Later dates will be disabled.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DateTime MaxDate
        {
            get
            {
                object dateObj = ViewState["maxDate"];
                if (dateObj != null)
                    return (DateTime) dateObj;
                return DateTime.MinValue.Date;
            }
            set => ViewState["maxDate"] = value.Date;
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

        protected override void OnDayRender(TableCell cell, CalendarDay day)
        {
            // TODO: Should not set to not selectable if date are not set.
            if (day.Date < MinDate || day.Date > MaxDate)
                day.IsSelectable = false;

            base.OnDayRender(cell, day);
        }
    }
}