using FluentValidation;

namespace Application.Features.Cars.Commands.Update;

public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
{
    public UpdateCarCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ModelId).NotEmpty();
        RuleFor(c => c.Kilometer).NotEmpty();
        RuleFor(c => c.ModelYear).NotEmpty();
        RuleFor(c => c.Plate).NotEmpty();
        RuleFor(c => c.MinFindexScore).NotEmpty();
        RuleFor(c => c.CarState).NotEmpty();
    }
}