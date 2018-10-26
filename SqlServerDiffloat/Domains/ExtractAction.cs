using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NLog;
using SqlServerDiffloat.Extentions;

namespace SqlServerDiffloat.Domains
{
    public static class ExtractAction
    {
        public static readonly string SourceConnectionStringParamDisplayName = "/SourceConnectionString";
        public static readonly string TargetFileParamDisplayName = "/TargetFile";

        private static string SourceConnectionStringParamName =>
            SourceConnectionStringParamDisplayName.ToLower();

        private static string TargetFileParamName =>
            TargetFileParamDisplayName.ToLower();

        public static async Task RunAsync(Dictionary<string, string> options, string sqlPackagePath,  Logger logger)
        {
            if (!options.ContainsKey(SourceConnectionStringParamName) || string.IsNullOrEmpty(options[SourceConnectionStringParamName]))
            {
                logger.Error($@"Operation Extract requires ""{SourceConnectionStringParamDisplayName}"" parameter.");
                return;
            }

            if (!options.ContainsKey(TargetFileParamName) || string.IsNullOrEmpty(options[TargetFileParamName]))
            {
                logger.Error($@"Operation Extract requires ""{TargetFileParamDisplayName}"" parameter. ");
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