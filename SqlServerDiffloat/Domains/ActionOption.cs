using System;
using System.Collections.Generic;
using SqlServerDiffloat.Entities;
using SqlServerDiffloat.Extentions;

namespace SqlServerDiffloat.Domains
{
    public static class ActionOption
    {
        public static readonly string ActionParamDisplayName = "/Action";
        private static string ActionParamName => ActionParamDisplayName.ToLower();

        public static Actions? Analyze(Dictionary<string, string> options)
        {
            if (!options.ContainsKey(ActionParamName))
                return null;

            if (!Enum.TryParse(typeof(Actions), options[ActionParamName], true, out var inputAction))
                return null;

            return  inputAction as Actions?;
        }
    }
}