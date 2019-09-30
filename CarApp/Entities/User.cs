
using Microsoft.AspNetCore.Identity;


namespace CarApp.Entities
{
    public class User: IdentityUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Car Car { get; set; }
        public object Claims { get; internal set; }
        public object Roles { get; internal set; }
    }
}
