using NArchitecture.Core.Application.Responses;

namespace Application.Features.Transmissions.Commands.Create;

public class CreatedTransmissionResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}