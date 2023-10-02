
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Contexts
{
    public class AppDbContext: IdentityDbContext<IdentityUser>
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Data Source=SQL5104.site4now.net;Initial Catalog=db_a8cf6a_futuredesign;User Id=db_a8cf6a_futuredesign_admin;Password=Jhong_2499");
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Invoice>? Invoices { get; set; }
        public DbSet<InvoiceItem>? InvoiceItems { get; set; }
        public DbSet<Store>? Stores { get; set; }


    }
}
