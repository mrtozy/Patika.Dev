using FluentValidation;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.Application.Commands.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>{
        public DeleteBookCommandValidator()
        {
            RuleFor(command=>command.BookId).GreaterThan(0);
        }
    }
}