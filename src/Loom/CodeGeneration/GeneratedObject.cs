#region Using Directives

using System.Diagnostics;

#endregion

namespace Loom.CodeGeneration
{
    [DebuggerDisplay("Name={" + nameof(Name) + "}")]
    public class GeneratedObject
    {
        public GeneratedObject(string content, string name)
        {
            Name = name;
            Content = content;
        }

        public string Content { get; }
        public string Name { get; }

        public override string ToString()
        {
            return Content;
        }
    }
}