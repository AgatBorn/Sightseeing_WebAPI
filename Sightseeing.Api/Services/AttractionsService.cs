﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using Sightseeing.Application.Features.Attractions.Queries.GetAttractionDetail;
using Sightseeing.Grpc;

namespace Sightseeing.Api.Services
{
    public class AttractionsService : Attractions.AttractionsBase
    {
        private readonly IMediator _mediator;

        public AttractionsService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<AttractionDetailsResponse> GetAttractionById(AttractionDetailRequest request, ServerCallContext context)
        {
            var id = request.Id;
            var attraction = await _mediator.Send(new GetAttractionDetailQuery() { Id = new Guid(id) });

            var response = new AttractionDetailsResponse
            {
                Attraction = new AttractionDetails
                {
                    Id = attraction.AttractionId.ToString(),
                    Name = attraction.Name,
                    Author = attraction.Author,
                    Date = attraction.Date,
                    Description = attraction.Description,
                    IsFree = attraction.IsFree,
                    Price = attraction.Price ?? 0,
                    CategoryName = attraction.Category.Name,
                    CityName = attraction.City.Name
                }
            };

            return response;
        }
    }
}
