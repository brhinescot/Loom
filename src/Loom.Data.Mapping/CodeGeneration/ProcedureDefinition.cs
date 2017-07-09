#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    [Serializable]
    [DebuggerDisplay("{FullNameBracketed,nq}")]
    public sealed class ProcedureDefinition : IEquatable<ProcedureDefinition>, IComparable<ProcedureDefinition>, ISchema
    {
        private Dictionary<string, object> charAsBooleans;
        private bool isReadOnly;
        private string name;
        private string owner;
        private ProcedureParameterDefinitionCollection parameters;

        public ProcedureDefinition(string name, string owner)
        {
            this.name = name;
            this.owner = owner;
        }

        public ProcedureDefinition(string name, string owner, string datasource)
        {
            this.name = name;
            this.owner = owner;
            Datasource = datasource;
        }

        public ProcedureDefinition(string name) : this(name, "dbo") { }

        internal Dictionary<string, object> CharAsBooleans => charAsBooleans ?? (charAsBooleans = new Dictionary<string, object>());

        public ProcedureParameterDefinitionCollection Parameters => parameters ?? (parameters = new ProcedureParameterDefinitionCollection());

        #region IComparable<ProcedureDefinition> Members

        /// <summary>
        ///     Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relative order of the objects being compared.
        ///     The return value has the following meanings: Value Meaning Less than zero This object is
        ///     less than the other parameter. Zero This object is equal to other. Greater than zero This
        ///     object is greater than other.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(ProcedureDefinition other)
        {
            if (other == null)
                return 0;

            return string.Compare(string.Format("{0}.{1}", Owner, Name), string.Format("{0}.{1}", other.Owner, other.Name), StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region IEquatable<ProcedureDefinition> Members

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(ProcedureDefinition other)
        {
            // TODO: ProcedureDefinition.Equals() Implementation
            throw new NotImplementedException();
        }

        #endregion

        #region ISchema Members

        /// <summary>
        ///     Gets the data source the table belongs to.
        /// </summary>
        public string Datasource { get; }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public bool IsReadOnly
        {
            get => isReadOnly;
            set => isReadOnly = value;
        }

        public string Owner
        {
            get => owner;
            set => owner = value;
        }

        #endregion

        public string ToCamelCase()
        {
            return CodeFormat.ToCamelCase(name);
        }

        public string ToPascalCase()
        {
            return CodeFormat.ToPascalCase(name, CodeFormatOptions.None);
        }

        public string ToProperCase()
        {
            return CodeFormat.ToProperCase(name);
        }
    }
}