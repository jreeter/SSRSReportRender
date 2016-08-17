using System.Collections.Generic;

namespace SSRSReporting
{
    public interface ISSRSReport
    {
        byte[] ExecuteReport(string reportPath, string reportFormat, Dictionary<string, string> reportParameters);
        byte[] ExecuteReport(string reportPath, string reportFormat);
        Dictionary<string, string> GetAvailableReports();
    }
}
