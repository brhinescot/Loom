#region Using Directives

using System;
using System.Globalization;
using System.Threading;
using System.Web;

#endregion

namespace Loom.Web.Localization
{
    public static class ClientLocalization
    {
        /// <summary>
        ///     Sets a user's Locale based on the browser's Locale setting. If no setting
        ///     is provided the default Locale is used.
        /// </summary>
        public static void SetUserLocale(this HttpContext context)
        {
            SetUserLocale(context, null);
        }

        /// <summary>
        ///     Sets a user's Locale based on the browser's Locale setting. If no setting
        ///     is provided the default Locale is used.
        /// </summary>
        public static void SetUserLocale(HttpContext context, bool fromCookie)
        {
            if (fromCookie)
            {
                HttpCookie cookie = context.Request.GetCookie("AspNet_Localization");
                if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                {
                    SetUserLocale(cookie.Value);
                    return;
                }
            }

            SetUserLocale(context, null);
        }

        /// <summary>
        ///     Sets a user's Locale based on the browser's Locale setting. If no setting
        ///     is provided the default Locale is used.
        /// </summary>
        public static void SetUserLocale(HttpContext context, string currencySymbol)
        {
            HttpRequest request = context.Request;
            if (request.UserLanguages == null)
                return;

            string language = request.UserLanguages[0];
            if (language == null)
                return;

            if (language.Length < 3)
                language = language + "-" + language.ToUpper();

            SetUserLocale(language, currencySymbol);
        }

        /// <summary>
        ///     Sets a user's Locale based on the browser's Locale setting. If no setting
        ///     is provided the default Locale is used.
        /// </summary>
        public static void SetUserLocale(string language)
        {
            SetUserLocale(language, null);
        }

        public static void SetUserLocale(string language, string currencySymbol)
        {
            if (Compare.IsNullOrEmpty(language))
                language = "en-US";

            CultureInfo rollbackCulture = null;
            CultureInfo rollbackUICulture = null;
            string rollbackCurrency = null;
            Thread thread = Thread.CurrentThread;

            try
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture(language != "en-US" ? language.Substring(0, 2) : language);

                rollbackCulture = thread.CurrentCulture;
                thread.CurrentCulture = culture;

                rollbackUICulture = thread.CurrentUICulture;
                thread.CurrentUICulture = culture;

                if (!string.IsNullOrEmpty(currencySymbol))
                {
                    rollbackCurrency = thread.CurrentCulture.NumberFormat.CurrencySymbol;
                    thread.CurrentCulture.NumberFormat.CurrencySymbol = currencySymbol;
                }
            }
            catch (Exception)
            {
                RollbackLocaleSetting(thread, rollbackCulture, rollbackUICulture, rollbackCurrency);
            }
        }

        private static void RollbackLocaleSetting(Thread thread, CultureInfo rollbackCulture, CultureInfo rollbackUICulture, string rollbackCurrency)
        {
            if (rollbackCulture != null)
                thread.CurrentCulture = rollbackCulture;
            if (rollbackUICulture != null)
                thread.CurrentUICulture = rollbackUICulture;
            if (rollbackCurrency != null)
                thread.CurrentCulture.NumberFormat.CurrencySymbol = rollbackCurrency;
        }
    }
}