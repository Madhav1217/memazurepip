namespace Aliera.BusinessObjects.Auth
{
    public class ClientBO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public int PortalId { get; set; }
    }
}