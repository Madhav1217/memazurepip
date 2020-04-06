namespace Aliera.BusinessObjects.Broker
{
    public class DocumentCategoryBO 
    {
        public int DocumentCategoryId { get; set; }
        public string DocumentCategoryName { get; set; }
        public int DocumentCategoryType { set; get; }           
        public int DocumentsAssociated { set; get; }
    }

}
