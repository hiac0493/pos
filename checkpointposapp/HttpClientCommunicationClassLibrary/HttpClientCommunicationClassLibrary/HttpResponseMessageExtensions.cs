using System.Net.Http;
using Newtonsoft.Json;

namespace HttpClientCommunicationClassLibrary
{
    public static class HttpResponseMessageExtensions
    {
        public static TResult Data<TResult>(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TResult>(data);
        }
    }
}
