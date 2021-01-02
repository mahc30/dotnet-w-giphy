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
        private IHttpClientFactory _factory;
        public enum GifType
        {
            Gif,
            Sticker
        }
        public GiphyClientService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<Datum[]> GetGifsAsync(GifType type)
        {
            var client = _factory.CreateClient("Giphy");
            
            GiphyResponse giphyRequest = null;
            HttpResponseMessage response;

            if (type == GifType.Gif)
            {
            response = await client.GetAsync($"gifs/trending?api_key={Constants.GIPHY_API_KEY}&limit={Constants.GIPHY_MAX_ELEMENTS}");
            }
            else
            {
                response = await client.GetAsync($"stickers/trending?api_key={Constants.GIPHY_API_KEY}&limit={Constants.GIPHY_MAX_ELEMENTS}");
            }

            if (response.IsSuccessStatusCode)
            {
                giphyRequest = await response.Content.ReadAsAsync<GiphyResponse>();
            }

            return giphyRequest?.data;
        }

        async public Task<Datum[]> GetGifsAsync(GifType type, string keyword)
        {
            var client = _factory.CreateClient("Giphy");

            GiphyResponse giphyRequest = null;
            HttpResponseMessage response = await client.GetAsync($"gifs/search?api_key={Constants.GIPHY_API_KEY}&limit={Constants.GIPHY_MAX_ELEMENTS}&q={keyword}");

            if (response.IsSuccessStatusCode)
            {
                giphyRequest = await response.Content.ReadAsAsync<GiphyResponse>();
            }
            return giphyRequest?.data;
        }

    }
}
