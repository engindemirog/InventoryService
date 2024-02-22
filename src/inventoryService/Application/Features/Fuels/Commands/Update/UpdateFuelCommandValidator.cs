using FluentValidation;

namespace Application.Features.Fuels.Commands.Update;

public class UpdateFuelCommandValidator : AbstractValidator<UpdateFuelCommand>
{
    public UpdateFuelCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}