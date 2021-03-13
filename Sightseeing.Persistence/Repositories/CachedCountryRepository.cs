using Microsoft.Extensions.Caching.Memory;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Persistence.Repositories
{
    public class CachedCountryRepository : ICountryRepository
    {
        private const string CountriesCacheKey = "countriesCacheKey";

        private readonly ICountryRepository _countryRepository;
        private readonly IMemoryCache _cache;

        public CachedCountryRepository(ICountryRepository countryRepository, IMemoryCache cache)
        {
            _countryRepository = countryRepository;
            _cache = cache;
        }

        public async Task<Country> AddAsync(Country entity)
        {
            _cache.Remove(CountriesCacheKey);
            return await _countryRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(Country entity)
        {
            _cache.Remove(CountriesCacheKey);
            await _countryRepository.DeleteAsync(entity);
        }

        public async Task<IReadOnlyList<Country>> GetAllAsync()
        {
            IReadOnlyList<Country> countries;
            if (_cache.TryGetValue(CountriesCacheKey, out countries))
            {
                return countries;
            }

            countries = await _countryRepository.GetAllAsync();
            _cache.Set(CountriesCacheKey, countries, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1)));

            return countries;
        }

        public async Task<Country> GetByIdAsync(Guid id)
        {
            IReadOnlyList<Country> countries;
            if (_cache.TryGetValue(CountriesCacheKey, out countries))
            {
                return countries.FirstOrDefault(x => x.CountryId == id);
            }

            return await _countryRepository.GetByIdAsync(id);
        }

        public async Task<Country> GetByIdWithRelatedDataAsync(Guid id)
        {
            return await _countryRepository.GetByIdWithRelatedDataAsync(id);
        }

        public async Task<bool> IsCountryNameUnique(string name)
        {
            IReadOnlyList<Country> countries;
            if (_cache.TryGetValue(CountriesCacheKey, out countries))
            {
                return !countries.Any(x => x.Name == name);
            }

            return await _countryRepository.IsCountryNameUnique(name);
        }

        public async Task UpdateAsync(Country entity)
        {
            _cache.Remove(CountriesCacheKey);
            await _countryRepository.UpdateAsync(entity);
        }
    }
}
