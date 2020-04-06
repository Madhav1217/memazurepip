using System;

namespace Aliera.BusinessObjects.Member
{
    public class AppSettingsBO
    {
        public int MaxDegreeOfParallelism { get; set; }
        public string DmsAPIUrl { get; set; }
        public string TemplateFilePath { get; set; }
        public string TemplateWithDataFilePath { get; set; }
        public string PdfFilePath { get; set; }
        public string HtmlFilePath { get; set; }
        public string SFTPFilePath { get; set; }
        public int JobStatus { get; set; }
        public int FailedAttemptCount { get; set; }
        public string BaseURL { get; set; }
        public string TriggerQueueFilePath { get; set; }
        public string HRPSFTPFilePath { get; set; }
        public string HRPTriggeredLoopBackFileName { get; set; }
        public string HRPTriggeredLoopBackFilePath { get; set; }
        public string PortalTriggeredLoopBackFileName { get; set; }
        public int RetryAttempts { get; set; }
        public int AdhocRuleType { get; set; }
        public string ArchivedDirectoryPath { get; set; }
        public DateTime StartDate { get; set; }
        public int RuleDuration { get; set; }
        public bool EnableStopWatch { get; set; }
        public int DefaultLanguage { get; set; }
    }
}
