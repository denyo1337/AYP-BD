using Application.Common;
using Application.Interfaces;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{

    public class RequestHandler : IHttpRequestHandler
    {
        private readonly string BASEURL = "https://api.steampowered.com/";
        private readonly IConfiguration _configuration;
        private string privateWebApiKey;
        public RequestHandler(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(IConfiguration));
            privateWebApiKey = _configuration["SteamApiKey"]?.ToString();
        }

        public async Task<Response<T>> Get<T>(string path, object queryParams = null)
        {
            var response = await BASEURL
                .AllowAnyHttpStatus()
                .AppendPathSegment(path)
                .SetQueryParam("key", privateWebApiKey)
                .SetQueryParams(queryParams)
                .GetAsync();
            try
            {
                var data = await response.GetJsonAsync<T>();
                return new(response, data);
            }
            catch (Exception)
            {
                return new(response, default);
            }
        }
    }
}
