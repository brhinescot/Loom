#region Using Directives

using System.Collections.Generic;

#endregion

namespace Loom.Documents.Pdf
{
    public sealed class PdfFormValues
    {
        private readonly List<FormField> innerList = new List<FormField>();

        public void Add(string name, string value)
        {
            innerList.Add(new FormField(name, value));
        }

        public IEnumerable<FormField> GetValues()
        {
            return innerList;
        }

        internal string RetrieveFieldValue(string name)
        {
            for (int i = 0; i < innerList.Count; i++)
            {
                FormField value = innerList[i];
                if (value.Name == name)
                    return value.Value;
            }
            return null;
        }
    }
}