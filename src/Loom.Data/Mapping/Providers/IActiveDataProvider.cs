#region File Header

// Copyright © 2004, 2008 Colossus Interactive, LLC
// All Rights Reserved
//  
// Unauthorized reproduction or distribution in source or compiled
// form is strictly prohibited.
//  
// http://www.colossusinteractive.com
// licensing@colossusinteractive.com

#endregion

#region Using Directives

using System.Data.Common;
using Loom.Data.Mapping.CodeGeneration;
using Loom.Data.Mapping.Configuration;
using Loom.Data.Mapping.Query;

#endregion

namespace Loom.Data.Mapping.Providers
{
    public interface IActiveDataProvider : IDataSessionProvider
    {
        DbCommand FetchCommand(CodeGenQuery codeGenQuery);
        DbCommand FetchCommand(QueryTree queryTree, ConstraintType constraintType);
        DbCommand FetchCommand(Insert insert);
        DbCommand FetchCommand(Update update);
        DbCommand FetchCommand(Delete delete, bool obliterate);
        DbCommand FetchCommand<T>(StoredProcedure<T> procedure) where T : StoredProcedure<T>, new();
        DatabaseSchema FetchDatabaseSchema(string connectionString, ActiveMapCodeGenConfigurationSection configuration);
    }
}
