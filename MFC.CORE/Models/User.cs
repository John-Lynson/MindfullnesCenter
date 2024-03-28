namespace MFC.CORE.Models
{
    public class User
    {
        public string Id { get; set; } // Auth0 User ID
        public string Email { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; } // Enum voor gebruikersrollen
        public DateTime RegistrationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }

    public enum UserRole
    {
        BasicUser,
        Coach,
        Admin
    }
}