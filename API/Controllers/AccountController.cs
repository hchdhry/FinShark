using Microsoft.AspNetCore.Mvc;
using API.interfaces;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers{


[Route("api/account")]
[ApiController]
public class AccountController:ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _TokenService;
    public AccountController(UserManager<AppUser> userManager,ITokenService tokenService)
    {
        _TokenService = tokenService;
        _userManager = userManager;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try{

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var AppUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };
            var createdUser = await _userManager.CreateAsync(AppUser,registerDto.Password);
            if(createdUser.Succeeded){
                var role = await _userManager.AddToRoleAsync(AppUser,"User");
                    if(role.Succeeded)
                    {
                        return Ok(new NewUserDto{
                            UserName = AppUser.UserName,
                            Email = AppUser.Email,
                            token = _TokenService.CreateToken(AppUser)
                        });
                    }
                    else{return BadRequest();}
            }
            else{return StatusCode(500,createdUser.Errors);}
            
        }
        catch(Exception e){return StatusCode(500,e);}
    }


}
}