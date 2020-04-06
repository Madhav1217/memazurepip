namespace Aliera.BusinessObjects.DMS
{
    public class LoggerSettingsBO
    {
        public string Default { get; set; }
        public bool IsError { get; set; }
        public string MinimumLevel { get; set; }
        public string MessageTemplate { get; set; }
        public string MessageTemplateForPostRequest { get; set; }
        public string MessageTemplateForGetRequest { get; set; }
        public string MessageTemplateForResponse { get; set; }
        public string FatalMessageTemplate { get; set; }
        public string VerboseMessageTemplate { get; set; }
        public bool IsGlobalDebugInformation { get; set; }
        public bool IsSqlServerLog { get; set; }
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
    }
}
