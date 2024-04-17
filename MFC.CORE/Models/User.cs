using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MFC.CORE.Models
{
    [Table("Users")]  
    public class User
    {
        [Key]
        [Column("UserId")]
        public string Id { get; set; } 

        [Column("EmailAddress")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Column("FullName")]
        public string Name { get; set; }

        [Column("UserRole")]
        public UserRole Role { get; set; }

        [Column("RegistrationDate")]
        public DateTime RegistrationDate { get; set; }

        [Column("LastLoginDate")]
        public DateTime? LastLoginDate { get; set; }

        [Column("Auth0UserId")]
        public string Auth0UserId { get; set; } // Field for Auth0 User ID integration
    }

    public enum UserRole
    {
        BasicUser,
        Coach,
        Admin
    }
}
