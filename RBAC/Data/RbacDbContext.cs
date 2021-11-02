using Mail.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Mail.Data
{
    public class RbacDbContext : IdentityDbContext<ApplicationUser>
    {
        
    }
}