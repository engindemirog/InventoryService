using FluentValidation;

namespace Application.Features.Transmissions.Commands.Update;

public class UpdateTransmissionCommandValidator : AbstractValidator<UpdateTransmissionCommand>
{
    public UpdateTransmissionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}