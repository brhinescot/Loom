#region Using Directives

using System.Data;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace Loom.Data
{
    public interface IDbColumn
    {
        /// <summary>
        ///     Gets the data source <see cref="DbType" /> of this instance.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        DbType DbType { get; }

        int MaxLength { get; }

        /// <summary>
        ///     Gets the name of the column in the data source.
        /// </summary>
        string Name { get; }
    }
}