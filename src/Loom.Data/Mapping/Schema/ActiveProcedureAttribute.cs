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

using System;

namespace Loom.Data.Mapping.Schema
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ActiveProcedureAttribute : Attribute
    {
        private readonly string name;
        private readonly string owner;

        public string Name
        {
            get { return name; }
        }

        public string Owner
        {
            get { return owner; }
        }

        public ActiveProcedureAttribute(string owner, string name)
        {
            this.name = name;
            this.owner = owner;
        }
    }
}
