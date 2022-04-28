using Flurl.Http;

namespace Application.Common
{
    public class Response<T>
    {
        public int? StatusCode { get; set; }
        public T Model { get; set; }
        public Response()
        {

        }
        public Response(IFlurlResponse response, T model)
        {
            StatusCode = response?.StatusCode;
            Model = model;
        }
    }
}
