using API.Extensions;
using API.interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers{
[ApiController]
[Route("api/portfolio")]

public class PortfolioController:ControllerBase
{
    private readonly IStockRepository _stockRepo;
    private readonly UserManager<AppUser> _userManger;
    private readonly IPortfolioRepository _portfolioRepository;
    public PortfolioController(IStockRepository stockRepository, UserManager<AppUser> userManager, IPortfolioRepository portfolioRepository)
    {
        _stockRepo = stockRepository;
        _userManger = userManager;
        _portfolioRepository = portfolioRepository;
    }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.getUserName();
            var appUser = await _userManger.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
      
    }
}