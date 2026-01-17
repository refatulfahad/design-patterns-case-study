using design_pattern_case_1.Data;

namespace design_pattern_case_1.Services
{
    public class ConfigService
    {
        private readonly AppDbContext _applicationDbContext;
        public ConfigService(AppDbContext applicationDbContext) { _applicationDbContext = applicationDbContext; }

        public string? GetConfigValue(string key)
        {

            var config = _applicationDbContext.AppConfigs.FirstOrDefault(c => c.Key == key);
            return config?.Value;
        }
    }
}
