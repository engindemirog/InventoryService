using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Transmissions.Queries.GetList;

public class GetListTransmissionListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}