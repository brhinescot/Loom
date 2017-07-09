#region Using Directives

using System.IO;
using iTextSharp.text.pdf;

#endregion

namespace Loom.Documents.Pdf
{
    public class PdfTemplate
    {
        private PdfTemplate() { }

        public string FileName { get; private set; }
        public PdfFormValues FormValues { get; set; }
        public MetaData MetaData { get; set; }
        public bool RemoveUsageRights { get; set; }

        public static PdfTemplate Open(string fileName)
        {
            Argument.Assert.IsNotNullOrEmpty(fileName, nameof(fileName));

            PdfTemplate form = new PdfTemplate {FileName = fileName};
            return form;
        }

        /// <summary>
        ///     Flattens the PDF removing any interactive elements.
        /// </summary>
        /// <remarks>
        ///     This method always removes usage rights from the PDF.
        /// </remarks>
        /// <param name="fileName">The name of the file to which to save to PDF.</param>
        public void Generate(string fileName)
        {
            RemoveUsageRights = true;
            Save(fileName, true);
        }

        /// <summary>
        ///     Flattens the PDF removing any interactive elements.
        /// </summary>
        /// <remarks>
        ///     This method always removes usage rights from the PDF.
        /// </remarks>
        /// <param name="stream">The <see cref="Stream" /> to which to save to PDF.</param>
        public void Generate(Stream stream)
        {
            RemoveUsageRights = true;
            Save(stream, true);
        }

        public void Save(string fileName)
        {
            Save(fileName, false);
        }

        public void Save(string fileName, bool flatten)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                Save(fileStream, flatten);
            }
        }

        public void Save(Stream stream)
        {
            Save(stream, false);
        }

        public void Save(Stream stream, bool flatten)
        {
            PdfStamper stamper = null;
            try
            {
                PdfReader reader = new PdfReader(FileName);
                if (RemoveUsageRights)
                    reader.RemoveUsageRights();

                stamper = new PdfStamper(reader, stream);

                if (FormValues != null)
                {
                    AcroFields formFields = stamper.AcroFields;
                    foreach (FormField field in FormValues.GetValues())
                        formFields.SetField(field.Name, field.Value);
                }

                if (MetaData != null)
                {
                    stamper.MoreInfo = MetaData.ToHashtable();
                    stamper.XmpMetadata = MetaData.ToByteArray();
                }

                stamper.FormFlattening = flatten;
            }
            finally
            {
                if (stamper != null)
                    stamper.Close();
            }
        }
    }
}