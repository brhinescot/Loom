#region Using Directives

using System;

#endregion

namespace Loom.Data.Mapping.Query
{
    public struct ParameterNames : IEquatable<ParameterNames>
    {
        public static readonly ParameterNames Empty = new ParameterNames(null, null);

        public string Parameter1 { get; }

        public string Parameter2 { get; }

        public ParameterNames(string parameter1, string parameter2)
        {
            Parameter1 = parameter1;
            Parameter2 = parameter2;
        }

        public static bool operator !=(ParameterNames parameterNames1, ParameterNames parameterNames2)
        {
            return !parameterNames1.Equals(parameterNames2);
        }

        public static bool operator ==(ParameterNames parameterNames1, ParameterNames parameterNames2)
        {
            return parameterNames1.Equals(parameterNames2);
        }

        public bool Equals(ParameterNames parameterNames)
        {
            return Equals(Parameter1, parameterNames.Parameter1) && Equals(Parameter2, parameterNames.Parameter2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ParameterNames))
                return false;
            return Equals((ParameterNames) obj);
        }

        public override int GetHashCode()
        {
            return (Parameter1 != null ? Parameter1.GetHashCode() : 0) + 29 * (Parameter2 != null ? Parameter2.GetHashCode() : 0);
        }
    }
}