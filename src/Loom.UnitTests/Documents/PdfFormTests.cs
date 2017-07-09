#region Using Directives

using System;
using System.IO;
using Loom.Documents.Pdf;
using NUnit.Framework;

#endregion

namespace Loom.Documents
{
    [TestFixture]
    public class PdfFormTests
    {
        [Test]
        public void FillPdf()
        {
            const string outputPath = "Documents/FillPdf.pdf";
            File.Delete(outputPath);

            Pdf.MetaData meta = new Pdf.MetaData
            {
                Title = "OmniMount Invoice",
                Creator = "PdfFormTests.FillPdf()",
                Author = "Colossus Interactive",
                Keywords = "Test, Test2",
                Subject = "Consumer Website Payment 1 Invoice."
            };

            PdfFormValues formValues = new PdfFormValues();
            formValues.Add("BillTo[0]", "OmniMount Systems");
            formValues.Add("BillTo[1]", "8201 South 48th Street");
            formValues.Add("BillTo[2]", "Phoenix, AZ 85044");
            formValues.Add("InvoiceNumber", "1234567");
            formValues.Add("PONumber", "34523452");
            formValues.Add("For", "OmniMount Consumer Website");
            formValues.Add("Terms", "Net 30 days.");
            formValues.Add("TaxID", "26-2911208");
            formValues.Add("InvoiceDate", DateTime.Now.ToShortDateString());
            formValues.Add("BilledItem[0]", "Initial retainer for start of consumer website project.");
            formValues.Add("BilledAmount[0]", "$26,234.30");
            formValues.Add("BilledItem[1]", "Some other money I think I should get.");
            formValues.Add("BilledAmount[1]", "$1,000.00");
            formValues.Add("Subtotal", "$27,234.30");
            formValues.Add("Tax", "$0.00");
            formValues.Add("Total", "$27,234.30");

            PdfTemplate template = PdfTemplate.Open("Documents/Template.pdf");
            template.MetaData = meta;
            template.FormValues = formValues;
            template.Generate(outputPath);

            OldFileAssert.Exists(outputPath);
        }

        [Test]
        public void SaveToFileStream()
        {
            string outputPath = "Documents/SaveToFileStream.pdf";
            File.Delete(outputPath);

            using (FileStream stream = File.OpenWrite(outputPath))
            {
                PdfTemplate template = PdfTemplate.Open("Documents/Template.pdf");
                template.RemoveUsageRights = true;
                template.Save(stream);
            }

            OldFileAssert.Exists(outputPath);
        }

        [TestCase("", ExpectedException = typeof(ArgumentException))]
        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        public void InvalidFileNameForOpen(string fileName)
        {
            PdfTemplate.Open(fileName);
        }

        [TestCase("", ExpectedException = typeof(ArgumentException))]
        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        public void InvalidFileNameForSave(string fileName)
        {
            PdfTemplate form = PdfTemplate.Open("Documents/Template.pdf");
            form.Save(fileName);
        }

        [TestCase("", ExpectedException = typeof(ArgumentException))]
        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        public void InvalidFileNameForFlatten(string fileName)
        {
            PdfTemplate form = PdfTemplate.Open("Documents/Template.pdf");
            form.Generate(fileName);
        }
    }
}