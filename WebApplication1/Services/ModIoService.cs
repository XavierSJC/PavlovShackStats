using System.Text.Json;
using WebApplication1.Models.Mod.io;

namespace WebApplication1.Services
{
    public class ModIoService : IModIoService
    {
        private readonly ILogger<ModIoService> _logger;
        private static int gamePavlovId = 3959;
        private bool _isConfigured = false;
        private string _apiPath;
        private string _apiKey;

        public ModIoService(ILogger<ModIoService> logger, string apiPath, string apiKey) 
        { 
            _logger = logger;
            _apiKey = apiKey;
            _apiPath = apiPath;
        }

        public Mod GetModDetailsByResourceId(int resourceId)
        {
            if(!_isConfigured)
            {
                _logger.LogError("Mod.io service is not configured. Please check your API path and key.");
                return null;
            }

            Mod mod;
            try
            {
                string url = $"{_apiPath}/v1/games/{gamePavlovId}/mods/{resourceId}?api_key={_apiKey}";
                using (HttpClient client = new())
                {
                    string json = client.GetStringAsync(url).Result;
                    mod = JsonSerializer.Deserialize<Mod>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching mod details from Mod.io API.");
                return null;
            }

            return mod;
        }

        private bool IsServiceConfigured()
        {
            if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_apiPath))
            {
                _logger.LogWarning("ApiKey or ApiPath invalid");
                return false;
            }

            string url = $"{_apiPath}/v1/games/{gamePavlovId}/mods?_limit=1&api_key={_apiKey}";
            string json;

            try
            {
                using (HttpClient client = new())
                {
                    json = client.GetStringAsync(url).Result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error to fetch info from mod.io api, please check ApiPath and ApiKey: {messageException}",
                    ex.Message);
                return false;
            }

            return true;
        }


        public bool IsConfigured()
        {
            _isConfigured = IsServiceConfigured();

            if (_isConfigured)
            {
                _logger.LogInformation("Mod.io service configured, info about custom maps will be fetched");
            }
            else
            {
                _logger.LogWarning("Mod.io service with problems, info about custom maps will not be fetched, " +
                    "plase check ApiPath and ApiKey information");
            }

            return _isConfigured;
        }
    }
}
