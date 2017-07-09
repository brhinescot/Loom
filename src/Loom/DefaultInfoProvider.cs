#region Using Directives

using System;
using System.Globalization;
using System.Security.Principal;

#endregion

namespace Loom
{
    /// <summary>
    /// </summary>
    public class DefaultInfoProvider : IAdditionalInfoProvider
    {
        #region IAdditionalInfoProvider Members

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public AdditionalInfo Generate()
        {
            return GeneratePrivate();
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="additionalInfo"></param>
        protected virtual void OnGenerate(AdditionalInfo additionalInfo) { }

        private AdditionalInfo GeneratePrivate()
        {
            AdditionalInfo info = new AdditionalInfo();
            info.AddInfo("MachineName", Environment.MachineName);
            info.AddInfo("TimeStamp (UTC)", DateTime.UtcNow.ToString(CultureInfo.CurrentCulture));
            info.AddInfo("AppDomainName", AppDomain.CurrentDomain.FriendlyName);

            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            info.AddInfo("WindowsIdentity", identity.Name);
            info.AddInfo("OS Version", Environment.OSVersion.ToString());
            info.AddInfo("CLR Version", Environment.Version.ToString());
            info.AddInfo("WorkingSet", Environment.WorkingSet / 1024 + "K");

            OnGenerate(info);
            return info;
        }
    }
}