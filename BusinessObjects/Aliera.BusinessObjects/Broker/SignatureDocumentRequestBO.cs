using System;

namespace Aliera.BusinessObjects.Broker
{
    public class SignatureDocumentRequestBO
    {
        public string FileName { get; set; }
        public int MemberId { get; set; }
        public DateTime SignedDate { get; set; }
        public string Html { get; set; }
        public string DocumentPath { get; set; }
        public string Browser { get; set; }
        public string IPAddress { get; set; }
        public long BrokerId { get; set; }
    }
}