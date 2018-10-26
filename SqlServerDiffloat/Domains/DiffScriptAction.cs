using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SqlServerDiffloat.Extentions;

namespace SqlServerDiffloat.Domains
{
    public static class DiffScriptAction
    {
        public static readonly string SourceFileParamDisplayName = "/SourceFile";
        public static readonly string SourceDatabaseNameParamDisplayName = "/SourceDatabaseName";
        public static readonly string TargetFileParamDisplayName = "/TargetFile";
        public static readonly string TargetDatabaseNameParamDisplayName = "/TargetDatabaseName";
        public static readonly string OutputFileParamDisplayName = "/OutputFile";

        private static string SourceFileParamName =>
            SourceFileParamDisplayName.ToLower();
        private static string SourceDatabaseNameParamName =>
            SourceDatabaseNameParamDisplayName.ToLower();
        private static string TargetFileParamName =>
            TargetFileParamDisplayName.ToLower();
        private static string TargetDatabaseNameParamName =>
            TargetDatabaseNameParamDisplayName.ToLower();
        private static string OutputFileParamName =>
            OutputFileParamDisplayName.ToLower();

        public static async Task RunAsync(Dictionary<string, string> options, string sqlPackagePath, Logger logger)
        {
            if (!CheckRequiredParamters(options, new[]
            {
                SourceFileParamDisplayName,
                SourceDatabaseNameParamDisplayName,
                TargetFileParamDisplayName,
                TargetDatabaseNameParamDisplayName,
                OutputFileParamDisplayName
            }, logger))
                return;

            using (var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = sqlPackagePath,
                    Arguments = string.Join(' ', new[]
                    {
                        $"/Action:Script",
                        $"/SourceFile:{options[SourceFileParamName]}",
                        $"/SourceDatabaseName:{options[SourceDatabaseNameParamName]} ",
                        $"/TargetFile:{options[TargetFileParamName]}",
                        $"/TargetDatabaseName:{options[TargetDatabaseNameParamName]}",
                        $"/OutputPath:{options[OutputFileParamName]}"
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

                logger.Error($@"Operation DiffScript requires ""{paramDisplayName}"" parameter.");
                result = false;
            }
            return result;
        }
    }
}
