#region License information

/******************************************************************
 * Copyright © 2004 Brian Scott (DevInterop)
 * All Rights Reserved
 * 
 * Unauthorized reproduction or distribution in source or compiled
 * form is strictly prohibited.
 * 
 * http://www.devinterop.com
 * 
 * ****************************************************************/

#endregion

#region Using Directives

using System.Data;
using System.Text;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    internal class ProcedureProcessor
    {
        private readonly CodeGenSession session;

        public ProcedureProcessor(CodeGenSession session)
        {
            this.session = session;
        }

        public string GenerateProcedure(ProcedureDefinition definition)
        {
            StringBuilder parameterBuilder = new StringBuilder();
            foreach (ProcedureParameterDefinition parameterInfo in definition.Parameters)
            {
                parameterBuilder.AppendLine(Indent.Two + "public static ICallableParameter " + parameterInfo.ToPascalCase() + "{");
                parameterBuilder.AppendLine(Indent.Three + "get { return CreateParameter(\"" + parameterInfo.ToPascalCase() + "\", typeof(" + definition.ToPascalCase() + ")); }");
                parameterBuilder.AppendLine(Indent.Two + "}");
                parameterBuilder.AppendLine();
            }

            string className = definition.ToPascalCase();
            var procedureConfig = session.GetTableConfiguration(definition.Owner, definition.Name);
            if(procedureConfig != null)
            {
                if (!Compare.IsNullOrEmpty(procedureConfig.RenameTo))
                    className = procedureConfig.RenameTo;

                if (!Compare.IsNullOrEmpty(procedureConfig.AddPrefix))
                    className = procedureConfig.AddPrefix + className;
                if (!Compare.IsNullOrEmpty(procedureConfig.AddSuffix))
                    className += procedureConfig.AddSuffix;
            }

            return Templates.ProcedureClass.
                Replace(Tokens.SchemaAttributes, GenerateProcedureAttribute(definition)).
                Replace(Tokens.ParameterList, parameterBuilder.ToString()).
                Replace(Tokens.ClassName, className).
                Replace(Tokens.SchemaName, definition.Owner).
                Replace(Tokens.ProcedureName, definition.Name).
                Replace(Tokens.Properties, GenerateProcedureProperties(definition));
        }

        private static string GenerateProcedureAttribute(ISchema definition)
        {
            return string.Format("ActiveProcedure(\"{0}\", \"{1}\")", definition.Owner, definition.Name);
        }

        private static string GeneratePropertyAttribute(ProcedureParameterDefinition definition)
        {
            return string.Format("ActiveProcedureParameter(\"{0}\", DbType.{1}, {2}, ParameterType.{3}, {4})", definition.Name, definition.DbType, definition.MaxLength, definition.ParameterType, definition.IsResult.ToString().ToLower());
        }

        public string GenerateProcedureProperties(ProcedureDefinition procedure)
        {
            StringBuilder builder = new StringBuilder();
            string templateProperty = Templates.ProcedureProperty;

            foreach (ProcedureParameterDefinition parameter in procedure.Parameters)
            {
                builder.AppendLine();

                string propertyName = parameter.ToPascalCase(CodeFormatOptions.None);
                string xmlElementName = propertyName;

                if (parameter.Name == procedure.Name)
                    propertyName += "_";
                if (procedure.CharAsBooleans.ContainsKey(parameter.Name))
                {
                    builder.AppendLine(templateProperty.
                                           Replace(Tokens.SchemaAttributes, GeneratePropertyAttribute(parameter)).
                                           Replace(Tokens.XmlAttribute, xmlElementName).
                                           Replace(Tokens.DataTypeShort, TableColumnDefinition.GetDataTypeShort(DbType.Boolean, false, session.Configuration.CodeGen.UseNullableTypes)).
                                           Replace(Tokens.DataTypeLong, "CharAsBoolean").
                                           Replace("#SETOPTION#", "BooleanAsChar").
                                           Replace(Tokens.PropertyName, propertyName).
                                           Replace(Tokens.ColumnName, propertyName));
                }
                else
                {
                    builder.AppendLine(templateProperty.
                                           Replace(Tokens.SchemaAttributes, GeneratePropertyAttribute(parameter)).
                                           Replace(Tokens.XmlAttribute, xmlElementName).
                                           Replace(Tokens.DataTypeShort, parameter.GetDataTypeShort()).
                                           Replace(Tokens.DataTypeLong, parameter.GetDataTypeLong()).
                                           Replace("#SETOPTION#", string.Empty).
                                           Replace(Tokens.PropertyName, propertyName).
                                           Replace(Tokens.ColumnName, propertyName));
                }
            }

            return builder.ToString();
        }
    }
}
