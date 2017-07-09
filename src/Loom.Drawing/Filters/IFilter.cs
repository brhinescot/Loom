#region Using Directives

using System.Drawing;

#endregion

namespace Loom.Drawing.Filters
{
    /// <summary>
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        Image Apply(Image image);
    }
}