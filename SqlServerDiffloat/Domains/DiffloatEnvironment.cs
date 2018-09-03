using System.IO;

namespace SqlServerDiffloat.Domains
{
    public class DiffloatEnvironment
    {
        private static readonly string BinaryFileName = @"sqlpackage.exe";

        private static readonly string SsdtVisualStudioPath =
            Path.Join(
                @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\140\",
                BinaryFileName);

        private static readonly string SsdtsqlServerPath =
            Path.Join(
            @"C:\Program Files (x86)\Microsoft SQL Server\140\DAC\bin\",
            BinaryFileName);


        public static string GetSQLServerDataToolsPath()
        {

            if (File.Exists(SsdtVisualStudioPath))
            {
                return SsdtVisualStudioPath;
            }

            if (File.Exists(SsdtsqlServerPath))
            {
                return SsdtsqlServerPath;
            }

            return string.Empty;
        }
    }
}