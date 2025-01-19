using DbOperationsWithEFCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(AppDbContext _appDbContext, ILogger<BooksController> logger) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody] Book model)
        {
            try
            {
                #region Adding Author explicitly for all requests
                //var author = new Author
                //{
                //    Name = "Author 1",
                //    Email = "author1@mail.com"
                //};
                //model.Author = author;
                #endregion
                _appDbContext.Books.Add(model);
                await _appDbContext.SaveChangesAsync();
                return Ok(model);
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

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBooks([FromBody] List<Book> model)
        {
            try
            {
                _appDbContext.Books.AddRange(model);
                await _appDbContext.SaveChangesAsync();

                return Ok(model);
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

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int bookId, [FromBody] Book model)
        {
            var book = await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == bookId);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = model.Title;
            book.Description = model.Description;
            book.AuthorId = model.Author.Id;
            
            await _appDbContext.SaveChangesAsync();
            return Ok(model);
        }
    }
}
