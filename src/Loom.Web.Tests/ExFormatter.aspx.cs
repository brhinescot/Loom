#region Using Directives

using System;
using System.Web.UI;
using Loom.Web.UI;

#endregion

namespace Loom.Web.Tests
{
    public partial class ExFormatter : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                DoSomethingDangerous();
            }
            catch (InvalidOperationException ex)
            {
                string message = "We are sorry but an unknown error has occured. Developers are being beat until it is fixed. Please try again later.";

                ErrorSummary.ShowError(Page, message);
                ErrorSummary.ShowError(Page, ex);
            }
        }

        private static void DoSomethingDangerous()
        {
            try
            {
                DoSomethingStupid(null);
            }
            catch (ArgumentNullException ex)
            {
                InvalidOperationException iopEx = new InvalidOperationException("An invalid Operation occured in your stupid code.", ex);
                iopEx.Data.Add("Incline", "90 Percent");
                iopEx.Data.Add("Overload Frequency", "532 Mhz");
                iopEx.Data.Add("Operational Status", "Cooldown");
                throw iopEx;
            }
        }

        private static void DoSomethingStupid(string test)
        {
            if (test == null)
                throw new ArgumentNullException("test");
        }
    }
}