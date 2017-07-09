#region Using Directives

using System;
using System.Collections.Generic;
using Loom.Data.Mapping.Configuration;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    [Serializable]
    public sealed class ProcedureDefinitionCollection : SchemaDefinitionCollection<ProcedureDefinition>
    {
        public ProcedureDefinitionCollection(ActiveMapCodeGenConfigurationSection configuration) : base(configuration) { }

        protected override bool ExplicitInclude => Configuration.Procedures.ExplicitInclude;

        protected override IEnumerable<ISchema> GetExcludedItems()
        {
            foreach (ProceduresElement procedure in Configuration.Procedures)
                if (procedure.Exclude)
                    yield return new Schema(procedure.Owner, procedure.Name);
        }

        protected override IEnumerable<string> GetSchemaExcludes()
        {
            if (Configuration.CodeGen.SchemaExcludes != null)
                foreach (string s in Configuration.CodeGen.SchemaExcludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;

            if (Configuration.Procedures.SchemaExcludes != null)
                foreach (string s in Configuration.Procedures.SchemaExcludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;
        }

        protected override IEnumerable<string> GetPrefixExcludes()
        {
            if (Configuration.CodeGen.PrefixExcludes != null)
                foreach (string s in Configuration.CodeGen.PrefixExcludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;

            if (Configuration.Procedures.PrefixExcludes != null)
                foreach (string s in Configuration.Procedures.PrefixExcludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;
        }

        protected override IEnumerable<string> GetSuffixExcludes()
        {
            if (Configuration.CodeGen.SuffixExcludes != null)
                foreach (string s in Configuration.CodeGen.SuffixExcludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;

            if (Configuration.Procedures.SuffixExcludes != null)
                foreach (string s in Configuration.Procedures.SuffixExcludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;
        }

        protected override IEnumerable<string> GetSchemaIncludes()
        {
            if (Configuration.Procedures.SchemaIncludes != null)
                foreach (string s in Configuration.Procedures.SchemaIncludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;
        }

        protected override IEnumerable<string> GetPrefixIncludes()
        {
            if (Configuration.Procedures.PrefixIncludes != null)
                foreach (string s in Configuration.Procedures.PrefixIncludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;
        }

        protected override IEnumerable<string> GetSuffixIncludes()
        {
            if (Configuration.Procedures.SuffixIncludes != null)
                foreach (string s in Configuration.Procedures.SuffixIncludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;
        }

        public ProcedureDefinition FindProcedure(string schemaName, string procedureName)
        {
            string key = schemaName + procedureName;
            return !InnerList.ContainsKey(key) ? null : InnerList[key];
        }
    }
}