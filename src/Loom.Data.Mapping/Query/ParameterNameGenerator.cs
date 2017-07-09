#region Using Directives

using System.Collections.Specialized;

#endregion

namespace Loom.Data.Mapping.Query
{
    public sealed class ParameterNameGenerator
    {
        private readonly StringDictionary parameterNameLookup = new StringDictionary();
        private readonly string uniquePrefix;

        private ParameterNameGenerator(string uniquePrefix)
        {
            this.uniquePrefix = uniquePrefix;
        }

        public static ParameterNameGeneratorHandler GetLongName()
        {
            ParameterNameGenerator generator = new ParameterNameGenerator("_");
            ParameterNameGeneratorHandler handler = generator.CalculateLongName;
            return handler;
        }

        public static ParameterNameGeneratorHandler GetShortName()
        {
            ParameterNameGenerator generator = new ParameterNameGenerator("_");
            ParameterNameGeneratorHandler handler = generator.CalculateShortName;
            return handler;
        }

        public static ParameterNameGeneratorHandler GetLongName(string uniquePrefix)
        {
            ParameterNameGenerator generator = new ParameterNameGenerator(uniquePrefix);
            ParameterNameGeneratorHandler handler = generator.CalculateLongName;
            return handler;
        }

        public static ParameterNameGeneratorHandler GetShortName(string uniquePrefix)
        {
            ParameterNameGenerator generator = new ParameterNameGenerator(uniquePrefix);
            ParameterNameGeneratorHandler handler = generator.CalculateShortName;
            return handler;
        }

        private string CalculateLongName(string columnName)
        {
            string parameterName = uniquePrefix + columnName;
            if (parameterNameLookup.ContainsKey(parameterName))
                parameterName += parameterNameLookup.Count;
            parameterNameLookup.Add(parameterName, null);
            return parameterName;
        }

        private string CalculateShortName(string columnName)
        {
            string parameterName = uniquePrefix + "p" + parameterNameLookup.Count;
            if (parameterNameLookup.ContainsKey(parameterName))
                parameterName += parameterNameLookup.Count;
            parameterNameLookup.Add(parameterName, null);
            return parameterName;
        }
    }
}