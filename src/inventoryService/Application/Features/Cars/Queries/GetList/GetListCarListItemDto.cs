using NArchitecture.Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.Cars.Queries.GetList;

public class GetListCarListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ModelId { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindexScore { get; set; }
    public CarState CarState { get; set; }
}