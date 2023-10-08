using Microsoft.AspNetCore.Identity;

namespace ContactBookAPI.Model
{
    public class User : IdentityUser
    {
        public string ImageURL { get; set; }
    }
}
