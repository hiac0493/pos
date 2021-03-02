using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace HttpClientCommunicationClassLibrary
{
    public class JsonContent : StringContent
    {
        private const string MediaType = "application/json";

        public JsonContent(object content)
            : base(GetContent(content), Encoding.UTF8, MediaType)
        {
        }

        private static string GetContent(object content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return JsonConvert.SerializeObject(content);
        }
    }
}
