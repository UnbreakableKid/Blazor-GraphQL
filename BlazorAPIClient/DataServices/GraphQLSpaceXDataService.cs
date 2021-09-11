using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorAPIClient.Dtos;

namespace BlazorAPIClient.DataServices
{
    public class GraphQLSpaceXDataService : ISpaceXDataService
    {
        private readonly HttpClient _httpClient;

        public GraphQLSpaceXDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<LaunchDto[]> GetAllLaunchesAsync()
        {
            var queryObject = new
            {
                query = @"{ launches { id is_tentative mission_name launch_date_local}}",
                variables = new { }
            };

            var launchQuery = new StringContent(
                JsonSerializer.Serialize(queryObject),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var result = await _httpClient.PostAsync("graphql", launchQuery);

            if (result.IsSuccessStatusCode)
            {
                var gqlData = await JsonSerializer.DeserializeAsync<GQLData>(await result.Content.ReadAsStreamAsync());

                return gqlData.Data.Launches;
            }

            return null;
        }
    }
}