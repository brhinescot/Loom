#region Using Directives

using System;

#endregion

namespace Loom.Web.UI.WebControls.CMS
{
    public class CallbackEventArgs : EventArgs
    {
        public CallbackEventArgs(string callbackMethod, string callbackData)
        {
            CallbackMethod = callbackMethod;
            CallbackData = callbackData;
        }

        public string CallbackData { get; }
        public bool CallbackFailed { get; set; }
        public string CallbackMethod { get; }
        public string FeedbackText { get; set; }
        public string HeaderText { get; set; }
        public string HelpText { get; set; }
    }
}