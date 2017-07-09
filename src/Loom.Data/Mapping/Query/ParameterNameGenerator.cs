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

using System.Collections.Specialized;

namespace Loom.Data.Mapping.Query
{
    public sealed class ParameterNameGenerator
    {
        private readonly StringDictionary parameterNameLookup = new StringDictionary();
        private readonly string uniquePrefix;

        public static ParameterNameGeneratorHandler GetLongName()
        {
            var generator = new ParameterNameGenerator("_");
            ParameterNameGeneratorHandler handler = generator.CalculateLongName;
            return handler;
        }

        public static ParameterNameGeneratorHandler GetShortName()
        {
            var generator = new ParameterNameGenerator("_");
            ParameterNameGeneratorHandler handler = generator.CalculateShortName;
            return handler;
        }

        public static ParameterNameGeneratorHandler GetLongName(string uniquePrefix)
        {
            var generator = new ParameterNameGenerator(uniquePrefix);
            ParameterNameGeneratorHandler handler = generator.CalculateLongName;
            return handler;
        }

        public static ParameterNameGeneratorHandler GetShortName(string uniquePrefix)
        {
            var generator = new ParameterNameGenerator(uniquePrefix);
            ParameterNameGeneratorHandler handler = generator.CalculateShortName;
            return handler;
        }

        private ParameterNameGenerator(string uniquePrefix)
        {
            this.uniquePrefix = uniquePrefix;
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
