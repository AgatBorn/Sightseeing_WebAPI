using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommand : IRequest<CreateCityCommandResponse>
    {
        public string Name { get; set; }
    }
}
