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
    private readonly IFMPService _fMPService;
    public PortfolioController(IStockRepository stockRepository, UserManager<AppUser> userManager, IPortfolioRepository portfolioRepository, IFMPService fMPService)
    {
        _stockRepo = stockRepository;
        _userManger = userManager;
        _portfolioRepository = portfolioRepository;
        _fMPService = fMPService;
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
            var username = User.getUserName();
            var appUser = await _userManger.FindByNameAsync(username);
            var stock = await _stockRepo.getBySymbol(symbol);

            if (stock == null)
            {
                stock = await _fMPService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _stockRepo.CreateAsync(stock);
                }
            }

            if (stock == null) return BadRequest("Stock not found");

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };

            await _portfolioRepository.CreateAsync(portfolioModel);

            if (portfolioModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
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