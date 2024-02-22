using NArchitecture.Core.Application.Responses;

namespace Application.Features.Transmissions.Commands.Delete;

public class DeletedTransmissionResponse : IResponse
{
    public Guid Id { get; set; }
}