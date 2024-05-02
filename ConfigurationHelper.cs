namespace GRCServices
{
    public class ConfigurationHelper
    {
        private static IConfiguration? _config;
        public static void Initialize(IConfiguration configuration)
        {
            _config = configuration;
        }
    }
}
