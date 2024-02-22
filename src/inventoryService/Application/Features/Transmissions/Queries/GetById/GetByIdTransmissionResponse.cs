using NArchitecture.Core.Application.Responses;

namespace Application.Features.Transmissions.Queries.GetById;

public class GetByIdTransmissionResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}