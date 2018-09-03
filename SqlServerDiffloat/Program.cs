using System;
using System.IO;
using System.Threading.Tasks;
using NLog;
using SqlServerDiffloat.Domains;
using SqlServerDiffloat.Extentions;

namespace SqlServerDiffloat
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            var path = DiffloatEnvironment.GetSQLServerDataToolsPath();

            if (string.IsNullOrEmpty(path))
            {
                logger.Error(@"Install SQL Server Data Tools (SSDT) from https://docs.microsoft.com/ja-jp/sql/ssdt/download-sql-server-data-tools-ssdt?view=sql-server-2017 .");
                return;
            }
            var actionOption = new ActionOption(args);

            if (actionOption.Action == null)
            {
                logger.Error(@"Argument 'Action' is required and value of Extract or DiffScript");
                return;
            }

        }
    }
}
