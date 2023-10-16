using Microsoft.AspNetCore.Identity;

namespace WebCoreApi.Models
{
    public class AppRole : IdentityRole<int>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
