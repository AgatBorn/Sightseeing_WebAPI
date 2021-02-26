using AutoMapper;
using Sightseeing.Application.Features.AttractionCategories.Commands.CreateAttractionCategory;
using Sightseeing.Application.Features.Attractions.Commands.CreateAttraction;
using Sightseeing.Application.Features.Attractions.Queries.GetAttractionDetail;
using Sightseeing.Application.Features.Cities.Commands.CreateCity;
using Sightseeing.Application.Features.Countries.Commands.CreateCountry;
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
            CreateMap<CreateAttractionCommand, Attraction>();
            CreateMap<Attraction, AttractionDto>().ReverseMap();
            CreateMap<Attraction, AttractionDetailVm>().ReverseMap();

            CreateMap<AttractionCategory, Features.Attractions.Queries.GetAttractionDetail.AttractionCategoryDto>().ReverseMap();
            CreateMap<CreateAttractionCategoryCommand, AttractionCategory>();
            CreateMap<AttractionCategory, Features.AttractionCategories.Commands.CreateAttractionCategory.AttractionCategoryDto>()
                .ForMember(c => c.Id, opt => opt.MapFrom(s => s.AttractionCategoryId));
            CreateMap<AttractionCategory, Features.AttractionCategories.Queries.GetAllCategories.AttractionCategoryDto>()
                .ForMember(c => c.Id, opt => opt.MapFrom(s => s.AttractionCategoryId));

            CreateMap<City, Features.Attractions.Queries.GetAttractionDetail.CityDto>().ReverseMap();
            CreateMap<CreateCityCommand, City>();
            CreateMap<City, Features.Cities.Commands.CreateCity.CityDto>();

            CreateMap<City, Features.Cities.Queries.GetAllCities.CityDto>()
                .ForMember(c => c.Id, opt => opt.MapFrom(s => s.CityId));

            CreateMap<CreateCountryCommand, Country>();
            CreateMap<Country, CountryDto>();

            CreateMap<Country, Features.Countries.Queries.GetAllCountries.CountryDto>()
                .ForMember(c => c.Id, opt => opt.MapFrom(s => s.CountryId));
        }
    }
}
