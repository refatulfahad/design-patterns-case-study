using design_pattern_case_1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace design_pattern_case_1.Services
{
    public class ConfigService
    {
        private readonly AppDbContext _applicationDbContext;
        private readonly IDistributedCache _cache;
        public ConfigService(AppDbContext applicationDbContext,
            IDistributedCache cache
            )
        {
            _applicationDbContext = applicationDbContext;
            _cache = cache;
        }

        public async Task<string?> GetConfigValue(string key)
        {
            var cachedValue = await _cache.GetStringAsync(key);
            if (cachedValue != null)
            {
                return cachedValue;
            }

            var config = await _applicationDbContext.AppConfigs.FirstOrDefaultAsync(c => c.Key == key);

            if (config != null)
            {
                await _cache.SetStringAsync(key, config.Value,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                    });
            }
            return config?.Value;
        }
    }
}
