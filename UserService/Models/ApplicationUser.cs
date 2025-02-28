using Microsoft.AspNetCore.Identity;

namespace UserService.Models
{
    public class ApplicationUser : IdentityUser
    {
        /*This inherits from IdentityUser, which includes:
        Id, Email, PasswordHash, PhoneNumber, etc.
        Custom property FullName for additional user details.*/

        public required string FullName { get; set; }
    }
}
