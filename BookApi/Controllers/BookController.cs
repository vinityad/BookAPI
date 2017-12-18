using AutoMapper;
using BookApi.Data.Entity;
using BookApi.Data.Infrastracture;
using BookApi.Data.Model;
using BookApi.Data.Repository;
using BookApi.Models;
using JsonPatch;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookApi.Controllers
{
    /// <summary>
    /// Book controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class BookController : ApiController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookController" /> class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public BookController(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Returns a list of book
        /// </summary>
        /// <returns>List of books.</returns>
        [Route("books")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(BookDTO[]))]
        public async Task<IEnumerable<BookDTO>> Get()
        {
            return await _bookRepository.GetAsync();
        }

        /// <summary>
        /// Returns the details of a single book.
        /// </summary>
        /// <param name="bookId">Book unique Identifier.</param>
        [Route("books/{bookId}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(BookDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid book id.", typeof(Models.Error))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Book not found", typeof(Models.Error))]
        public async Task<IHttpActionResult> Get(string bookId)
        {
            Guid bookUniqueIdentifier;
            if (!Guid.TryParse(bookId, out bookUniqueIdentifier))
                return Content<Error>(HttpStatusCode.BadRequest, new Error("400", $"Invalid book id supplied. Book id: {bookId}."));

            var book = await _bookRepository.GetAsync(bookId);
            if (book == null)
                return Content<Error>(HttpStatusCode.NotFound, new Error("404", $"Cannot find book with id {bookId}."));
            return Ok(book);
        }

        /// <summary>
        /// Add a new book.
        /// </summary>
        /// <param name="book">Book details.</param>
        [Route("books")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Validation failed")]
        public async Task<IHttpActionResult> Post([FromBody]Models.BookModel book)
        {
            // We can even use Fluent validation here for better validation, but for demo app, simple model validation will work.
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert DTO to Entity.
            Book entity = Mapper.Map<Book>(book);

            // Save entity in db, can also check GUID is unique or not, because GUID is not cryptographically unique, for now it is fine.
            _bookRepository.Insert(entity);
            await _unitOfWork.SaveChangesAsync();

            // If we comes here, means Success
            return Created($"/books/{entity.BookId}", Mapper.Map<BookDTO>(entity));
        }

        /// <summary>
        /// Deletes the specified book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        [Route("books/{bookId}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(BookDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid book id.", typeof(Models.Error))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Book not found", typeof(Models.Error))]
        public async Task<IHttpActionResult> Delete(string bookId)
        {
            Guid bookUniqueIdentifier;
            if (!Guid.TryParse(bookId, out bookUniqueIdentifier))
                return Content<Error>(HttpStatusCode.BadRequest, new Error("400", $"Invalid book id supplied. Book id: {bookId}."));

            var book = await _bookRepository.GetAsync(bookId);
            if (book == null)
                return Content<Error>(HttpStatusCode.NotFound, new Error("404", $"Cannot find book with id {bookId}."));

            _bookRepository.Delete(book);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Selectively update book details. (Make sure to use content type application/json-patch+json)
        /// </summary>
        [Route("books/{bookId}")]
        [HttpPatch]
        [SwaggerResponse(HttpStatusCode.Created, "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid book id.", typeof(Models.Error))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Book not found", typeof(Models.Error))]
        [SwaggerResponseContentType(responseType: "application/json-patch+json", Exclusive = true)]
        public async Task<IHttpActionResult> Patch(string bookId, JsonPatchDocument<BookModel> patch)
        {
            Guid bookUniqueIdentifier;
            if (!Guid.TryParse(bookId, out bookUniqueIdentifier))
                return Content<Error>(HttpStatusCode.BadRequest, new Error("400", $"Invalid book id supplied. Book id: {bookId}."));

            var book = await _bookRepository.GetByIDAsync(bookId);
            if (book == null)
                return Content<Error>(HttpStatusCode.NotFound, new Error("404", $"Cannot find book with id {bookId}."));

            BookModel originalModel = Mapper.Map<BookModel>(book);
            patch.ApplyUpdatesTo(originalModel);

            // Apply changes back to book object and save.
            book.Name = originalModel.Name;
            book.DateOfPublication = originalModel.DateOfPublication;
            book.Authors = string.Join(",", originalModel.Authors);
            book.NumberOfPages = originalModel.NumberOfPages;

            await _bookRepository.UpdateAsync(book);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
