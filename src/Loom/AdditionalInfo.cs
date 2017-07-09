#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Principal;

#endregion

namespace Loom
{
    public class AdditionalInfo : IEnumerable<string>
    {
        private readonly List<string> lines = new List<string>();

        #region IEnumerable<string> Members

        public IEnumerator<string> GetEnumerator()
        {
            return lines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return lines.GetEnumerator();
        }

        #endregion

        public void AddInfo(string name, string value)
        {
            if (Compare.IsNullOrEmpty(name))
                return;

            AddInfoPrivate(name, value);
        }

        public void AddHeader(string name)
        {
            if (Compare.IsNullOrEmpty(name))
                return;

            AddHeaderPrivate(name);
        }

        public void AddInfo(string name, NameValueCollection values)
        {
            if (values == null || values.Count == 0)
                return;

            AddInfoPrivate(values, name);
        }

        public void AddInfo(IPrincipal principal)
        {
            if (principal == null)
                return;

            AddInfoPrivate(principal);
        }

        public void AddInfo(string name, Uri uri)
        {
            if (uri == null)
                return;

            AddInfoPrivate(name, uri.ToString());
        }

        public void AddInfo(string name, bool value)
        {
            AddInfoPrivate(name, value.ToString());
        }

        public void AddInfo(string name, int value)
        {
            AddInfoPrivate(name, value.ToString(CultureInfo.InvariantCulture));
        }

        private void AddHeaderPrivate(string name)
        {
            lines.Add(Environment.NewLine + name);
            lines.Add(ExceptionFormatter.LineSeparator);
        }

        private void AddInfoPrivate(string name, string value)
        {
            lines.Add("--> " + name + ": " + value);
        }

        private void AddInfoPrivate(NameValueCollection values, string name)
        {
            AddInfo(name, string.Empty);
            for (int i = 0; i < values.Count; i++)
            {
                string key = values.AllKeys[i];
                if (key.StartsWith("__"))
                    continue;
                AddInfo("     " + key, values[i]);
            }
        }

        private void AddInfoPrivate(IPrincipal principal)
        {
            AddInfo("Authentication Type", principal.Identity.AuthenticationType);
            AddInfo("User Identity", principal.Identity.Name);
        }
    }
}