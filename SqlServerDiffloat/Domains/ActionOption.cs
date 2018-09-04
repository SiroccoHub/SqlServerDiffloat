using System;
using System.Collections.Generic;
using SqlServerDiffloat.Entities;
using SqlServerDiffloat.Extentions;

namespace SqlServerDiffloat.Domains
{
    public class ActionOption
    {
        public readonly Actions? Action = null;

        public ActionOption(Dictionary<string, string> options)
        {
            if (!options.ContainsKey("/action"))
            {
                Action = null;
                return;
            }

            if (!Enum.TryParse(typeof(Actions), options["/action"], true, out var inputAction))
            {
                return;
            }

            Action = inputAction as Actions?;
        }


    }
}