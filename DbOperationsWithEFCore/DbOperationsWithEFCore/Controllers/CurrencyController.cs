using DbOperationsWithEFCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var result = await (from currencies in _appDbContext.Currencies
                         select currencies).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCurrenciesByIdAsync([FromRoute] int id)
        {
            //var result = await (from currencies in _appDbContext.Currencies
            //                    select currencies).ToListAsync();
            var result = await _appDbContext.Currencies.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCurrencyByTitleAsync([FromRoute] string name)
        {
            var result = await _appDbContext.Currencies.FirstAsync(x => x.Title == name);
            return Ok(result);
        }

         [HttpPost("all")]
        public async Task<IActionResult> GetCurrencyByIdAsync([FromBody] List<int> ids)
        {
            //var ids = new List<int> { 1};
            var result = await _appDbContext.Currencies
                .Where( x => ids.Contains(x.Id))
                .Select(x => new Currency()
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .ToListAsync();
            return Ok(result);
        }
    }
}
