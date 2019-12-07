using Microsoft.AspNetCore.Identity;

namespace Data.Entityes
{
   public class User : IdentityUser
    {
        public string Comment { get; set; }
    }
}
