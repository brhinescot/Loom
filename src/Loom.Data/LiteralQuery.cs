#region Using Directives

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Loom.Data.Entities;

#endregion

namespace Loom.Data
{
    public class LiteralQuery : ICommandFactory
    {
        protected LiteralQuery() { }

        public LiteralQuery(IDataSessionBase session, string commandText, params object[] parameters)
        {
            Session = session;
            CommandText = commandText;
            Parameters = parameters;
        }

        internal IDataSessionBase Session { get; set; }

        internal string CommandText { get; }
        public object[] Parameters { get; set; }

        #region ICommandFactory Members

        public virtual DbCommand CreateCommand()
        {
            DbCommand dbCommand = Session.CreateCommand(CommandText, Parameters);
            Session.EnlistConnection(dbCommand);
            return dbCommand;
        }

        #endregion

        public virtual DataSet FetchDataSet()
        {
            return Session.FetchDataSet(CommandText, Parameters);
        }

        public virtual IDataReader FetchReader()
        {
            return Session.ExecuteReader(CommandText, Parameters);
        }

        public virtual object FetchScalar()
        {
            return Session.ExecuteScalar(CommandText, Parameters);
        }

        public void FillEntity<T>(T entity)
        {
            using (DbCommand command = CreateCommand())
            {
                IEntityAdapter adapter = new EntityAdapter(command);
                adapter.MissingPropertyMappingAction = MissingPropertyMappingAction.Ignore;
                adapter.Fill(entity);
            }
        }

        public void FillEntityCollection<T>(ICollection<T> entityCollection) where T : new()
        {
            using (DbCommand command = CreateCommand())
            {
                IEntityAdapter adapter = new EntityAdapter(command);
                adapter.MissingPropertyMappingAction = MissingPropertyMappingAction.Ignore;
                adapter.FillCollection(entityCollection);
            }
        }
    }
}