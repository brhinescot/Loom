using System;

namespace Loom.Data.Mapping.Query
{
    public struct ParameterNames : IEquatable<ParameterNames>
    {
        private readonly string parameter1;
        private readonly string parameter2;

        public static readonly ParameterNames Empty = new ParameterNames(null, null);

        public string Parameter1
        {
            get { return parameter1; }
        }

        public string Parameter2
        {
            get { return parameter2; }
        }

        public ParameterNames(string parameter1, string parameter2)
        {
            this.parameter1 = parameter1;
            this.parameter2 = parameter2;
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
            return Equals(parameter1, parameterNames.parameter1) && Equals(parameter2, parameterNames.parameter2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ParameterNames))
                return false;
            return Equals((ParameterNames) obj);
        }

        public override int GetHashCode()
        {
            return (parameter1 != null ? parameter1.GetHashCode() : 0) + 29*(parameter2 != null ? parameter2.GetHashCode() : 0);
        }
    }
}
