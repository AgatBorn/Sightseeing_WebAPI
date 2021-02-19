using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Countries.Commands
{
    public class CreateCountryCommand : IRequest<CreateCountryCommandResponse>
    {
        public string Name { get; set; }
    }
}
