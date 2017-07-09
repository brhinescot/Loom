#region Using Directives

using System.ComponentModel;
using System.Globalization;
using System.Web;
using System.Web.UI;
using AttributeCollection = System.Web.UI.AttributeCollection;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ControlBuilder(typeof(ListItemControlBuilder))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [ParseChildren(true, "Text")]
    public sealed class ListItem : IAttributeAccessor, IParserAccessor
    {
        private AttributeCollection attributes;

        private StateBag attributeState;
        private string itemText;
        private string itemValue;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ListItem" /> class.
        /// </summary>
        public ListItem() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ListItem" /> class.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="disabled"></param>
        /// <param name="selected"></param>
        public ListItem(string text = null, string value = null, bool disabled = false, bool selected = false)
        {
            itemText = text;
            itemValue = value;
            Disabled = disabled;
            Selected = selected;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AttributeCollection Attributes
        {
            get
            {
                if (attributes == null)
                {
                    if (attributeState == null)
                        attributeState = new StateBag(true);
                    attributes = new AttributeCollection(attributeState);
                }
                return attributes;
            }
        }

        public bool Disabled { get; set; }
        public string Label { get; set; }
        public bool Selected { get; set; }

        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CssStyleCollection Style => Attributes.CssStyle;

        [Localizable(true)]
        [PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
        [DefaultValue("")]
        public string Text
        {
            get
            {
                if (itemText != null)
                    return itemText;
                return itemValue ?? string.Empty;
            }
            set => itemText = value;
        }

        [DefaultValue("")]
        [Localizable(true)]
        public string Value
        {
            get
            {
                if (itemValue != null)
                    return itemValue;
                return itemText ?? string.Empty;
            }
            set => itemValue = value;
        }

        public string Group { get; set; }

        internal bool HasAttributes => attributes != null && attributes.Count > 0;

        #region IAttributeAccessor Members

        string IAttributeAccessor.GetAttribute(string key)
        {
            return Attributes[key];
        }

        void IAttributeAccessor.SetAttribute(string key, string value)
        {
            Attributes[key] = value;
        }

        #endregion

        #region IParserAccessor Members

        void IParserAccessor.AddParsedSubObject(object obj)
        {
            LiteralControl literal = obj as LiteralControl;
            if (literal == null)
                throw new HttpException("Cannot have children of type " + obj.GetType().Name.ToString(CultureInfo.InvariantCulture));
            Text = literal.Text;
        }

        #endregion
    }
}