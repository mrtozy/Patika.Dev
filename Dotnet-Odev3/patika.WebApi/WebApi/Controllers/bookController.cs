using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Commands.BookOperations.CreateBook;
using WebApi.Application.Commands.BookOperations.DeleteBook;
using WebApi.Application.Commands.BookOperations.UpdateBook;
using WebApi.Application.Queries.BookOperations.GetById;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
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
            private readonly IMapper _mapper;
        public bookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        

           [HttpGet] // GET: api/books
            public IActionResult GetBooks()
            {
                GetBooksQuery query = new GetBooksQuery(_context,_mapper);
                var result = query.Handle();
                return Ok(result);
            }

           
            [HttpGet("{id}")] // GET: api/books/id
            public IActionResult GetById(int id)
            {
                BookDetailViewModel result;
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                query.BookId =id;
                GetByIdCommandValidator validator = new GetByIdCommandValidator();
                validator.ValidateAndThrow(query);
                result =query.Handle();
                return Ok(result);
            }
             [HttpPost]
            public IActionResult AddBook([FromBody] CreateBookModel newBook)
            {
              
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
                try{
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
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
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command); 
                command.Handle();
                return Ok();
                
            }
            [HttpDelete("{id}")]
            public IActionResult DeleteBook(int id)
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId=id;
                
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok(); 
            }


    }
    }