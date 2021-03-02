//*********************************************************************************************
//
//HttpClientTools : HttpClient communications tools.
//
//*********************************************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HttpClientCommunicationClassLibrary.Model;
using Newtonsoft.Json;

namespace HttpClientCommunicationClassLibrary
{
    public class HttpClientTools : IHttpClientTools, IDisposable
    {
        // HttpClient´s for read data
        private static HttpClient _httpClientGet;
        private static HttpClient _httpClientGetTask;

        // HttpClient´s for post data
        private static HttpClient _httpClientPost;
        private static HttpClient _httpClientPostTask;
        private static HttpClient _httpClientPostDocumentTask;

        private static HttpClient _httpSendGridPost;

        //**************************************************
        //*           VARIABLES
        //**************************************************

        public HttpClientTools(bool isTrustCertificate)
        {
            // **************************************************************************************************************************************************
            // * This callback is useded only for Self-Signed certificate and ignore non-trusted check; don't use it when is a CA trusted...
            // **************************************************************************************************************************************************
            if (isTrustCertificate)
            {
                ServicePointManager.ServerCertificateValidationCallback =
                    (sender, certificate, chain, sslPolicyErrors) => true;
            }
        }

        /// <summary>
        /// Http tool where no auth is required, only the uri
        /// </summary>
        /// <param name="webApiUrl"></param>
        public HttpClientTools(string webApiUrl)
        {
            WebApiUrl = webApiUrl;
        }

        //public HttpClientTools(string webApiUrl, string user, string password, string key, bool isTrustCertificate)
        public HttpClientTools(string webApiUrl, string user, string password, bool isTrustCertificate)
        {
            WebApiUrl = webApiUrl;
            UserName = user;
            PassWord = password;

            //Use this with Untrusted Certificate, remove when it's trusted.
            if (isTrustCertificate)
            {
                ServicePointManager.ServerCertificateValidationCallback =
                    (sender, certificate, chain, sslPolicyErrors) => true;
            }
        }


        //**************************************************
        //*           PRIVATE PROPERTIES
        //**************************************************

        public string WebApiUrl { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string PassWord { get; set; } = string.Empty;

        //public string Key
        //{
        //    get { return _key; }
        //    set { _key = value; }
        //}

        private BearerToken AuthorizationToken { get; set; }

        public void Dispose()
        {
            _httpClientGet?.Dispose();
            _httpClientGetTask?.Dispose();
            _httpClientPost?.Dispose();
            _httpClientPostTask?.Dispose();
            _httpClientPostDocumentTask?.Dispose();
        }



        //**************************************************
        //*      G E T  for LOGIN                          *
        //**************************************************

        // This Should be use only with the LOGIN FORM OR PASSWORD RECOVERY.
        public HttpStatusCode HttpGetUser<T>(string url, ref T value, string usr, string pwd)
        {
            var host = WebApiUrl + url;
            try
            {
                using (var client = new HttpClient())
                {
                    SetupAuthorization(client);
                    // Send the GET request and read the response
                    var response = client.GetAsync(host).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        value = response.Data<T>();
                    }

                    return response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(host, ex);
            }

            return HttpStatusCode.NotFound;
        }

        // This Should be use only with the LOGIN FORM,
        public List<T> HttpGetCustomUserList<T>(string url, string usr, string pwd, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            IList<T> values = new List<T>();
            try
            {
                using (var client = new HttpClient())
                {
                    SetupAuthorization(client);
                    // Send the GET request and read the response
                    var response = client.GetAsync(serverHost).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        values = response.Data<List<T>>();
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return (List<T>) Convert.ChangeType(values, typeof(List<T>));
        }

        // This Should be use only within the PROGRAM.CS AND SHOULD NOT BE USED IN PRODUCTION CODE!!!
        public T HttpGetLoggedInUser<T>(string url)
        {
            var serverHost = WebApiUrl + url;
            var employee = (T) Activator.CreateInstance(typeof(T));
            using (var client = new HttpClient())
            {
                SetupAuthorization(client);
                // Send the GET request and read the response
                var response = client.GetAsync(serverHost).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    employee = response.Data<T>();
                }
            }

            return employee;
        }

        // This Should be use only within the PROGRAM.CS AND SHOULD NOT BE USED IN PRODUCTION CODE!!!
        public List<T> HttpGetEmployeesUnderSupervision<T>(string url)
        {
            var serverHost = WebApiUrl + url;

            IList<T> values = new List<T>();
            using (var client = new HttpClient())
            {
                SetupAuthorization(client);
                // Send the GET request and read the response
                var response = client.GetAsync(serverHost).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    values = response.Data<IList<T>>();
                }
            }

            return (List<T>) Convert.ChangeType(values, typeof(List<T>));
        }




        //**************************************************
        //*               G E T
        //**************************************************

        // Get a simbre object T
        public HttpStatusCode HttpGetSingle<T>(string url, ref T value, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientGet == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientGet = new HttpClient();
                    //SetupAuthorization(httpClientGet);
                }

                // Send the GET request and read the response
                SetupAuthorization(_httpClientGet);
                var response = _httpClientGet.GetAsync(serverHost).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    value = response.Data<T>();
                }

                return response.StatusCode;
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return HttpStatusCode.NotFound;
        }

        // Get a HttpStatusCode and a reference IList<T>
        public HttpStatusCode HttpGetList<T>(string url, ref IList<T> values, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientGet == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientGet = new HttpClient();
                    //SetupAuthorization(httpClientGet);
                }

                // Send the GET request and read the response
                SetupAuthorization(_httpClientGet);
                var response = _httpClientGet.GetAsync(serverHost).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    values = response.Data<List<T>>();
                }

                return response.StatusCode;
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return HttpStatusCode.NotFound;
        }

        // Get async object T
        public async Task<T> HttpGetSingleAsync<T>(string url, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            //object value = null;
            var value = (T) Activator.CreateInstance(typeof(T));
            try
            {
                if (_httpClientGetTask == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientGetTask = new HttpClient();
                    //SetupAuthorization(httpClientGetTask);
                }

                // Send the GET request and read the response
                SetupAuthorization(_httpClientGetTask);
                var response = await _httpClientGetTask.GetAsync(serverHost);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    value = response.Data<T>();
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return value;
        }

        // Get a async List<T> and a reference IList<T>
        public async Task<IList<T>> HttpGetListAsync<T>(string url, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            IList<T> values = new List<T>();
            try
            {
                if (_httpClientGetTask == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientGetTask = new HttpClient();
                    //SetupAuthorization(httpClientGetTask);
                }

                // Send the GET request and read the response
                SetupAuthorization(_httpClientGetTask);
                var response = await _httpClientGetTask.GetAsync(serverHost);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    values = response.Data<List<T>>();
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return values; //(List<T>)Convert.ChangeType(values, typeof(List<T>));
        }

        public async Task<HttpResponseMessage> HttpGetResponseMessageAsync(string url, string ErrorMessage)
        {
            string serverHost = WebApiUrl + url;

            try
            {
                if (_httpClientGetTask == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientGetTask = new HttpClient();

                }

                SetupAuthorization(_httpClientGetTask);
                // Send the GET request and read the response
                var response = await _httpClientGetTask.GetAsync(serverHost);

                return response;
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return null;
        }

        // Get async Stream document
        public async Task<Stream> HttpGetDocument(string url, string fileName, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            Stream fileStream = null;
            try
            {
                if (_httpClientGetTask == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientGetTask = new HttpClient();
                    //SetupAuthorization(httpClientGetTask);
                }

                // Send the GET request and read the response
                SetupAuthorization(_httpClientGetTask);
                var response = await _httpClientGetTask.GetAsync(serverHost + fileName);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    fileStream = await response.Content.ReadAsStreamAsync();
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return fileStream;
        }




        //**************************************************
        //*             P O S T
        //**************************************************

        public HttpStatusCode HttpPostString(string url, Dictionary<string, string> values, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientPostTask == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientPostTask = new HttpClient();
                    SetupAuthorization(_httpClientPostTask);
                }

                // Encoded from values into content
                var content = new FormUrlEncodedContent(values);
                var response = _httpClientPostTask.PostAsync(serverHost, content).Result;
                return response.StatusCode;
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return HttpStatusCode.NotFound;
        }

        public HttpStatusCode HttpPostObject<T>(string url, T entity, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientPost == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientPost = new HttpClient();
                    //SetupAuthorization(httpClientPost);
                }

                SetupAuthorization(_httpClientPost);
                var content = new JsonContent(entity);
                var response = _httpClientPost.PostAsync(serverHost, content).Result;
                return response.StatusCode;
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return HttpStatusCode.NotFound;
        }

        public async Task<HttpStatusCode> HttpPostObjectAsync<T>(string url, T entity, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientPostTask == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientPostTask = new HttpClient();
                    //SetupAuthorization(httpClientPostTask);
                }

                SetupAuthorization(_httpClientPostTask);
                var content = new JsonContent(entity);
                var response = await _httpClientPostTask.PostAsync(serverHost, content);
                return response.StatusCode;
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return HttpStatusCode.NotFound;
        }

        public async Task<IEnumerable<K>> HttpPostObjectWithResponseDataAsync<T,K>(string url, List<T> entity, IEnumerable<K> responseObject, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientPostTask == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientPostTask = new HttpClient();

                }

                SetupAuthorization(_httpClientPostTask);
                var data = new JsonContent(entity);
                var response = await _httpClientPostTask.PostAsync(serverHost, data).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return response.Data<IEnumerable<K>>();
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return default(IEnumerable<K>);
        }


        // Performs a post request with an object payload to get data back in the response
        public async Task<K> HttpPostObjectWithResponseDataAsync<T, K>(string url, T entity, K responseObject, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientPostTask == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientPostTask = new HttpClient();

                }

                SetupAuthorization(_httpClientPostTask);
                var data = new JsonContent(entity);
                var response = await _httpClientPostTask.PostAsync(serverHost, data);
                if (response.IsSuccessStatusCode)
                {
                    return response.Data<K>();
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return default(K);
        }

        public async Task<K> HttpPostCreateObjectAsync<T, K>(string url, T entity, K createdObject, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientPostTask == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientPostTask = new HttpClient();
                    //SetupAuthorization(httpClientPostTask);
                }

                SetupAuthorization(_httpClientPostTask);
                var content = new JsonContent(entity);
                var response = await _httpClientPostTask.PostAsync(serverHost, content);

                if (response.IsSuccessStatusCode)
                {
                    return response.Data<K>();
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return default(K);
        }

        public async Task<HttpStatusCode> HttpPostDocumentAsync(string url, Dictionary<string, byte[]> dictionaryFiles, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientPostDocumentTask == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientPostDocumentTask = new HttpClient();
                    //SetupAuthorization(httpClientPostDocumentTask);
                }

                HttpResponseMessage response;
                using (var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                {
                    foreach (var item in dictionaryFiles)
                    {
                        content.Add(new StreamContent(new MemoryStream(item.Value)), "file", item.Key);
                    }

                    SetupAuthorization(_httpClientPostDocumentTask);
                    response = await _httpClientPostDocumentTask.PostAsync(serverHost, content);
                }

                return response.StatusCode;
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return HttpStatusCode.NotFound;
        }

        public async Task<T> HttpPostObjectAsync<T>(string url, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            //object value = null;
            var value = (T) Activator.CreateInstance(typeof(T));
            try
            {
                if (_httpClientPost == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientPost = new HttpClient();
                    //SetupAuthorization(httpClientPost);
                }

                SetupAuthorization(_httpClientPost);
                var response = await _httpClientPost.PostAsync(serverHost, null);
                if (response.IsSuccessStatusCode)
                {
                    value = response.Data<T>();
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return value; //(T)Convert.ChangeType(value, typeof(T));
        }

        // Performs a post request with an object payload to get data back in the response
        public async Task<K> HttpPostObjectWithResponseDataAsync<T, K>(string url, T entity, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientPostTask == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientPostTask = new HttpClient();
                    //SetupAuthorization(httpClientPostTask);
                }

                SetupAuthorization(_httpClientPostTask);
                var data = new JsonContent(entity);
                var response = await _httpClientPostTask.PostAsync(serverHost, data).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return response.Data<K>();
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return default(K);
        }



        //**************************************************
        //*             D E L E T E
        //**************************************************

        public async Task<HttpStatusCode> HttpDeleteAsync(string url, int entityId, string errorMessage)
        {
            var apiEndPoint = string.Empty;

            if (!string.IsNullOrEmpty(url) && url.EndsWith("="))
            {
                apiEndPoint = WebApiUrl + url + entityId;

                try
                {
                    if (_httpClientPost == null)
                    {
                        // Creating and Setting up request header with Basic Authentication
                        _httpClientPost = new HttpClient();
                        //SetupAuthorization(httpClientPost);
                    }

                    SetupAuthorization(_httpClientPost);
                    var response = await _httpClientPost.DeleteAsync(apiEndPoint);

                    return response.StatusCode;
                }
                catch (Exception ex)
                {
                    LogErrorMessage(apiEndPoint, ex);
                }
            }
            else
            {
                throw new ArgumentException("The provided webapi url is not valid");
            }

            return HttpStatusCode.NotFound;
        }

        public async Task<HttpStatusCode> HttpDeleteAsync(string url, object data, string errorMessage)
        {
            var apiEndPoint = string.Empty;

            if (!string.IsNullOrEmpty(url))
            {
                apiEndPoint = WebApiUrl + url;

                try
                {
                    if (_httpClientPost == null)
                    {
                        // Creating and Setting up request header with Basic Authentication
                        _httpClientPost = new HttpClient();
                        //SetupAuthorization(httpClientPost);
                    }

                    SetupAuthorization(_httpClientPost);
                    var json = new JsonContent(data);
                    var request = new HttpRequestMessage(HttpMethod.Delete, apiEndPoint) { Content = json };
                    var response = await _httpClientPost.SendAsync(request);
                    return response.StatusCode;
                }
                catch (Exception ex)
                {
                    LogErrorMessage(apiEndPoint, ex);
                }
            }
            else
            {
                throw new ArgumentException("The provided webapi url is not valid");
            }

            return HttpStatusCode.NotFound;
        }

        public async Task<byte[]> HttpPostByteArrayAsync(string url, string errorMessage)
        {
            var serverHost = WebApiUrl + url;
            byte[] value = null;
            try
            {
                if (_httpClientPost == null)
                {
                    // Creating and Setting up request header with Basic Authentication
                    _httpClientPost = new HttpClient();
                    //SetupAuthorization(httpClientPost);
                }

                SetupAuthorization(_httpClientPost);
                var response = await _httpClientPost.PostAsync(serverHost, null);
                if (response.IsSuccessStatusCode)
                {
                    value = DeserializeByteArray(response.Content);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return value;
        }

        public HttpResponseMessage HttpPostToSendGrid<T>(string url, T entity, string token, string errorMessage)
        {
            var serverHost = url;
            try
            {
                if(_httpSendGridPost == null)
                {
                    _httpSendGridPost = new HttpClient();
                }
                _httpSendGridPost.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new JsonContent(entity);
                var response = _httpSendGridPost.PostAsync(serverHost, content).Result;
                return response;
            }
            catch (Exception ex)
            {
                LogErrorMessage(serverHost, ex);
            }

            return null;
        }




        //******************************************************
        //*                 NEW SYNTAX
        //******************************************************

        public async Task<HttpResponseMessage> HttpGetAsync(string url, bool skipAuth = false)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientGetTask == null)
                {
                    _httpClientGetTask = new HttpClient();
                }

                if (skipAuth == false)
                    SetupAuthorization(_httpClientGetTask);

                var response = await _httpClientGetTask.GetAsync(serverHost);
                return response;
            }
            catch (Exception ex)
            {
                LogErrorMessage(url, ex);
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.RequestTimeout,
                    Content = new StringContent(ex.GetBaseException().Message)
                };
            }
        }

        public async Task<HttpResponseMessage> HttpPostAsync(string url, object obj)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientPostTask == null)
                {
                    _httpClientPostTask = new HttpClient();
                }

                SetupAuthorization(_httpClientPostTask);
                var json = new JsonContent(obj);
                var response = await _httpClientPostTask.PostAsync(serverHost, json);
                return response;
            }
            catch (Exception ex)
            {
                LogErrorMessage(url, ex);
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.RequestTimeout,
                    Content = new StringContent(ex.GetBaseException().Message)
                };
            }
        }

        public async Task<HttpResponseMessage> HttpPutAsync(string url, object obj)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientPostTask == null)
                {
                    _httpClientPostTask = new HttpClient();
                }

                SetupAuthorization(_httpClientPostTask);
                var json = new JsonContent(obj);
                var response = await _httpClientPostTask.PutAsync(serverHost, json);
                return response;
            }
            catch (Exception ex)
            {
                LogErrorMessage(url, ex);
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.RequestTimeout,
                    Content = new StringContent(ex.GetBaseException().Message)
                };
            }
        }

        public async Task<HttpResponseMessage> HttpDeleteAsync(string url, object obj)
        {
            var serverHost = WebApiUrl + url;
            try
            {
                if (_httpClientPostTask == null)
                {
                    _httpClientPostTask = new HttpClient();
                }

                SetupAuthorization(_httpClientPostTask);
                var json = new JsonContent(obj);
                var request = new HttpRequestMessage(HttpMethod.Delete, serverHost) { Content = json };
                var response = await _httpClientPostTask.SendAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                LogErrorMessage(url, ex);
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.RequestTimeout,
                    Content = new StringContent(ex.GetBaseException().Message)
                };
            }
        }




        //**************************************************
        //*          TOOLS
        //**************************************************

        [Obsolete]
        public async Task<T> DeserializeStringAsync<T>(HttpContent content)
        {
            var data = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        [Obsolete]
        public HttpContent SerializeString<T>(T model)
        {
            var data = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            return content;
        }



        public byte[] DeserializeByteArray(HttpContent content)
        {
            var data = content.ReadAsStringAsync().Result.Replace("\"", string.Empty);
            return Convert.FromBase64String(data);
        }

        private static void LogErrorMessage(string url, Exception ex)
        {
            // Log to Text File
            var stamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var name = stamp + "_" + Process.GetCurrentProcess().ProcessName + "_LOG.txt";
            var message = stamp + " " + url + " " + ex.Message;
            var file = new StreamWriter(name);
            file.WriteLine(message);
            file.WriteLine(ex.StackTrace);
            file.Close();
        }

        private void SetupAuthorization(HttpClient client)
        {
            // Bearer Authentification
            if (AuthorizationToken == null || AuthorizationToken.tokenExpires < DateTime.UtcNow)
            {
                AuthorizationToken = GetToken();
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthorizationToken.Token);
        }

        private BearerToken GetToken()
        {
            const string url = "GetUserToken";
            var client = new HttpClient();
            var currentToken = new BearerToken();
            try
            {
                var usr = new AuthRequest
                {
                    User = UserName,
                    Pwd = PassWord
                };
                client.BaseAddress = new Uri(WebApiUrl);
                var content = new JsonContent(usr);
                var response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    currentToken = response.Data<BearerToken>();
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(client.BaseAddress + url, ex);
            }

            return currentToken;
        }
    }
}
