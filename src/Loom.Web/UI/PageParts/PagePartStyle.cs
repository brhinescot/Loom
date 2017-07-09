#region Using Directives

using System.Drawing;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.PageParts
{
    public sealed class PagePartStyle
    {
        private Color borderColor;
        private Unit borderWidth;
        private Color contentBackgroundColor;
        private Color contentColor;
        private Color headerBackgroundColor;
        private Color headerColor;
        private Unit headerFontHeight;
        private string[] headerFontNames;

        private Unit headerHeight;

        public Color BorderColor
        {
            get
            {
                if (borderColor == Color.Empty)
                    borderColor = Color.DarkGray;
                return borderColor;
            }
            set => borderColor = value;
        }

        public Unit BorderWidth
        {
            get
            {
                if (borderWidth == Unit.Empty)
                    borderWidth = new Unit(0);
                return borderWidth;
            }
            set => borderWidth = value;
        }

        public Unit HeaderHeight
        {
            get
            {
                if (headerHeight == Unit.Empty)
                    headerHeight = new Unit(23);
                return headerHeight;
            }
            set => headerHeight = value;
        }

        public Unit HeaderFontHeight
        {
            get
            {
                if (headerFontHeight == Unit.Empty)
                    headerFontHeight = new Unit(16);
                return headerFontHeight;
            }
            set => headerFontHeight = value;
        }

        public Color HeaderBackgroundColor
        {
            get
            {
                if (headerBackgroundColor == Color.Empty)
                    headerBackgroundColor = Color.DarkGray;
                return headerBackgroundColor;
            }
            set => headerBackgroundColor = value;
        }

        public Color HeaderColor
        {
            get
            {
                if (headerColor == Color.Empty)
                    headerColor = Color.White;
                return headerColor;
            }
            set => headerColor = value;
        }

        public Color ContentBackgroundColor
        {
            get
            {
                if (contentBackgroundColor == Color.Empty)
                    contentBackgroundColor = Color.White;
                return contentBackgroundColor;
            }
            set => contentBackgroundColor = value;
        }

        public Color ContentColor
        {
            get
            {
                if (contentColor == Color.Empty)
                    contentColor = Color.Black;
                return contentColor;
            }
            set => contentColor = value;
        }

        public string[] HeaderFontNames
        {
            get
            {
                if (headerFontNames == null)
                    headerFontNames = new[] {"Arial", "Helvetica", "sans-serif"};
                return headerFontNames;
            }
            set => headerFontNames = value;
        }
    }
}