using Sightseeing.Application.Responses;

namespace Sightseeing.Application.Features.Countries.Commands
{
    public class CreateCountryCommandResponse : BaseResponse
    {
        public CountryDto Country { get; set; }
    }
}