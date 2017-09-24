using System.Threading.Tasks;

namespace ClientAPI.Network
{
    public interface IHttpRequestHandler
    {
        Task<string> SendPostRequest(string uriPostfix, object payload);
        Task<string> SendPutRequest(string uriPostfix, object payload);
        Task<string> SendDeleteRequest(string orderID, object payload);
        Task<string> SendGetRequest(string orderID, object payload);
    }
}