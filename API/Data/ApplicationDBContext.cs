using Microsoft.EntityFrameworkCore;
using API.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace API.Data;

public class ApplicationDBContext:IdentityDbContext<AppUser>
{

public ApplicationDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
{
    

}
public DbSet<Stock> Stock {get;set;}
public DbSet<Comment> Comments {get;set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

       

        List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
        builder.Entity<IdentityRole>().HasData(roles);

    }
}
