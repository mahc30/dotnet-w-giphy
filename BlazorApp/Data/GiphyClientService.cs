using BlazorApp.models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace BlazorApp.Data
{
    public class GiphyClientService
    {
        private const int GIF_LIMIT = 9;
        private IHttpClientFactory _factory;
        public GiphyClientService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<GifItem[]> GetGifsAsync()
        {
            var client = _factory.CreateClient("Giphy");
            
            GiphyResponse giphyRequest = null;
            HttpResponseMessage  response = await client.GetAsync($"gifs/trending?api_key={Constants.GIPHY_API_KEY}&limit={Constants.GIPHY_MAX_ELEMENTS}");
            if (response.IsSuccessStatusCode)
            {
                giphyRequest = await response.Content.ReadAsAsync<GiphyResponse>();
            }
            return await Task.FromResult(giphyRequest.data.Select(item => new GifItem(item.id, item.title, item.url, item.embed_url)).ToArray());
        }

        async public Task<GifItem[]> GetGifsAsync(string keyword)
        {
            var client = _factory.CreateClient("Giphy");

            GiphyResponse giphyRequest = null;
            HttpResponseMessage response = await client.GetAsync($"gifs/search?api_key={Constants.GIPHY_API_KEY}&limit={Constants.GIPHY_MAX_ELEMENTS}&q={keyword}");

            if (response.IsSuccessStatusCode)
            {
                giphyRequest = await response.Content.ReadAsAsync<GiphyResponse>();
            }
            return await Task.FromResult(giphyRequest.data.Select(item => new GifItem(item.id, item.title, item.url, item.embed_url)).ToArray());
        }
    }
}
