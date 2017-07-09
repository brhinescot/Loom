#region Using Directives

using System;

#endregion

namespace Loom.Web.ServiceModel
{
    public class ApiFault
    {
        public ApiFault()
        {
            Message = string.Empty;
            HelpLink = string.Empty;
            Source = string.Empty;
        }

        public string Message { get; set; }
        public string HelpLink { get; set; }
        public string Source { get; set; }

        public static ApiFault FromException(Exception exception)
        {
            return new ApiFault {Message = exception.Message, HelpLink = exception.HelpLink, Source = exception.Source};
        }
    }
}