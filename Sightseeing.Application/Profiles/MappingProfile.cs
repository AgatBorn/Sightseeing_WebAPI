using AutoMapper;
using Sightseeing.Application.Features.AttractionCategories.Commands.CreateAttractionCategory;
using Sightseeing.Application.Features.AttractionCategories.Queries.GetAllCategories;
using Sightseeing.Application.Features.AttractionCategories.Queries.GetAttractionCategoryDetail;
using Sightseeing.Application.Features.Attractions.Commands.CreateAttraction;
using Sightseeing.Application.Features.Attractions.Queries.GetAllAtractions;
using Sightseeing.Application.Features.Attractions.Queries.GetAttractionDetail;
using Sightseeing.Application.Features.Cities.Commands.CreateCity;
using Sightseeing.Application.Features.Cities.Queries.GetAllCities;
using Sightseeing.Application.Features.Cities.Queries.GetCityDetail;
using Sightseeing.Application.Features.Countries.Commands.CreateCountry;
using Sightseeing.Application.Features.Countries.Queries.GetAllCountries;
using Sightseeing.Application.Features.Countries.Queries.GetCountryDetail;
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
            CreateMap<Attraction, AttractionDto>();

            CreateMap<Attraction, AttractionDetailVm>();
            CreateMap<AttractionCategory, AttractionDetailCategoryVm>();
            CreateMap<City, AttractionDetailCityVm>();

            CreateMap<Attraction, AttractionListVm>()
                .ForMember(a => a.Id, opt => opt.MapFrom(aa => aa.AttractionId))
                .ForMember(a => a.AttractionCategoryName, opt => opt.MapFrom(aa => aa.Category.Name))
                .ForMember(a => a.CityName, opt => opt.MapFrom(aa => aa.City.Name));

            CreateMap<CreateAttractionCategoryCommand, AttractionCategory>();
            CreateMap<AttractionCategory, AttractionCategoryDto>()
                .ForMember(c => c.Id, opt => opt.MapFrom(cc => cc.AttractionCategoryId));

            CreateMap<AttractionCategory, AttractionCategoryListVm>()
                .ForMember(c => c.Id, opt => opt.MapFrom(cc => cc.AttractionCategoryId));

            CreateMap<AttractionCategory, AttractionCategoryDetailVm>()
                .ForMember(c => c.Id, opt => opt.MapFrom(cc => cc.AttractionCategoryId));
            CreateMap<Attraction, AttractionCategoryDetailAttractionVm>()
                .ForMember(a => a.CityName, opt => opt.MapFrom(aa => aa.City.Name))
                .ForMember(a => a.Id, opt => opt.MapFrom(aa => aa.AttractionId));

            CreateMap<CreateCityCommand, City>();
            CreateMap<City, CityDto>();

            CreateMap<City, CityListVm>()
                .ForMember(c => c.Id, opt => opt.MapFrom(cc => cc.CityId));

            CreateMap<City, CityDetailVm>()
                .ForMember(c => c.Id, opt => opt.MapFrom(cc => cc.CityId))
                .ForMember(c => c.CountryName, opt => opt.MapFrom(cc => cc.Country.Name));
            CreateMap<Attraction, CityDetailAttractionVm>()
                .ForMember(a => a.Id, opt => opt.MapFrom(aa => aa.AttractionId))
                .ForMember(a => a.AttractionCategoryName, opt => opt.MapFrom(aa => aa.Category.Name));

            CreateMap<CreateCountryCommand, Country>();
            CreateMap<Country, CountryDto>();

            CreateMap<Country, CountryListVm>()
                .ForMember(c => c.Id, opt => opt.MapFrom(cc => cc.CountryId));

            CreateMap<Country, CountryDetailVm>()
                .ForMember(c => c.Id, opt => opt.MapFrom(cc => cc.CountryId));
            CreateMap<City, CountryDetailCityVm>()
                .ForMember(c => c.Id, opt => opt.MapFrom(cc => cc.CityId));
        }
    }
}
