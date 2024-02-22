using NArchitecture.Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Cars.Queries.GetById;

public class GetByIdCarResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ModelId { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindexScore { get; set; }
    public CarState CarState { get; set; }
}