using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.AddControllers
    {
        [ApiController]
        [Route("api/[controller]s")]
        public class bookController : ControllerBase
        {
            private readonly BookStoreDbContext _context;
            public bookController(BookStoreDbContext context)
            {
                _context = context;
            }
        

            [HttpGet] 
            public IActionResult GetBooks()
            {
               GetBooksQuery query=new GetBooksQuery(_context);
               var result= query.Handle();
              return Ok(result);
                       
            }
          [HttpGet("{id}")] 
            public IActionResult GetById(int id)
            {
                BookDetailViewModel result;
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId =id;
                result =query.Handle();
                return Ok(result);
            }
             [HttpPost]
            public IActionResult AddBook([FromBody] CreateBookModel newBook)
            {
              
            CreateBookCommand command = new CreateBookCommand(_context);
                try{
               command.Model = newBook;
                command.Handle();
                   }

                 catch (Exception ex){
                   return BadRequest(ex.Message);
                        }

                   return Ok();
            }

            [HttpPut("{id}")]
            public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId=id;
                command.Model=updatedBook;
                command.Handle();
                return Ok();
                
            }
              [HttpDelete("{id}")]
            public IActionResult DeleteBook(int id)
            {
                var book=_context.Books.SingleOrDefault(x=>x.Id==id);
                if (book is null)
                {
                    return NotFound();
                }
                _context.Books.Remove(book);
                _context.SaveChanges();
                return Ok("Kitap Silindi");
            }


    }
    }