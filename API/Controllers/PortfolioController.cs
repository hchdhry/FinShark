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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePortfolio(string symbol)
        {
            var userName = User.getUserName();
            var appUser = await _userManger.FindByNameAsync(userName);
            var stock = await _stockRepo.getBySymbol(symbol);
            if(stock == null) return BadRequest("stock not found");

            var portfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            
            if (portfolio.Any(u=>u.Symbol.ToLower() == symbol.ToLower())) return BadRequest("stock already in portfolio");
            var portfolioModel = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id,
            };
            await _portfolioRepository.CreateAsync(portfolioModel);

            if(portfolioModel == null) return StatusCode(500,"could not create");
            else return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.getUserName();
            var appUser = await _userManger.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStock.Count() == 1)
            {
                await _portfolioRepository.DeleteAsync(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock not in your portfolio");
            }

            return Ok();
        }

    }
}