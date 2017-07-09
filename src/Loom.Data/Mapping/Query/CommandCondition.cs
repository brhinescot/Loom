namespace Loom.Data.Mapping.Query
{
    // TODO: Change to use a Command<T> class for Delete, Update, and Insert commands.
    public abstract class CommandCondition<TDataRecord, TCommand, TCommandCondition>
        where TDataRecord : DataRecord<TDataRecord>, new()
        where TCommandCondition : CommandCondition<TDataRecord, TCommand, TCommandCondition>
    {
        #region Member Fields

        private readonly ColumnPredicateCollection predicates;
        private readonly TCommand command;

        #endregion

        #region .ctor

        internal CommandCondition(TCommand command, ColumnPredicateCollection predicates)
        {
            Argument.Assert.IsNotNull(command, Argument.Names.query);

            this.command = command;
            this.predicates = predicates;
        }

        #endregion

        #region Public Construction Methods

        public TCommandCondition Or(ColumnPredicate predicate)
        {
            if (predicate != null)
            {
                predicate.OrToPreviousGroup = true;
                predicates.Add(predicate);
            }
            return (TCommandCondition) this;
        }

        public TCommandCondition And(ColumnPredicate predicate)
        {
            if(predicate != null)
                predicates.Add(predicate);
            return (TCommandCondition) this;
        }

        /// <summary>
        /// Ends the current condition and returns the command object for further querying.
        /// </summary>
        /// <returns></returns>
        public TCommand End()
        {
            return command;
        }

        #endregion
    }
}
