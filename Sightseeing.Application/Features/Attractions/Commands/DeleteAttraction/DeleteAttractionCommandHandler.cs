using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Exceptions;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Attractions.Commands.DeleteAttraction
{
    public class DeleteAttractionCommandHandler : IRequestHandler<DeleteAttractionCommand>
    {
        private readonly IAttractionRepository _attractionRepository;

        public DeleteAttractionCommandHandler(IAttractionRepository attractionRepository)
        {
            _attractionRepository = attractionRepository;
        }

        public async Task<Unit> Handle(DeleteAttractionCommand request, CancellationToken cancellationToken)
        {
            var attraction = await _attractionRepository.GetByIdAsync(request.Id);

            if (attraction == null)
            {
                throw new NotFoundException(nameof(Attraction), request.Id.ToString());
            }

            await _attractionRepository.DeleteAsync(attraction);

            return Unit.Value;
        }
    }
}
