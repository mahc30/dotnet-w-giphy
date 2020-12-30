using System;

namespace BlazorApp.Data
{
    public class GifItem
    {
        public GifItem(string id, string title, string url, string embed_url)
        {
            Id = id;
            Title = title;
            Url = url;
            Embed_url = embed_url;
        }

        public string Title { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public string Embed_url { get; set; }
    }
}