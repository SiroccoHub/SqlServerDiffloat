using System;
using System.IO;
using System.Threading.Tasks;
using NLog;
using SqlServerDiffloat.Domains;

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
                Console.ReadLine();
                return;
            }

        }
    }
}
