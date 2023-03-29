using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheBugTracker.Models;

namespace TheBugTracker.Data
{
    //ApplicationDbContext extends IdentityDbContext and supplied IdentityDbContex with type parameter of BTUser
    public class ApplicationDbContext : IdentityDbContext<BTUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}