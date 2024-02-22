using FluentValidation;

namespace Application.Features.Transmissions.Commands.Delete;

public class DeleteTransmissionCommandValidator : AbstractValidator<DeleteTransmissionCommand>
{
    public DeleteTransmissionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}