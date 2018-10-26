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

        public static async Task RunAsync(Dictionary<string, string> options, string sqlPackagePath, Logger logger)
        {
            if (!CheckRequiredParamters(options, new[]
            {
                SourceConnectionStringParamDisplayName,
                TargetFileParamDisplayName
            }, logger))
                return;

            using (var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = sqlPackagePath,
                    Arguments = string.Join(' ', new[]
                    {
                        $"/Action:Extract",
                        $"/SourceConnectionString:{options[SourceConnectionStringParamName]}",
                        $"/TargetFile:{options[TargetFileParamName]}"
                    }),
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

        private static bool CheckRequiredParamters(Dictionary<string, string> options, IEnumerable<string> requiredParamDisplayNames, Logger logger)
        {
            var result = true;
            foreach (var paramDisplayName in requiredParamDisplayNames)
            {
                if (options.ContainsKey(paramDisplayName.ToLower()) && !string.IsNullOrEmpty(options[paramDisplayName.ToLower()]))
                    continue;

                logger.Error($@"Operation Extract requires ""{paramDisplayName}"" parameter.");
                result = false;
            }
            return result;
        }
    }
}