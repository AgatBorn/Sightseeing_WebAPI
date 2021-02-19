using Sightseeing.Application.Responses;

namespace Sightseeing.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandResponse : BaseResponse
    {
        public CityDto City { get; set; }
    }
}