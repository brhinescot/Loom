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

using System;
using System.Collections.Generic;
using System.Reflection;

#endregion

namespace Loom.Data.Mapping.Schema
{
    internal static class SchemaParser
    {
        #region Parameters

        internal static ICallableParameter CreateParameter(string name, Type type)
        {
            PropertyInfo property = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                return null;

            ActiveProcedureParameterAttribute attr = (ActiveProcedureParameterAttribute)Attribute.GetCustomAttribute(property, typeof(ActiveProcedureParameterAttribute));
            return new ProcedureParameterData(attr.Name, attr.DbType, attr.MaxLength, attr.ParameterType, attr.IsResult);
        }

        internal static IEnumerable<ICallableParameter> CreateParameters(Type type)
        {
            foreach (PropertyInfo info in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                ActiveProcedureParameterAttribute attr = (ActiveProcedureParameterAttribute)Attribute.GetCustomAttribute(info, typeof(ActiveProcedureParameterAttribute));
                if (attr == null)
                    continue;
                
                yield return new ProcedureParameterData(attr.Name, attr.DbType, attr.MaxLength, attr.ParameterType, attr.IsResult);
            }
        }

        #endregion
    }
}
