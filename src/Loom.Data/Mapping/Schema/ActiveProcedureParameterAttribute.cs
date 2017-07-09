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
using System.Data;
using Loom.Dynamic;

#endregion

namespace Loom.Data.Mapping.Schema
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ActiveProcedureParameterAttribute : DynamicPropertyAttribute
    {
        #region Instance Fields

        private readonly DbType dbType;
        private readonly int maxLength;
        private readonly ParameterType parameterType;
        private readonly bool isResult;

        #endregion

        #region Property Accessors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
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

        public ActiveProcedureParameterAttribute(string name, DbType dbType, int maxLength, ParameterType parameterType, bool isResult)
        {
            Name = name;
            this.dbType = dbType;
            this.maxLength = maxLength;
            this.parameterType = parameterType;
            this.isResult = isResult;
        }
    }
}
