using DbOperationsWithEFCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DbOperationsWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController (AppDbContext _appDbContext, ILogger<LanguageController> logger) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetAllLanguagesAsync()
        {
            var result = await (from Language language in _appDbContext.Languages
                                select language).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLanguagesByIdAsync([FromRoute] int id)
        {
            try
            {
                //var result = await (from Language language in _appDbContext.Languages
                //                    select language).ToListAsync();
                var result = await _appDbContext.Languages.FindAsync(id);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                return new BadRequestObjectResult($"Database update error: {ex.Message}");
            }
            catch (SqlException ex)
            {
                logger.LogError($"SQL Erorr {ex.Message} (Error Code: {ex.Number}).");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception thrown: {ex.Message}.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:int}/{name}")]
        public async Task<IActionResult> GetLanguageByTitle([FromRoute] int id, [FromRoute] string name)
        {
            var result = await _appDbContext.Languages.FirstAsync(x => x.Id == id && x.Title == name);
            return Ok(result);
        }
    }
}
