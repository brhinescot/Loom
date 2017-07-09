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
    public sealed class ActiveTableAttribute : Attribute
    {
        #region Instance Fields

        private readonly string name;
        private readonly string owner;
        private string datasource;
        private readonly string keyColumn;
        private string deletedColumn;
        private string createdOnColumn;
        private string modifiedOnColumn;
        private string deletedByColumn;
        private string createdByColumn;
        private string modifiedByColumn;
        private bool readOnly;

        #endregion

        #region Property Accessors

        public string Name
        {
            get { return name; }
        }

        public string Owner
        {
            get { return owner; }
        }

        public string Datasource
        {
            get { return datasource; }
            set { datasource = value; }
        }

        public string KeyColumn
        {
            get { return keyColumn; }
        }

        public string DeletedColumn
        {
            get { return deletedColumn; }
            set { deletedColumn = value; }
        }

        public string CreatedOnColumn
        {
            get { return createdOnColumn; }
            set { createdOnColumn = value; }
        }

        public string ModifiedOnColumn
        {
            get { return modifiedOnColumn; }
            set { modifiedOnColumn = value; }
        }

        public string DeletedByColumn
        {
            get { return deletedByColumn; }
            set { deletedByColumn = value; }
        }

        public string CreatedByColumn
        {
            get { return createdByColumn; }
            set { createdByColumn = value; }
        }

        public string ModifiedByColumn
        {
            get { return modifiedByColumn; }
            set { modifiedByColumn = value; }
        }

        public bool ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; }
        }

        #endregion

        public ActiveTableAttribute(string owner, string name) : this(owner, name, null) {}

        public ActiveTableAttribute(string owner, string name, string keyColumn)
        {
            this.name = string.Intern(name);
            this.owner = string.Intern(owner);
            this.keyColumn = keyColumn;
        }
    }
}
