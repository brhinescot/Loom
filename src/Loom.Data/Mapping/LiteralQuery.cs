#region File Header

//  *************************************************************************
//  Copyright © 2008 Colossus Interactive, LLC
//  All Rights Reserved
//   
//  Unauthorized reproduction or distribution in source or compiled
//  form is strictly prohibited.
//   
//  http://www.colossusinteractive.com
//  licensing@colossusinteractive.com
//   
//  *************************************************************************

#endregion

#region Using Directives

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Loom.Annotations;
using Loom.Data.Entities;

#endregion

namespace Loom.Data.Mapping
{
    public class LiteralQuery
    {
        internal DataSession Session { get; set; }

        internal string CommandText { get; private set; }
        public object[] Parameters { get; set; }

        protected LiteralQuery() {}

        public LiteralQuery(DataSession session, string commandText, params object[] parameters)
        {
            Session = session;
            CommandText = commandText;
            Parameters = parameters;
        }

        public virtual DbCommand CreateCommand()
        {
            return Session.CreateCommand(CommandText, Parameters);
        }

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
                Session.EnlistConnection(command);
                IEntityAdapter adapter = new EntityAdapter(command);
                adapter.MissingPropertyMappingAction = MissingPropertyMappingAction.Ignore;
                adapter.Fill(entity);
            }
        }

        public void FillEntityCollection<T>(ICollection<T> entityCollection) where T : new()
        {
            using (DbCommand command = CreateCommand())
            {
                Session.EnlistConnection(command);
                IEntityAdapter adapter = new EntityAdapter(command);
                adapter.MissingPropertyMappingAction = MissingPropertyMappingAction.Ignore;
                adapter.FillCollection(entityCollection);
            }
        }

        [NotNull]
        public CommandConverter Convert()
        {
            return new CommandConverter(this);
        }
    }
}