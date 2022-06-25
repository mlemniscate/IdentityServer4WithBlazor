using Microsoft.AspNetCore.Components;
using API.Models;
using Client.Services;
using IdentityModel.Client;


namespace Client.Pages
{
    public partial class CoffeeShops
    {
        private List<CoffeeShopModel> Shops = new();

        [Inject] private IConfiguration Config { get; set; }
        [Inject] private ITokenService TokenService { get; set; }
        [Inject] private HttpClient HttpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var tokenResponse = await TokenService.GetToken("CoffeeAPI.read");

            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            var result = await HttpClient.GetAsync(Config["apiUrl"] + "/api/CoffeeShop");
            if (result.IsSuccessStatusCode)
            {
                Shops = await result.Content.ReadFromJsonAsync<List<CoffeeShopModel>>();
            }
        }
    }
}
