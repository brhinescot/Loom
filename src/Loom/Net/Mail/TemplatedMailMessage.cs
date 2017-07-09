#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace Loom.Net.Mail
{
    /// <summary>
    ///     Represents a templated e-mail message that can be sent using the <see cref="SmtpClient" /> class.
    /// </summary>
    public class TemplatedMailMessage : MailMessage
    {
        private const string MultiLineCommentEnd = "*/";
        private const string MultiLineCommentStart = "/*";
        private const string SingleLineComment = "//";

        private static readonly Regex MergeFieldRegex = new Regex(@"{{(\w*)}}", RegexOptions.Compiled);
        private readonly MergeFields mergeFields = new MergeFields();

        /// <summary>
        ///     Initializes a new instance of the <see cref="TemplatedMailMessage" /> class.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public TemplatedMailMessage(string path, string from, string to) : base(from, to)
        {
            LoadContent(path);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TemplatedMailMessage" /> class.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public TemplatedMailMessage(string path, string from, string to, string subject, string body) : base(from, to, subject, body)
        {
            LoadContent(path);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TemplatedMailMessage" /> class.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public TemplatedMailMessage(string path, MailAddress from, MailAddress to) : base(from, to)
        {
            LoadContent(path);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TemplatedMailMessage" /> class.
        /// </summary>
        /// <param name="path"></param>
        public TemplatedMailMessage(string path)
        {
            LoadContent(path);
        }

        /// <summary>
        ///     Merges the values of the supplied object's properties with the template and sets
        ///     the <see cref="MailMessage.Body" /> property to the result.
        /// </summary>
        /// <remarks>
        ///     If the supplied object is null, the <see cref="MailMessage.Body" /> of this
        ///     instance is set to it's non-merged template value.
        /// </remarks>
        public object MergeObject { get; private set; }

        /// <summary>
        /// </summary>
        public string Layout { get; set; }

        /// <summary>
        /// </summary>
        public string Content { get; private set; }

        public void Merge(object obj)
        {
            Argument.Assert.IsNotNull(obj, nameof(obj));

            MergeTemplate(obj);
        }

        public void AddTemplate(string name, string value)
        {
            mergeFields.Add(name, value);
        }

        public string GetMergeValue(string fieldName)
        {
            Argument.Assert.IsNotNullOrEmpty(fieldName, nameof(fieldName));

            string value;
            return mergeFields.TryGetValue(fieldName, out value) ? value : null;
        }

        private void LoadContent(string path)
        {
            Content = path;

            Argument.Assert.IsNotNullOrEmpty(path, nameof(path));
            Argument.Assert.FileExists(path);

            foreach (Match capture in MergeFieldRegex.Matches(Body))
                mergeFields.Add(capture.Groups[1].Value, null);
        }

        private string MergeWithLayout(string content)
        {
            IDictionary<string, string> fields = new Dictionary<string, string>();
            fields.Add("Content", content);
            return FormattableObject.ToString(fields, ReadTemplate(Layout), null);
        }

        private static string ReadTemplate(string path)
        {
            bool inComment = false;

            StringBuilder builder = new StringBuilder();
            using (StreamReader reader = File.OpenText(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string trimmedLine = line.Trim();

                    if (!inComment && trimmedLine.StartsWith(MultiLineCommentStart))
                    {
                        if (!trimmedLine.EndsWith(MultiLineCommentEnd))
                            inComment = true;
                        continue;
                    }

                    if (inComment && trimmedLine.EndsWith(MultiLineCommentEnd))
                    {
                        inComment = false;
                        continue;
                    }

                    if (inComment)
                        continue;

                    if (trimmedLine.StartsWith(SingleLineComment))
                        continue;

                    int endCommentIndex = line.IndexOf("//", StringComparison.Ordinal);
                    builder.AppendLine(endCommentIndex >= 0 ? line.Substring(0, endCommentIndex) : line);
                }
            }
            return builder.ToString();
        }

        // Merge the member values of the specified object.
        private void MergeTemplate(object obj)
        {
            MergeObject = obj;

            // Fill any templates that may be in the merge fields.
            foreach (string key in mergeFields.Keys.ToArray())
                mergeFields[key] = FormattableObject.ToString(obj, mergeFields[key], null, mergeFields);

            // Read the template from the file.
            string content = ReadTemplate(Content);

            // If there is a layout then merge in the template.
            if (!string.IsNullOrEmpty(Layout))
                content = MergeWithLayout(content);

            // Fill any templates that my be in the subject.
            Subject = FormattableObject.ToString(obj, Subject, null, mergeFields);

            // Set the body of the email to the final parsed content.
            Body = FormattableObject.ToString(obj, content, null, mergeFields);
        }
    }
}