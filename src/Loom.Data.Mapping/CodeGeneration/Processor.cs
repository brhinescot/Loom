#region Using Directives

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Humanizer;
using Loom.CodeGeneration;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    /// <summary>
    ///     A class for generating design time code from a database schema.
    /// </summary>
    public class Processor : IFileProcessor
    {
        private readonly ClassProcessor classProcessor;
        private readonly ProcedureProcessor procedureProcessor;
        private readonly CodeGenSession session;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Processor" /> class.
        /// </summary>
        public Processor()
        {
            session = new CodeGenSession();
            classProcessor = new ClassProcessor(session);
            procedureProcessor = new ProcedureProcessor(session);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Processor" /> class.
        /// </summary>
        /// <param name="configurationFilePath">The path to the configuration file.</param>
        public Processor(string configurationFilePath)
        {
            Argument.Assert.IsNotNullOrEmpty(configurationFilePath, nameof(configurationFilePath));

            session = new CodeGenSession(configurationFilePath);
            classProcessor = new ClassProcessor(session);
            procedureProcessor = new ProcedureProcessor(session);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Processor" /> class.
        /// </summary>
        /// <param name="session">The <see cref="CodeGenSession" /> to use.</param>
        internal Processor(CodeGenSession session)
        {
            Argument.Assert.IsNotNullOrEmpty("session", nameof(session));

            this.session = session;
            classProcessor = new ClassProcessor(session);
            procedureProcessor = new ProcedureProcessor(session);
        }

        #region IFileProcessor Members

        public IEnumerable<GeneratedObject> GenerateObjects()
        {
            List<string> namespaces = new List<string>();

            if (session.Configuration.CodeGen.GenerateTables)
            {
                foreach (TableDefinition table in session.Schema.Tables)
                {
                    if (table.IsReadOnly)
                        continue;

                    string classNamespace = session.Configuration.CodeGen.BaseNamespace.Trim();
                    if (!Compare.IsNullOrEmpty(classNamespace) && !Compare.AreSameOrdinalIgnoreCase(table.Owner, "dbo"))
                        classNamespace += ".";

                    if (!Compare.AreSameOrdinalIgnoreCase(table.Owner, "dbo"))
                        classNamespace += table.Owner;

                    if (!namespaces.Contains(classNamespace))
                        namespaces.Add(classNamespace);
                }

                foreach (TableDefinition table in session.Schema.Tables)
                {
                    if (table.IsReadOnly)
                        continue;

                    string name = Compare.AreSameOrdinalIgnoreCase(table.Owner, "dbo") ? table.Name : table.Owner + "." + table.Name;
                    yield return new GeneratedObject(GenerateSingleClass(table, namespaces), name);
                }
            }

            if (session.Configuration.CodeGen.GenerateViews)
            {
                foreach (TableDefinition table in session.Schema.Tables)
                {
                    if (!table.IsReadOnly)
                        continue;

                    string classNamespace = session.Configuration.CodeGen.BaseNamespace.Trim();
                    if (!Compare.IsNullOrEmpty(classNamespace) && !Compare.AreSameOrdinalIgnoreCase(table.Owner, "dbo"))
                        classNamespace += ".";

                    if (!Compare.AreSameOrdinalIgnoreCase(table.Owner, "dbo"))
                        classNamespace += table.Owner;

                    if (!namespaces.Contains(classNamespace))
                        namespaces.Add(classNamespace);
                }

                foreach (TableDefinition table in session.Schema.Tables)
                {
                    if (!table.IsReadOnly)
                        continue;

                    string name = Compare.AreSameOrdinalIgnoreCase(table.Owner, "dbo") ? table.Name : table.Owner + "." + table.Name;
                    yield return new GeneratedObject(GenerateSingleClass(table, namespaces), name);
                }
            }

            if (session.Configuration.CodeGen.GenerateProcedures)
            {
                foreach (ProcedureDefinition procedure in session.Schema.Procedures)
                {
                    string classNamespace = session.Configuration.CodeGen.BaseNamespace.Trim();
                    if (!Compare.IsNullOrEmpty(classNamespace) && !Compare.AreSameOrdinalIgnoreCase(procedure.Owner, "dbo"))
                        classNamespace += ".";

                    if (!Compare.AreSameOrdinalIgnoreCase(procedure.Owner, "dbo"))
                        classNamespace += procedure.Owner;

                    if (!namespaces.Contains(classNamespace))
                        namespaces.Add(classNamespace);
                }

                foreach (ProcedureDefinition procedure in session.Schema.Procedures)
                {
                    string name = Compare.AreSameOrdinalIgnoreCase(procedure.Owner, "dbo") ? procedure.Name : procedure.Owner + "." + procedure.Name;
                    yield return new GeneratedObject(GenerateSingleProcedure(procedure, namespaces), name);
                }
            }

            string className = session.Configuration.Provider.Name;
            yield return new GeneratedObject(GenerateDataSessionInterface(namespaces, className), "I" + className);
            yield return new GeneratedObject(GenerateDataSession(namespaces, className), className);
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string GenerateAllObjects()
        {
            Dictionary<string, StringBuilder> lookup = new Dictionary<string, StringBuilder>();

            StringBuilder output = new StringBuilder();
            AddBaseUsingDirectives(output);

            string baseNamespace = session.Configuration.CodeGen.BaseNamespace.Trim();

            if (session.Configuration.CodeGen.GenerateTables)
                GenerateFromTables(baseNamespace, lookup, output);
            if (session.Configuration.CodeGen.GenerateViews)
                GenerateFromViews(baseNamespace, lookup, output);
            if (session.Configuration.CodeGen.GenerateProcedures)
                GenerateFromProcedures(baseNamespace, lookup, output);

            AddBaseNamespaceStart(output, baseNamespace);

            StringBuilder value;
            if (lookup.Count == 1 && lookup.TryGetValue("Dbo", out value))
            {
                output.AppendLine(value.ToString());
                AddBaseNamespaceEnd(output, baseNamespace);
                return output.ToString();
            }

            foreach (KeyValuePair<string, StringBuilder> pair in lookup)
            {
                if (Compare.AreSameOrdinalIgnoreCase(pair.Key, "dbo"))
                {
                    output.AppendLine(pair.Value.ToString());
                    continue;
                }

                output.AppendLine("namespace " + pair.Key);
                output.AppendLine("{");
                output.AppendLine(pair.Value.ToString());
                output.AppendLine("}");
            }

            AddBaseNamespaceEnd(output, baseNamespace);
            return output.ToString();
        }

        private string GenerateDataSessionInterface(IEnumerable<string> namespaces, string className)
        {
            StringBuilder namespaceOutput = new StringBuilder();
            StringBuilder propertyOutput = new StringBuilder();

            foreach (string s in namespaces)
                namespaceOutput.AppendLine("using " + s + ";");

            string classTemplate = Templates.Interface
                .Replace(Tokens.NamespaceList, namespaceOutput.ToString())
                .Replace(Tokens.Namespace, session.Configuration.CodeGen.BaseNamespace)
                .Replace(Tokens.ClassName, className.ToPascalCase());

            string propertyTemplate = Templates.DataSessionInterfaceProperty;

            if (session.Configuration.CodeGen.GenerateTables)
                foreach (TableDefinition table in session.Schema.Tables)
                {
                    if (table.IsReadOnly || table.IsEnum || table.IsLookup)
                        continue;

                    propertyOutput.AppendLine(propertyTemplate
                        .Replace(Tokens.TableName, table.Name.ToPascalCase().Singularize(Plurality.CouldBeEither))
                        .Replace(Tokens.Namespace, session.Configuration.CodeGen.BaseNamespace)
                        .Replace(Tokens.SchemaName, table.Owner.Equals("dbo", StringComparison.OrdinalIgnoreCase) ? null : table.Owner + ".")
                        .Replace(Tokens.PropertyName, table.Name.ToPascalCase().Pluralize(Plurality.CouldBeEither)));

                    propertyOutput.AppendLine();
                }

            if (session.Configuration.CodeGen.GenerateViews)
                foreach (TableDefinition table in session.Schema.Tables)
                {
                    if (!table.IsReadOnly || table.IsEnum || table.IsLookup)
                        continue;

                    propertyOutput.AppendLine(propertyTemplate
                        .Replace(Tokens.TableName, table.Name.ToPascalCase())
                        .Replace(Tokens.Namespace, session.Configuration.CodeGen.BaseNamespace)
                        .Replace(Tokens.SchemaName, table.Owner.Equals("dbo", StringComparison.OrdinalIgnoreCase) ? null : table.Owner + ".")
                        .Replace(Tokens.PropertyName, table.Name.ToPascalCase() + "s"));
                    propertyOutput.AppendLine();
                }

            classTemplate = classTemplate.Replace(Tokens.Properties, propertyOutput.ToString());

            return classTemplate;
        }

        private string GenerateDataSession(IEnumerable<string> namespaces, string className)
        {
            StringBuilder namespaceOutput = new StringBuilder();
            StringBuilder propertyOutput = new StringBuilder();

            foreach (string s in namespaces)
                namespaceOutput.AppendLine("using " + s + ";");

            string classTemplate = Templates.DataSessionClass
                .Replace(Tokens.NamespaceList, namespaceOutput.ToString())
                .Replace(Tokens.Namespace, session.Configuration.CodeGen.BaseNamespace)
                .Replace(Tokens.ClassName, className.ToPascalCase())
                .Replace(Tokens.ProviderName, session.Configuration.Provider.Name);

            string propertyTemplate = Templates.DataSessionProperty;

            if (session.Configuration.CodeGen.GenerateTables)
                foreach (TableDefinition table in session.Schema.Tables)
                {
                    if (table.IsReadOnly || table.IsEnum || table.IsLookup)
                        continue;

                    propertyOutput.AppendLine(propertyTemplate
                        .Replace(Tokens.TableName, table.Name.ToPascalCase().Singularize(Plurality.CouldBeEither))
                        .Replace(Tokens.Namespace, session.Configuration.CodeGen.BaseNamespace)
                        .Replace(Tokens.SchemaName, table.Owner.Equals("dbo", StringComparison.OrdinalIgnoreCase) ? null : table.Owner + ".")
                        .Replace(Tokens.PropertyName, table.Name.ToPascalCase().Pluralize(Plurality.CouldBeEither)));

                    propertyOutput.AppendLine();
                }

            if (session.Configuration.CodeGen.GenerateViews)
                foreach (TableDefinition table in session.Schema.Tables)
                {
                    if (!table.IsReadOnly || table.IsEnum || table.IsLookup)
                        continue;

                    propertyOutput.AppendLine(propertyTemplate
                        .Replace(Tokens.TableName, table.Name.ToPascalCase())
                        .Replace(Tokens.Namespace, session.Configuration.CodeGen.BaseNamespace)
                        .Replace(Tokens.SchemaName, table.Owner.Equals("dbo", StringComparison.OrdinalIgnoreCase) ? null : table.Owner + ".")
                        .Replace(Tokens.PropertyName, table.Name.ToPascalCase() + "s"));
                    propertyOutput.AppendLine();
                }

            classTemplate = classTemplate.Replace(Tokens.Properties, propertyOutput.ToString());

            return classTemplate;
        }

        public string GenerateSingleClass(string schema, string table, IEnumerable<string> namespaces)
        {
            return GenerateSingleClass(session.Schema.Tables.FindTable(schema, table), namespaces);
        }

        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public string GenerateSingleClass(TableDefinition table, IEnumerable<string> namespaces)
        {
            Argument.Assert.IsNotNull(table, nameof(table));

            StringBuilder output = new StringBuilder();
            output.AppendLine("#region Using Directives");
            output.AppendLine("using System;");
            output.AppendLine("using System.Data;");
            output.AppendLine("using System.Xml;");
            output.AppendLine("using System.Xml.Schema;");
            output.AppendLine("using System.Xml.Serialization;");
            output.AppendLine("using System.Runtime.Serialization;");
            output.AppendLine("using Loom;");
            output.AppendLine("using Loom.Data.Mapping;");
            output.AppendLine("using Loom.Data.Mapping.Schema;");

            foreach (string ns in namespaces)
                output.AppendLine(string.Format("using {0};", ns));
            output.AppendLine("#endregion");

            string classNamespace = session.Configuration.CodeGen.BaseNamespace.Trim();
            if (!Compare.AreSameOrdinalIgnoreCase(table.Owner, "dbo"))
                classNamespace += "." + table.Owner;

            AddBaseNamespaceStart(output, classNamespace);

            output.AppendLine(table.IsEnum ? GenerateEnum(table) : classProcessor.GenerateClass(table));

            AddBaseNamespaceEnd(output, classNamespace);

            return output.ToString();
        }

        public string GenerateSingleProcedure(string schemaName, string procedureName, IEnumerable<string> namespaces)
        {
            return GenerateSingleProcedure(session.Schema.Procedures.FindProcedure(schemaName, procedureName), namespaces);
        }

        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public string GenerateSingleProcedure(ProcedureDefinition procedure, IEnumerable<string> namespaces)
        {
            Argument.Assert.IsNotNull(procedure, nameof(procedure));

            StringBuilder output = new StringBuilder();
            output.AppendLine("#region Using Directives");
            output.AppendLine("using System;");
            output.AppendLine("using System.Data;");
            output.AppendLine("using System.Xml;");
            output.AppendLine("using System.Xml.Schema;");
            output.AppendLine("using System.Xml.Serialization;");
            output.AppendLine("using System.Runtime.Serialization;");
            output.AppendLine("using Loom;");
            output.AppendLine("using Loom.Data.Mapping;");
            output.AppendLine("using Loom.Data.Mapping.Schema;");
            foreach (string ns in namespaces)
                output.AppendLine(string.Format("using {0};", ns));
            output.AppendLine("#endregion");

            string baseNamespace = session.Configuration.CodeGen.BaseNamespace.Trim();
            if (!Compare.AreSameOrdinalIgnoreCase(procedure.Owner, "dbo"))
                baseNamespace += "." + procedure.Owner;

            AddBaseNamespaceStart(output, baseNamespace);
            output.AppendLine(procedureProcessor.GenerateProcedure(procedure));
            AddBaseNamespaceEnd(output, baseNamespace);

            return output.ToString();
        }

        private static void AddBaseUsingDirectives(StringBuilder output)
        {
            output.AppendLine("using System;");
            output.AppendLine("using System.Data;");
            output.AppendLine("using System.Xml;");
            output.AppendLine("using System.Xml.Schema;");
            output.AppendLine("using System.Xml.Serialization;");
            output.AppendLine("using System.Runtime.Serialization;");
            output.AppendLine("using Loom;");
            output.AppendLine("using Loom.Data.Mapping;");
            output.AppendLine("using Loom.Data.Mapping.Schema;");
        }

        private void GenerateFromTables(string baseNamespace, IDictionary<string, StringBuilder> lookup, StringBuilder output)
        {
            foreach (TableDefinition table in session.Schema.Tables)
            {
                if (table.IsReadOnly)
                    continue;

                string classNamespace = CodeFormat.ToPascalCase(table.Owner);
                // Separate tables into namespace buckets.
                StringBuilder value;
                if (lookup.TryGetValue(classNamespace, out value))
                {
                    if (table.IsEnum)
                    {
                        value.AppendLine(GenerateEnum(table));
                        value.AppendLine();
                    }
                    else
                    {
                        value.AppendLine(classProcessor.GenerateClass(table));
                        value.AppendLine();
                    }
                }
                else
                {
                    if (!Compare.AreSameOrdinal(table.Owner, "dbo"))
                        output.AppendLine(string.Format("using {0};", baseNamespace.Length > 0 ? baseNamespace + "." + classNamespace : classNamespace));

                    StringBuilder builder = new StringBuilder();
                    lookup.Add(classNamespace, builder);
                    if (table.IsEnum)
                    {
                        builder.AppendLine(GenerateEnum(table));
                        builder.AppendLine();
                    }
                    else
                    {
                        builder.AppendLine(classProcessor.GenerateClass(table));
                        builder.AppendLine();
                    }
                }
            }
        }

        private void GenerateFromViews(string baseNamespace, IDictionary<string, StringBuilder> lookup, StringBuilder output)
        {
            foreach (TableDefinition table in session.Schema.Tables)
            {
                if (!table.IsReadOnly)
                    continue;

                string classNamespace = CodeFormat.ToPascalCase(table.Owner);
                // Separate tables into namespace buckets.
                StringBuilder value;
                if (lookup.TryGetValue(classNamespace, out value))
                {
                    if (table.IsEnum)
                    {
                        value.AppendLine(GenerateEnum(table));
                        value.AppendLine();
                    }
                    else
                    {
                        value.AppendLine(classProcessor.GenerateClass(table));
                        value.AppendLine();
                    }
                }
                else
                {
                    if (!Compare.AreSameOrdinal(table.Owner, "dbo"))
                        output.AppendLine(string.Format("using {0};", baseNamespace.Length > 0 ? baseNamespace + "." + classNamespace : classNamespace));

                    StringBuilder builder = new StringBuilder();
                    lookup.Add(classNamespace, builder);
                    if (table.IsEnum)
                    {
                        builder.AppendLine(GenerateEnum(table));
                        builder.AppendLine();
                    }
                    else
                    {
                        builder.AppendLine(classProcessor.GenerateClass(table));
                        builder.AppendLine();
                    }
                }
            }
        }

        private void GenerateFromProcedures(string baseNamespace, IDictionary<string, StringBuilder> lookup, StringBuilder output)
        {
            foreach (ProcedureDefinition procedure in session.Schema.Procedures)
            {
                string classNamespace = CodeFormat.ToPascalCase(procedure.Owner);
                // Separate tables into namespace buckets.
                StringBuilder value;
                if (lookup.TryGetValue(classNamespace, out value))
                {
                    value.AppendLine(procedureProcessor.GenerateProcedure(procedure));
                    value.AppendLine();
                }
                else
                {
                    if (!Compare.AreSameOrdinalIgnoreCase(procedure.Owner, "dbo"))
                        output.AppendLine(string.Format("using {0};", baseNamespace.Length > 0 ? baseNamespace + "." + classNamespace : classNamespace));

                    StringBuilder builder = new StringBuilder();
                    lookup.Add(classNamespace, builder);
                    builder.AppendLine(procedureProcessor.GenerateProcedure(procedure));
                    builder.AppendLine();
                }
            }
        }

        private static void AddBaseNamespaceStart(StringBuilder output, string baseNamespace)
        {
            if (Compare.IsNullOrEmpty(baseNamespace))
                return;

            output.AppendLine();
            output.AppendLine("namespace " + baseNamespace);
            output.AppendLine("{");
        }

        private static void AddBaseNamespaceEnd(StringBuilder output, string baseNamespace)
        {
            if (!Compare.IsNullOrEmpty(baseNamespace))
                output.AppendLine("}");
        }

        private string GenerateEnum(TableDefinition table)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                using (IDataReader reader = session.ExecuteReader(CodeGenQuery.From(table)))
                {
                    int count = 0;
                    while (reader.Read())
                    {
                        string name = reader.GetString(table.ValueOrdinal);
                        if (Compare.IsNullOrEmpty(name))
                            name = "Empty";

                        if (count > 0)
                            builder.AppendLine();
                        builder.AppendLine(Indent.One + "/// <summary>");
                        builder.AppendLine(Indent.One + "/// " + name);
                        builder.AppendLine(Indent.One + "/// </summary>");
                        builder.AppendLine(Indent.One + "[EnumDescription(\"" + name + "\")]");
                        builder.AppendLine(Indent.One + CodeFormat.ToPascalCase(name, CodeFormatOptions.RemoveFKPrefix) + " = " + Convert.ToInt32(reader[table.KeyOrdinal]) + ",");
                        count++;
                    }

                    builder.Remove(builder.Length - Environment.NewLine.Length - 1, 1);
                }
                return Templates.Enum
                    .Replace(Tokens.ClassName, table.Name)
                    .Replace(Tokens.SchemaName, table.Owner)
                    .Replace(Tokens.TableName, table.Name)
                    .Replace(Tokens.EnumValues, builder.ToString());
            }
            catch (InvalidCastException ex)
            {
                builder.AppendLine("/*");
                builder.AppendLine("While attempting to generate an enum, this table failed with the following exception:");
                builder.AppendLine(ex.ToString());
                builder.AppendLine("*/");
                builder.AppendLine(classProcessor.GenerateClass(table));
            }
            return builder.ToString();
        }
    }
}