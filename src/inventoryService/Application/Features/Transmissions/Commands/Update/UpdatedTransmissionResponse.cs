using NArchitecture.Core.Application.Responses;

namespace Application.Features.Transmissions.Commands.Update;

public class UpdatedTransmissionResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}