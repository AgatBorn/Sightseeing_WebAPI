using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection RegisterPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SightseeingDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SightseeingConnectionString"));
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAttractionRepository, AttractionRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.Decorate<ICountryRepository, CachedCountryRepository>();
            services.AddScoped<IAttractionCategoryRepository, AttractionCategoryRepository>();

            return services;
        }
    }
}
