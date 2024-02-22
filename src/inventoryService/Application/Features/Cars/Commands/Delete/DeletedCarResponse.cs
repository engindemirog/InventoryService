using NArchitecture.Core.Application.Responses;

namespace Application.Features.Cars.Commands.Delete;

public class DeletedCarResponse : IResponse
{
    public Guid Id { get; set; }
}