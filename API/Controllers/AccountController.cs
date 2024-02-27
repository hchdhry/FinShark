using Microsoft.AspNetCore.Mvc;
using API.interfaces;
using API.Models;
using Microsoft.AspNetCore.Identity;
using API.Dtos.Account;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers{


[Route("api/account")]
[ApiController]
public class AccountController:ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _TokenService;

    private readonly SignInManager<AppUser> _SignInManager;
    public AccountController(UserManager<AppUser> userManager,ITokenService tokenService, SignInManager<AppUser> signInManager)
    {
        _TokenService = tokenService;
        _userManager = userManager;
        _SignInManager = signInManager;
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
    [HttpPost("Login")]

    public async Task<IActionResult> LogIn(LoginDto loginDto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName);
        if(user == null)
        {
            return Unauthorized("invalid username");
        }
        var result = await _SignInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);

        if(!result.Succeeded)
        {
            return NotFound("incorrect credentials");
        }
        return Ok( new NewUserDto
        {
            UserName = user.UserName,
            Email = user.Email,
            token = _TokenService.CreateToken(user)
        });

        
    }


}
}