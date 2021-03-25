using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Attractions.Commands.DeleteAttraction
{
    public class DeleteAttractionCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
