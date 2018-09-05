using System;
using System.IO;
using System.Threading.Tasks;
using NLog;
using SqlServerDiffloat.Domains;
using SqlServerDiffloat.Entities;
using SqlServerDiffloat.Extentions;

namespace SqlServerDiffloat
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            var sqlPackagePath = DiffloatEnvironment.GetSqlPackagePath();

            if (string.IsNullOrEmpty(sqlPackagePath))
            {
                logger.Error(@"Install SQL Server Data Tools (SSDT) from https://docs.microsoft.com/ja-jp/sql/ssdt/download-sql-server-data-tools-ssdt?view=sql-server-2017 .");
                return;
            }

            var options = OptionParser.ConvertOptionsToDictionary(args);
            var actionOption = new ActionOption(options);
            if (actionOption.Action == null)
            {
                logger.Error(@"Argument 'Action' is required and value of Extract or DiffScript");
                return;
            }

            switch (actionOption.Action.Value)
            {
                case Actions.Extract:
                    await ExtractAction.RunAsync(options, sqlPackagePath, logger);
                    break;
                case Actions.DiffScript:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
