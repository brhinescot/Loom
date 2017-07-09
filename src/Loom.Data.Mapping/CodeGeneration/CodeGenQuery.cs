namespace Loom.Data.Mapping.CodeGeneration
{
    public sealed class CodeGenQuery
    {
        private CodeGenQuery(TableDefinition table)
        {
            Table = table;
        }

        public TableDefinition Table { get; }

        ///<summary>
        ///</summary>
        ///<param name="table"></param>
        ///<returns></returns>
        public static CodeGenQuery From(TableDefinition table)
        {
            Argument.Assert.IsNotNull(table, nameof(table));

            return new CodeGenQuery(table);
        }
    }
}