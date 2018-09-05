using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NLog;
using SqlServerDiffloat.Extentions;

namespace SqlServerDiffloat.Domains
{
    public static class ExtractAction
    {
        private static readonly string SourceConnectionStringParamName = "/SourceConnectionString".ToLower();
        private static readonly string TargetFileParamName = "/TargetFile".ToLower();

        public static async Task RunAsync(Dictionary<string, string> options, string sqlPackagePath,  Logger logger)
        {
            if (!options.ContainsKey(SourceConnectionStringParamName) || string.IsNullOrEmpty(options[SourceConnectionStringParamName]))
            {
                logger.Error(@"Operation Extract requires ""/SourceConnectionString"" parameter.");
                return;
            }

            if (!options.ContainsKey(TargetFileParamName) || string.IsNullOrEmpty(options[TargetFileParamName]))
            {
                logger.Error(@"Operation Extract requires ""/TargetFile"" parameter. ");
                return;
            }

            using (var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = sqlPackagePath,
                    Arguments =
                        $"/Action:Extract /SourceConnectionString:{options[SourceConnectionStringParamName]} /TargetFile:{options[TargetFileParamName]}",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                },
                EnableRaisingEvents = true,
            })
            {
                await process.RunAsync(logger);
            }
        }
    }
}