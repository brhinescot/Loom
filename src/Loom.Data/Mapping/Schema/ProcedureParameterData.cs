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

using System.Collections.ObjectModel;
using System.Data;

namespace Loom.Data.Mapping.Schema
{
    internal sealed class ProcedureParameterData : ICallableParameter
    {
        #region Member Fields

        private readonly string name;
        private readonly DbType dbType;
        private readonly int maxLength;
        private readonly ParameterType parameterType;
        private readonly bool isResult;

        #endregion

        #region Property Accessors

        public string Name
        {
            get { return name; }
        }

        public DbType DbType
        {
            get { return dbType; }
        }

        public int MaxLength
        {
            get { return maxLength; }
        }

        public ParameterType ParameterType
        {
            get { return parameterType; }
        }

        public bool IsResult
        {
            get { return isResult; }
        }

        #endregion

        public ProcedureParameterData(string name, DbType dbType, int maxLength, ParameterType parameterType, bool isResult)
        {
            this.name = name;
            this.dbType = dbType;
            this.maxLength = maxLength;
            this.parameterType = parameterType;
            this.isResult = isResult;
        }
    }


    public class ICallableParameterCollection : Collection<ICallableParameter>{}
}
