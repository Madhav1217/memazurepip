using Microsoft.AspNetCore.Http;
using System;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerDocumentBO
    {
        public long DocumentId { get; set; }
        public int BrokerDocumentId { get; set; }
        public int BrokerId { get; set; }
        public DateTimeOffset DocumentCreatedDate { get; set; }
        public int DocumentCategoryId { get; set; }
        public string DisplayDocumentName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentLabel { get; set; }
        public string DocumentNotes { get; set; }
        public string FolderName { get; set; }
        public bool IsSecuredDocument { get; set; }
        public string DocumentType { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FileExtension { get; set; }
        public string DocumentCategoryName { get; set; }
        public string TokenNumber { get; set; }
        public int ResumableChunkNumber { get; set; }
        public string ResumableFilename { get; set; }
        public string ResumableIdentifier { get; set; }
        public int ResumableChunkSize { get; set; }
        public long ResumableTotalSize { get; set; }
        public IFormFileCollection FileCollection { get; set; }
    }
}
