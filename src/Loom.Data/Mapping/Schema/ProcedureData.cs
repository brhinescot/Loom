#region License Information

// ******************************************************************
// Devinterop Framework 
// 
// Copyright © 2004, 2008 by Brian Scott (DevInterop)
// All Rights Reserved
//  
// Unauthorized reproduction or distribution in source or compiled
// form is strictly prohibited.
//  
// http://www.devinterop.com
// http://blogs.geekdojo.net/brian
//  
// ******************************************************************

#endregion

namespace Loom.Data.Mapping.Schema
{
    internal sealed class ProcedureData : ICallable
    {
        #region Instance Fields

        private const string DataSource = "Default";
        private readonly string name;
        private readonly string owner;
        private ICallableParameterCollection parameters;

        #endregion

        #region .ctor

        public ProcedureData(string name, string owner)
        {
            this.name = name;
            this.owner = owner;
        }

        #endregion

        #region ICallable Members

        public ICallableParameterCollection Parameters
        {
            get { return parameters ?? (parameters = new ICallableParameterCollection()); }
        }

        public string Owner
        {
            get { return owner; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Datasource
        {
            get { return DataSource; }
        }

        public bool IsReadOnly
        {
            get { return false;}
        }

        #endregion
    }
}
