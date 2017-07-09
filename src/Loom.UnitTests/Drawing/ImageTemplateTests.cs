#region Using Directives

using System.Drawing;
using System.Text;
using NUnit.Framework;

#endregion

namespace Loom.Drawing
{
    [TestFixture]
    public class ImageTemplateTests
    {
        [Test]
        public void GenOne()
        {
            ImageTemplate definition = new ImageTemplate
            {
                Height = 120,
                Width = 300,
                Id = "Green",
                Style =
                {
                    BackColor = Color.Black
                }
            };

            BoxTemplate box = new BoxTemplate(definition)
            {
                Id = "Header",
                Height = 20,
                Width = 20,
                Left = 20
            };
            definition.Canvas.Add(box);

            box = new BoxTemplate(definition)
            {
                Id = "Description",
                Height = 20,
                Width = 20,
                Left = 20,
                Top = 20,
                Text = "Welcome Home!",
                Style =
                {
                    BackColor = Color.Red,
                    ForeColor = Color.White,
                    Image = "/images/logo.png",
                    ImageRepeat = ImageRepeat.Horizontal
                }
            };

            definition.Canvas.Add(box);

            string expected = definition.ToXml();
            ImageTemplate imageTemplate = ImageTemplate.FromXml(expected);
            string actual = imageTemplate.ToXml();

            StringBuilder message = new StringBuilder(100);
            message.AppendLine("------- ORIGINAL XML -------");
            message.AppendLine();
            message.AppendLine(expected);
            message.AppendLine();
            message.AppendLine("------- ROUNDTRIP XML -------");
            message.AppendLine();
            message.AppendLine(actual);
            message.AppendLine();

            Assert.AreEqual(expected, actual, message.ToString());
        }

        [Test]
        public void GenMany()
        {
            ImageTemplate definition = new ImageTemplate();
            definition.Height = 120;
            definition.Width = 300;
            definition.Id = "Green";

            definition.Style.BackColor = Color.Black;

            BoxTemplate box = new BoxTemplate(definition);
            box.Id = "Header";
            box.Height = 20;
            box.Width = 20;
            box.Left = 20;
            definition.Canvas.Add(box);

            box.Style.BackColor = Color.Red;
            box.Style.ForeColor = Color.White;
            box.Style.Image = "/images/logo.png";
            box.Style.ImageRepeat = ImageRepeat.Horizontal;

            box = new BoxTemplate(definition);
            box.Id = "Description";
            box.Height = 20;
            box.Width = 20;
            box.Left = 20;
            box.Top = 20;
            box.Text = "Welcome Home!";
            definition.Canvas.Add(box);

            box.Style.BackColor = Color.Red;
            box.Style.ForeColor = Color.White;
            box.Style.Image = "/images/logo.png";
            box.Style.ImageRepeat = ImageRepeat.Horizontal;

            //-------------------------------------------------------------------

            ImageTemplate definition2 = new ImageTemplate();
            definition2.Height = 120;
            definition2.Width = 300;
            definition2.Id = "Blue";

            definition2.Style.BackColor = Color.Black;

            box = new BoxTemplate(definition2);
            box.Id = "Header";
            box.Height = 20;
            box.Width = 20;
            box.Left = 20;
            definition2.Canvas.Add(box);

            box.Style.BackColor = Color.Red;
            box.Style.ForeColor = Color.White;
            box.Style.Image = "/images/logo.png";
            box.Style.ImageRepeat = ImageRepeat.Horizontal;

            box = new BoxTemplate(definition2);
            box.Id = "Description";
            box.Height = 20;
            box.Width = 20;
            box.Left = 20;
            box.Top = 20;
            box.Text = "Welcome Home!";
            definition2.Canvas.Add(box);

            box.Style.BackColor = Color.Red;
            box.Style.ForeColor = Color.White;
            box.Style.Image = "/images/logo.png";
            box.Style.ImageRepeat = ImageRepeat.Horizontal;

            ImageTemplateCollection definitions = new ImageTemplateCollection();
            definitions.Add(definition);
            definitions.Add(definition2);

            string expected = definitions.ToXml();
            string actual = ImageTemplateCollection.FromXml(expected).ToXml();

            StringBuilder message = new StringBuilder(100);
            message.AppendLine("------- ORIGINAL XML -------");
            message.AppendLine();
            message.AppendLine(expected);
            message.AppendLine();
            message.AppendLine("------- ROUNDTRIP XML -------");
            message.AppendLine();
            message.AppendLine(actual);
            message.AppendLine();

            Assert.AreEqual(expected, actual, message.ToString());
        }
    }
}