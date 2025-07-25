using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender Gender { get; set; }
        public string FullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
public enum Gender
{
    Male,
    Female
}
