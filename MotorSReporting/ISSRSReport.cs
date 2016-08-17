using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSRSReporting.phxssrs;

namespace SSRSReporting
{
    public interface ISSRSReport
    {
        byte[] ExecuteReport(string reportPath, string reportFormat, Dictionary<string, string> reportParameters);
        byte[] ExecuteReport(string reportPath, string reportFormat);
        Dictionary<string, string> GetAvailableReports();
    }
}
