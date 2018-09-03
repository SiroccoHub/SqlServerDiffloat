using System.Collections.Generic;
using System.Linq;

namespace SqlServerDiffloat.Extentions
{
    public static class OptionParser
    {
        private static (string key, string value) SplitOption(string option)
        {
            var splits = option.Split(":");
            if (!splits.Skip(1).Any())
            {
                return (option, null);
            }

            return (splits.First().ToLower(), string.Join(":", splits.Skip(1)).Trim(new char[] { '"' }).Trim());
        }

        public static Dictionary<string, string> ConvertOptionsToDictionary(string[] options)
        {
            return options.Select(SplitOption).ToDictionary(pair => pair.key, pair=> pair.value);
        }
    }
}