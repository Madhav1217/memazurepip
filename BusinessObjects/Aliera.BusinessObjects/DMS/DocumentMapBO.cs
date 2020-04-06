using System;

namespace Aliera.BusinessObjects.DMS
{
    public class DocumentMapBO
    {
        public Int64 DocumentId { get; set; }
        public int DocumentTypeId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public int DocumentStatusId { get; set; }
        public int OwnerTypeId { get; set; }
        public Int64 OwnerId { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsSecuredDocument { get; set; }
        public DateTime? DocumentCreatedDate { get; set; }
        public string DocumentId123 { get; set; }
        public string Url123 { get; set; }
        public bool IsUploadedS3123 { get; set; }
    }
}
