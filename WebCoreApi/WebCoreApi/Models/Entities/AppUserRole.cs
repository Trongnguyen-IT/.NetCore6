using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreApi.Models
{
    public class AppUserRole:IdentityUserRole<int>
    {
        [ForeignKey(nameof(RoleId))]
        public virtual AppRole Role { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; }

    }
}
