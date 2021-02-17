using Sightseeing.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Attractions.Commands.CreateAttraction
{
    public class CreateAttractionCommandResponse : BaseResponse
    {
        public AttractionDto Attraction { get; set; }
    }
}
