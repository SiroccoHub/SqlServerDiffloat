using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NLog;

namespace SqlServerDiffloat.Extentions
{
    public static class ProcessExtentions
    {
        public static Task RunAsync(this Process process, Logger logger)
        {
            var tcs = new TaskCompletionSource<object>();
            process.EnableRaisingEvents = true;
            process.Exited += (_, args) => tcs.TrySetResult(process.ExitCode);
            process.OutputDataReceived += (_, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    logger.Info(args.Data);
                }
            };
            process.ErrorDataReceived += (_, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    logger.Error(args.Data);
                }
            };

            if (!process.Start())
            {
                throw new InvalidOperationException("Could not start process: " + process);
            }

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            return tcs.Task;
        }
    }
}