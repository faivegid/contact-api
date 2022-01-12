using Contact.Models.DomainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Contact.Data
{
    public class DataContext : IdentityDbContext<UserContact>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<UserContact> Users { get; set; }
    }
}
