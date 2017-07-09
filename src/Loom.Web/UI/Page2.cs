#region Using Directives

using System.Web.UI;

#endregion

namespace Loom.Web.UI
{
    public class Page2 : Page
    {
        protected void ShowError(string message)
        {
            ErrorSummary.ShowError(this, message);
        }
    }
}