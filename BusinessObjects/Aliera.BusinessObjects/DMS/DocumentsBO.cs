namespace Aliera.BusinessObjects.DMS
{
    public class DocumentsBO : DocumentMapBO
    {
        private byte[] fileContent;
        public byte[] FileContent
        {
            get
            {
                if (fileContent != null)
                {
                    return (byte[])fileContent.Clone();
                }
                else
                    return null;
            }
            set { fileContent = value; }
        }
    }  
}
