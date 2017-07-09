#region Using Directives

using System.Data.Common;

#endregion

namespace Loom.Data
{
    public interface ICommandFactory
    {
        DbCommand CreateCommand();
    }
}