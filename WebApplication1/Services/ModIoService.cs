using System.Text.Json;
using WebApplication1.Models.Mod.io;

namespace WebApplication1.Services
{
    public class ModIoService : IModIoService
    {
        private static int gamePavlovId = 3959;
        private string _apiPath;
        private string _apiKey;

        public ModIoService(string apiPath, string apiKey) 
        { 
            _apiKey = apiKey;
            _apiPath = apiPath;
        }

        public Mod GetModDetailsByResourceId(int resourceId)
        {
            string url = $"{_apiPath}/v1/games/{gamePavlovId}/mods/{resourceId}?api_key={_apiKey}";
            string json;

            using (HttpClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }

            return JsonSerializer.Deserialize<Mod>(json);
        }

        public bool IsServiceConfigured()
        {
            if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_apiPath))
            {
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
                return false;
            }

            return true;
        }
    }
}
