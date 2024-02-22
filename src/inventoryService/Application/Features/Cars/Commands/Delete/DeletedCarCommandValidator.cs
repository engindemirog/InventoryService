using FluentValidation;

namespace Application.Features.Cars.Commands.Delete;

public class DeleteCarCommandValidator : AbstractValidator<DeleteCarCommand>
{
    public DeleteCarCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}