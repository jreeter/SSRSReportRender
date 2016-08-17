using System.Collections.Generic;
using System.Linq;
using System.Net;
using ReportExecution;
using ReportServices;

namespace SSRSReporting
{
    /// <summary>
    /// ReportClient deals with SSRS reporting architecture.
    /// </summary>
    public class SSRSReportClient : ISSRSReport
    {
        private string _executionAddress;
        private string _serviceAddress;
        private ICredentials _credentials;
        
        private ReportExecutionService _executionService;
        private ReportingService2010 _reportService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="executionAddress">Address of execution web service.</param>
        /// <param name="serviceAddress">Address of reporting service web service.</param>
        /// <param name="credentials">ICredentials</param>
        public SSRSReportClient(string executionAddress, string serviceAddress, ICredentials credentials)
        {
            _executionAddress = executionAddress;
            _serviceAddress = serviceAddress;
            _credentials = credentials;

            _executionService = new ReportExecutionService()
            {
                Url = _executionAddress,
                Credentials = _credentials
            };

            _reportService = new ReportingService2010()
            {
                Url = _serviceAddress,
                Credentials = _credentials
            };
        }

        /// <summary>
        /// Executes a report without parameters.
        /// </summary>
        /// <param name="reportPath">The relative report path on the reporting server website.</param>
        /// <param name="reportFormat">The format the report should be generated to: XML, NULL, CSV, IMAGE, PDF, HTML4.0, HTML3.2, MHTML, EXCEL, Word</param>
        /// <returns></returns>
        public byte[] ExecuteReport(string reportPath, string reportFormat)
        {
           string extension, encoding, mimeType;
           ReportExecution.Warning[] warnings = null;
           string[] streamIDS = null;
           _executionService.LoadReport2(reportPath, null); 
           return _executionService.Render2(reportFormat, null, PageCountMode.Estimate, out extension, out mimeType, out encoding, out warnings, out streamIDS);
        }


        /// <summary>
        /// Executes a report with parameters.
        /// </summary>
        /// <param name="reportPath">The relative report path on the reporting server website.</param>
        /// <param name="reportFormat">The format the report should be generated to: XML, NULL, CSV, IMAGE, PDF, HTML4.0, HTML3.2, MHTML, EXCEL, Word</param>
        /// <param name="parameters">The parameters needed to run the report.</param>
        /// <returns></returns>
        public byte[] ExecuteReport(string reportPath, string reportFormat, Dictionary<string,string> reportParameters)
        {
            ReportExecution.ParameterValue[] parameters = reportParameters.Select(x => new ReportExecution.ParameterValue()
            {
                Name = x.Key,
                Value = x.Value
            }).ToArray();

            string extension, encoding, mimeType;
            ReportExecution.Warning[] warnings = null;
            string[] streamIDS = null;
           
            //do the magic
            _executionService.LoadReport2(reportPath, null);
            _executionService.SetExecutionParameters(parameters, "en-us");
            return _executionService.Render2(reportFormat, null, PageCountMode.Estimate, out extension, out mimeType, out encoding, out warnings, out streamIDS);
        }

        /// <summary>
        /// Returns a Dictionary of all available reports on the server. The key is the report name and value is the report path. 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAvailableReports()
        {
            return _reportService.ListChildren("/", true).ToDictionary(key => key.Name, value => value.Path);
        }

    }
}
