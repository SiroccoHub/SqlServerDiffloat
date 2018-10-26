using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SqlServerDiffloat.Entities;
using SqlServerDiffloat.Extentions;

namespace SqlServerDiffloat.Domains
{
    public static class SqlServerDiffloat
    {
        public static async Task<int> Run(string[] args, Logger logger)
        {
            var sqlPackagePath = DiffloatEnvironment.GetSqlPackagePath();

            if (string.IsNullOrEmpty(sqlPackagePath))
            {
                logger.Error(@"Install SQL Server Data Tools (SSDT) from https://docs.microsoft.com/ja-jp/sql/ssdt/download-sql-server-data-tools-ssdt .");
                return -1;
            }

            var options = OptionParser.ConvertOptionsToDictionary(args);
            var action = ActionOption.Analyze(options);
            if (action == null)
            {
                logger.Error($@"Argument '{ActionOption.ActionParamDisplayName}' is required and value of Extract or DiffScript.");
                return -1;
            }

            switch (action.Value)
            {
                case Actions.Extract:
                    await ExtractAction.RunAsync(options, sqlPackagePath, logger);
                    break;
                case Actions.DiffScript:
                    break;
                default:
                    logger.Error(@"This Action is not implemented.");
                    return -1;
            }

            return 0;

        }
    }
}
