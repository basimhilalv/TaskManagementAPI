namespace TaskManagementAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
    }
}
