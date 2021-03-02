using System;

namespace HttpClientCommunicationClassLibrary.Model
{
    public class BearerToken
    {
        public string Token { get; set; }
        public DateTime tokenExpires { get; set; }
    }
}
