namespace Aliera.BusinessObjects.Auth
{
    public class PasswordBO
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string UserName { get; set; }
    }
}