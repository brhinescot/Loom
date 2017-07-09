#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    public sealed class ControlExtender<T> where T : Control
    {
        private const string GuidFormat = "N";

        private readonly HttpContext context = HttpContext.Current;
        private string clientId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ControlExtender{T}" /> class.
        /// </summary>
        /// <param name="control">The <see cref="Control" /> to extend.</param>
        public ControlExtender(T control)
        {
            if (control != null)
            {
                control.Init += HandleControlInit;
                control.PreRender += HandleControlPreRender;
                control.Unload += HandleControlUnload;
            }

            AdditionalLocalizables = new Collection<string>();
        }

        /// <summary>
        ///     Gets a collection of additional properties that may be localized via the ASP.net
        ///     localization services.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Adding property names to this collection allows non-localizable
        ///         properties (properties not decorated with <see cref="LocalizableAttribute" />)
        ///         to be treated as localizable.
        ///     </para>
        ///     <para>
        ///         For example; <see cref="HyperLink" /> urls are not localizabe by default. Using this property,
        ///         the web application may serve different urls depending on the users locale.
        ///     </para>
        /// </remarks>
        public Collection<string> AdditionalLocalizables { get; }

        private void HandleControlInit(object sender, EventArgs e)
        {
            T control = sender as T;

            ISpamGuardian spamGuardian = sender as ISpamGuardian;
            if (spamGuardian != null && spamGuardian.AntiSpam)
                AntiSpamInitPrivate(control);

            ILocalizable localizable = sender as ILocalizable;
            if (localizable != null && !string.IsNullOrEmpty(localizable.ResourceKey))
                ControlLocalizer.Localize(control, localizable.ResourceKey, AdditionalLocalizables);
        }

        private void HandleControlPreRender(object sender, EventArgs e)
        {
            T control = sender as T;

            ISpamGuardian spamGuardian = sender as ISpamGuardian;
            if (spamGuardian != null && spamGuardian.AntiSpam)
                AntiSpamPreRenderPrivate(control);
        }

        private void HandleControlUnload(object sender, EventArgs e)
        {
            T control = sender as T;

            if (control == null)
                return;

            control.Init -= HandleControlInit;
            control.PreRender -= HandleControlPreRender;
            control.Disposed -= HandleControlUnload;
        }

        public void AntiSpamInit(T control)
        {
            Argument.Assert.IsNotNull(control, nameof(control));

            AntiSpamInitPrivate(control);
        }

        public void AntiSpamPreRender(T control)
        {
            Argument.Assert.IsNotNull(control, nameof(control));

            AntiSpamPreRenderPrivate(control);
        }

        private void AntiSpamInitPrivate(T guardian)
        {
            // Cache the controls current id.
            clientId = guardian.ID;

            // If the session is valid then the control has been rendered
            // with a random id on the previous page request. Set the 
            // control's ID back to the value it was last rendered to 
            // enable page level access to the control.
            //
            if (context.Session[clientId] != null)
                guardian.ID = context.Session[clientId].ToString();
        }

        private void AntiSpamPreRenderPrivate(T guardian)
        {
            // The page has finished processing the control for the
            // curret request. Generate a new random ID for the
            // control.
            //
            string randomId = Guid.NewGuid().ToString(GuidFormat);

            // Insert the new random ID into session state for 
            // retrieval on the next request.
            //
            context.Session[clientId] = randomId;

            // Set the control's id to the new random id for rendering
            // to the client.
            //
            guardian.ID = randomId;
        }
    }
}