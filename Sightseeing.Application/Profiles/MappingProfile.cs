using AutoMapper;
using Sightseeing.Application.Features.Attractions.Commands.CreateAttraction;
using Sightseeing.Application.Features.Attractions.Queries.GetAttractionDetail;
using Sightseeing.Application.Features.Cities.Commands.CreateCity;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Attraction, CreateAttractionCommand>().ReverseMap();
            CreateMap<Attraction, AttractionDto>().ReverseMap();
            CreateMap<Attraction, AttractionDetailVm>().ReverseMap();

            CreateMap<AttractionCategory, AttractionCategoryDto>().ReverseMap();

            CreateMap<City, Features.Attractions.Queries.GetAttractionDetail.CityDto>().ReverseMap();
            CreateMap<CreateCityCommand, City>();
            CreateMap<City, Features.Cities.Commands.CreateCity.CityDto>();
        }
    }
}
