namespace Loom.Data.Mapping.Query
{
    // TODO: Change to use a Command<T> class for Delete, Update, and Insert commands.
    public abstract class CommandCondition<TDataRecord, TCommand, TCommandCondition>
        where TDataRecord : DataRecord<TDataRecord>, new()
        where TCommandCondition : CommandCondition<TDataRecord, TCommand, TCommandCondition>
    {
        private readonly TCommand command;

        private readonly ColumnPredicateCollection predicates;

        internal CommandCondition(TCommand command, ColumnPredicateCollection predicates)
        {
            Argument.Assert.IsNotNull(command, nameof(command));

            this.command = command;
            this.predicates = predicates;
        }

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
            if (predicate != null)
                predicates.Add(predicate);
            return (TCommandCondition) this;
        }

        /// <summary>
        ///     Ends the current condition and returns the command object for further querying.
        /// </summary>
        /// <returns></returns>
        public TCommand End()
        {
            return command;
        }
    }
}