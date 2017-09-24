using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI.Network
{
    public class HttpRequestHandler : IHttpRequestHandler
    {
        private readonly string ContentType = "application/json";
        private readonly HttpClient httpClient;
        
        public HttpRequestHandler() : this("http://localhost:51295/api/")
        {
        }

        public HttpRequestHandler(string baseURL)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseURL);
            httpClient.DefaultRequestHeaders.Accept.Clear();
        }
        
        public async Task<string> SendPostRequest(string uriPostfix, object payload = null)
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(uriPostfix, payload);
            return await ProcessHttpResponseMessage(responseMessage);
        }

        public async Task<string> SendGetRequest(string uriPostfix, object payload = null)
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync(uriPostfix);
            return await ProcessHttpResponseMessage(responseMessage);
        }

        public async Task<String> SendPutRequest(string uriPostfix, object payload = null)
        {
            HttpResponseMessage responseMessage = await httpClient.PutAsJsonAsync(uriPostfix, payload);
            return await ProcessHttpResponseMessage(responseMessage);            
        }

        public async Task<string> SendDeleteRequest(string uriPostFix, object payload = null)
        {
            HttpResponseMessage responseMessage = await httpClient.SendAsync(CreateHttpRequestMessage(uriPostFix, HttpMethod.Delete, payload));
            return await ProcessHttpResponseMessage(responseMessage);
        }

        private async Task<string> ProcessHttpResponseMessage(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                responseMessage.Dispose();
                return result;
            }
            else
            {
                throw new HttpRequestException((int)responseMessage.StatusCode + " " + responseMessage.ReasonPhrase);
            }
        }

        private HttpRequestMessage CreateHttpRequestMessage(string uriPostfix, HttpMethod httpMethod, object payload)
        {
            string content = ConvertPayloadToJson(payload);
            var httpRequestMessage = new HttpRequestMessage(httpMethod, uriPostfix);
            httpRequestMessage.Headers.Add("Accept", ContentType);
            httpRequestMessage.Content = new StringContent(content, Encoding.UTF8, ContentType);
            return httpRequestMessage;
        }
        
        private string ConvertPayloadToJson(object payload)
        {
            if(payload == null)
            {
                return "";
            }
            return JsonConvert.SerializeObject(payload);
        }
    }
}
