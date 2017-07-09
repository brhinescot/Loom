#region Using Directives

using System;

#endregion

namespace Loom.Web.Portal.Controllers
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Class)]
    public sealed class HtmlEncodeAttribute : Attribute
    {
        public HtmlEncodeAttribute() : this(true) { }

        public HtmlEncodeAttribute(bool encode)
        {
            Encode = encode;
        }

        public bool Encode { get; }
    }
}