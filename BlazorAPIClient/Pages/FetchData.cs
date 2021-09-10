using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorAPIClient.Dtos;
using Microsoft.AspNetCore.Components;

namespace BlazorAPIClient.Pages
{
    public partial class FetchData
    {

        private LaunchDto[] launches;

        [Inject] //inject the HttpClient instance into the component instance 
        private HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            launches = await Http.GetFromJsonAsync<LaunchDto[]>("rest/launches");
        }
    }
}