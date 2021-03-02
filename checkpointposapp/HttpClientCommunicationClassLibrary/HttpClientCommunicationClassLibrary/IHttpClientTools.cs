using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace HttpClientCommunicationClassLibrary
{
    public interface IHttpClientTools
    {
        //**************************************************
        //*      G E T  for LOGIN
        //**************************************************

        // This Should be use only with the LOGIN FORM OR PASSWORD RECOVERY. 
        HttpStatusCode HttpGetUser<T>(string url, ref T value, string usr, string pwd);


        // This Should be use only with the LOGIN FORM, 
        List<T> HttpGetCustomUserList<T>(string url, string usr, string pwd, string ErrorMessage);


        // This Should be use only within the PROGRAM.CS AND SHOULD NOT BE USED IN PRODUCTION CODE!!!
        T HttpGetLoggedInUser<T>(string url);


        // This Should be use only within the PROGRAM.CS AND SHOULD NOT BE USED IN PRODUCTION CODE!!! 
        List<T> HttpGetEmployeesUnderSupervision<T>(string url);







        //**************************************************
        //*               G E T
        //**************************************************

        // Get a simbre object T
        HttpStatusCode HttpGetSingle<T>(string url, ref T value, string ErrorMessage);

        // Get a HttpStatusCode and a reference IList<T>
        HttpStatusCode HttpGetList<T>(string url, ref IList<T> Values, string ErrorMessage);


        // Get async object T
        Task<T> HttpGetSingleAsync<T>(string url, string ErrorMessage);

        // Get a async List<T> and a reference IList<T>
        Task<IList<T>> HttpGetListAsync<T>(string url, string ErrorMessage);

        // Get async Stream document
        Task<Stream> HttpGetDocument(string url, string fileName, string ErrorMessage);




        //**************************************************
        //*             P O S T
        //**************************************************

        HttpStatusCode HttpPostString(string url, Dictionary<string, string> values, string ErrorMessage);

        HttpStatusCode HttpPostObject<T>(string url, T entity, string ErrorMesssage);

        Task<HttpStatusCode> HttpPostObjectAsync<T>(string url, T entity, string ErrorMesssage);

        Task<K> HttpPostCreateObjectAsync<T, K>(string url, T entity, K createdObject, string ErrorMesssage);

        Task<HttpStatusCode> HttpPostDocumentAsync(string url, Dictionary<string, byte[]> dictionaryFiles, string ErrorMessage);

        Task<T> HttpPostObjectAsync<T>(string url, string ErrorMesssage);

        // Performs a post request with an object payload to get data back in the response
        Task<K> HttpPostObjectWithResponseDataAsync<T, K>(string url, T entity, string ErrorMesssage);





        //**************************************************
        //*             D E L E T E
        //**************************************************

        Task<HttpStatusCode> HttpDeleteAsync(string url, int entityId, string errorMesssage);

        Task<HttpStatusCode> HttpDeleteAsync(string url, object data, string errorMesssage);
    }
}
