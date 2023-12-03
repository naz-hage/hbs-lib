using System.Diagnostics;
using System.Reflection;

namespace homelib.Helper
{
    public class AssemblyInformation
    {
        public static string Get()
        {
            var fileVersionInfo = FileVersionInfo.
                                                GetVersionInfo(Assembly.
                                                                GetExecutingAssembly().
                                                                Location);

            return $" *** {fileVersionInfo.FileDescription}, " +
                            $"{fileVersionInfo.ProductName}, " +
                            $"{fileVersionInfo.CompanyName}, " +
                            $"{fileVersionInfo.LegalCopyright} -" +
                            $" Version: {fileVersionInfo.FileVersion}";
        }
    }
}
