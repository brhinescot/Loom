#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using Loom.Web.UI.PageParts;

#endregion

namespace Loom.Web.Tests
{
    public partial class PageParts : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TestPart part = new TestPart {HeaderText = "Section Header"};
            PageContainer1.Parts.Add(part);

            part = new TestPart {HeaderText = "Next Section Header"};
            PageContainer1.Parts.Add(part);
            part.PagePartStyle.HeaderBackgroundColor = Color.Salmon;

            part = new TestPart {HeaderText = "Another Section Header"};
            PageContainer1.Parts.Add(part);
            part.RemoveHeader = true;

            part = new TestPart {HeaderText = "That Section Header"};
            PageContainer1.Parts.Add(part);
        }

        #region Nested type: TestPart

        public class TestPart : PagePart
        {
            private string headerId;

            protected override void AddHeaderAttributesToRender(ContentRenderer renderer)
            {
                headerId = Guid.NewGuid().ToString();
                renderer.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "hand");
                renderer.AddClientEvent(ClientEvent.OnClick, InlineClientScript.ToggleVisibility(headerId));
                base.AddHeaderAttributesToRender(renderer);
            }

            protected override void AddContentAttributesToRender(ContentRenderer renderer)
            {
                renderer.AddAttribute(HtmlTextWriterAttribute.Id, headerId);
                base.AddContentAttributesToRender(renderer);
            }

            protected override void OnRenderContent(ContentRenderer renderer)
            {
                renderer.AddClientEvent(ClientEvent.OnFocus, InlineClientScript.UpdateCssClass("inputhover"));
                renderer.AddClientEvent(ClientEvent.OnBlur, InlineClientScript.UpdateCssClass(string.Empty));
                renderer.RenderTextBox("Test", "TestId", "None", "TestClass", false);

                renderer.RenderTextBox("Test", "TestId", "None", "TestClass", true);

                List<string> list = new List<string> {"Donkeys", "Dogs", "Monkeys", "Hogs"};
                renderer.RenderRepeater(list, new HtmlListRenderer<string>());

                renderer.AddClientEvent(ClientEvent.OnMouseOver, InlineClientScript.ChangeImage("files/search.gif"));
                renderer.AddClientEvent(ClientEvent.OnMouseOut, InlineClientScript.ChangeImage("files/steelBlueIndicatorBig.gif"));
                renderer.RenderImage("files/steelBlueIndicatorBig.gif");

                renderer.InnerWriter.WriteBreak();
                renderer.InnerWriter.WriteBreak();

                renderer.AddAttribute(HtmlTextWriterAttribute.Href, "http://www.google.com");
                renderer.AddClientEvent(ClientEvent.OnMouseOver, InlineClientScript.UpdateWindowStatus("You will be asked to confirm this link."));
                renderer.AddClientEvent(ClientEvent.OnMouseOut, InlineClientScript.UpdateWindowStatus(string.Empty));
                renderer.AddClientEvent(ClientEvent.OnClick, InlineClientScript.ConfirmAction("Are you sure you want to do this?"));
                renderer.InnerWriter.RenderBeginTag(HtmlTextWriterTag.A);
                renderer.RenderLiteral("Go to Google");
                renderer.InnerWriter.RenderEndTag();

                base.OnRenderContent(renderer);
            }
        }

        #endregion
    }
}