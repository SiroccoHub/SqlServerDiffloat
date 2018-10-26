using System;
using System.IO;
using System.Threading.Tasks;
using NLog;

namespace SqlServerDiffloat
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                return await Domains.SqlServerDiffloat.Run(args, logger);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return -1;
            }

        }
    }
}
