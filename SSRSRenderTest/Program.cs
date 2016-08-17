using System.Collections.Generic;
using SSRSReporting;
using System.Net;

namespace SSRSRenderTest
{
    class Program
    {
        static void Main(string[] args) 
        {
            // Report parameters defined in your .rdl files
            Dictionary<string, string> reportParameters = new Dictionary<string, string>();
            reportParameters.Add("reportParameter", "123");

            // Execute the report 
            SSRSReportClient rc = new SSRSReportClient("http://YOUR_SERVER_HERE/reportserver/reportexecution2005.asmx", "http://YOUR_SERVER_HERE/reportserver/reportservice2010.asmx", new NetworkCredential(@"user", "password"));
            byte[] toolPickPDF = rc.ExecuteReport("PATH TO REPORT ON REPORTING SERVER", "PDF", reportParameters);
        }
    }
}
