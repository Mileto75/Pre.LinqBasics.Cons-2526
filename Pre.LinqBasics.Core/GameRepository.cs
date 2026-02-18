using Newtonsoft.Json;
using Pre.LinqBasics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pre.LinqBasics.Core
{
    public class GameRepository
    {
        private readonly HttpClient _httpClient;
        private const string _url = "https://www.gamerpower.com/api/giveaways";

        public GameRepository()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            _httpClient = new HttpClient(handler);
        }
        public async Task<IEnumerable<Game>> GetGames()
        {
            var result = await _httpClient.GetAsync(_url);
            var content = await result.Content.ReadAsStringAsync();
            var games = JsonConvert.DeserializeObject<List<Game>>(content);
            games.ForEach(g => g.PlatformsList = g.Platforms.Split(","));
            return games;
        }
    }
}
