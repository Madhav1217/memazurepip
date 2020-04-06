using System;

namespace Aliera.BusinessObjects.DMS
{
    public class AppSettings
    {
        public Int64 AlieraBrokerId { get; set; }
        public string LowerIPAddress { get; set; }
        public string UpperIPAddress { get; set; }
        public string BrokerDocumentPath { get; set; }
        public string S3BucketName { get; set; }
    }
}
