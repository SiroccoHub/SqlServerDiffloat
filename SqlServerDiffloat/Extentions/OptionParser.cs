using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlServerDiffloat.Extentions
{
    public static class OptionParser
    {
        private static (string key, string value) SplitOption(string option)
        {
            var colonIndex = option.IndexOf(":", StringComparison.Ordinal);
            if (colonIndex == -1)
            {
                return (option.Trim(new char[] { '"' }).Trim(), null);
            }

            var key = option.Substring(0, colonIndex).ToLower();
            if (key[0] != '/')
            {
                return (option.Trim(new char[] { '"' }).Trim(), null);
            }

            return (key, option.Substring(colonIndex + 1).Trim(new char[] { '"' }).Trim());
        }

        public static Dictionary<string, string> ConvertOptionsToDictionary(string[] options)
        {
            return options.Select(SplitOption).ToDictionary(pair => pair.key, pair=> pair.value);
        }
    }
}