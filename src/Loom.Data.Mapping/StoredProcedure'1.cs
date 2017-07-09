#region Using Directives

using System;
using System.Reflection;
using Loom.Data.Mapping.Schema;

#endregion

// ReSharper disable once StaticFieldInGenericType
namespace Loom.Data.Mapping
{
    public abstract class StoredProcedure<TStoredProcedure> : DataEntity<TStoredProcedure> where TStoredProcedure : StoredProcedure<TStoredProcedure>, new()
    {
        private static ICallable procedure;

        public static ICallable Procedure
        {
            get
            {
                LoadSchema();
                return procedure;
            }
        }

        private static void LoadSchema()
        {
            Type type = typeof(TStoredProcedure);

            ActiveProcedureAttribute procedureAttribute = (ActiveProcedureAttribute) Attribute.GetCustomAttribute(type, typeof(ActiveProcedureAttribute));
            procedure = new ProcedureData(procedureAttribute.Name, procedureAttribute.Owner);

            foreach (PropertyInfo info in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                ActiveProcedureParameterAttribute attr = (ActiveProcedureParameterAttribute) Attribute.GetCustomAttribute(info, typeof(ActiveProcedureParameterAttribute));
                procedure.Parameters.Add(new ProcedureParameterData(attr.Name, attr.DbType, attr.MaxLength, attr.ParameterType, attr.IsResult));
            }
        }

        protected static ICallableParameter CreateParameter(string name, Type type)
        {
            return SchemaParser.CreateParameter(name, type);
        }
    }
}