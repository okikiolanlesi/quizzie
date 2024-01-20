using Quizzie.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Quizzie.DTOs
{
    public class RegisterAdminDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
