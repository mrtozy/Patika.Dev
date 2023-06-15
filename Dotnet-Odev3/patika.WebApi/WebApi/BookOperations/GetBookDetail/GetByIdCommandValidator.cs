using FluentValidation;
using WebApi.BookOperations.GetBookDetail;

namespace WebApi.Application.Queries.BookOperations.GetById{
    public class GetByIdCommandValidator : AbstractValidator<GetBookDetailQuery>{
        public GetByIdCommandValidator()
        {
            RuleFor(command=>command.BookId).GreaterThan(0);
        }
    }
}