using Microsoft.EntityFrameworkCore;
using API.Models;
using System.Security.Cryptography.X509Certificates;

namespace API.Data;

public class ApplicationDBContext:DbContext
{

public ApplicationDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
{
    

}
public DbSet<Stock> Stock {get;set;}
public DbSet<Comment> Comments {get;set;}

}
