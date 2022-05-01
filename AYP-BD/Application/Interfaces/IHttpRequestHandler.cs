using Application.Common;

namespace Application.Interfaces
{
    public interface IHttpRequestHandler
    {
        Task<Response<T>> Get<T>(string path, object queryParams = null);
    }
}
