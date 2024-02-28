using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace API.Models{


public class AppUser:IdentityUser
{
    public List<Portfolio> portfolios {get; set;} = new List<Portfolio>();

}
}