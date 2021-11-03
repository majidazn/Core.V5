using Framework.Security;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utilities
{
 
    /// <summary>
    /// Provides a base class for sending HTTP POST requests and receiving HTTP responses.
    /// </summary>
    public static class HttpRequestSender
    {
        /// <summary>
        /// Provides a base class for sending HTTP POST requests and receiving HTTP responses from a resource identified by a URI.
        /// </summary>
        /// <typeparam name="T">input model type</typeparam>
        /// <typeparam name="S">output model type</typeparam>
        public static class Post<T, S> where T : class
                                              where S : class
        {
            private static StringContent ConvertModelToEncryptedString(T model)
            {
                if (model == null)
                    return null;
                try
                {
                    var myContent = ObjectSerializer<T>.SerializingObject(model);
                    var myContentEncrypted = myContent.EncryptValue();

                    return new StringContent("\"" + myContentEncrypted + "\"", System.Text.Encoding.UTF8, "application/json");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            private static StringContent ConvertModelToString(T model)
            {
                if (model == null)
                    return null;
                try
                {
                    var myContent = ObjectSerializer<T>.SerializingObject(model);
                    return new StringContent(myContent, System.Text.Encoding.UTF8, "application/json");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            /// <summary>
            /// Encrypting (as needed) data then send a POST request to the specified Uri as an asynchronous operation.
            /// </summary>
            /// <param name="baseUrl">A string that identifies the resource to be represented by the System.Uri instance. Note that an IPv6 address in string form must be enclosed within brackets. For  example,  http://[2607:f8b0:400d:c06::69] .</param>
            /// <param name="apiUrl">The Uri the request is sent to. ex: api/Doctor/GetPatients/</param>
            /// <param name="model">The model that you want to send as content</param>
            /// <param name="token">Assigning the token, if ever, to Request.Headers["Authorization"].ToString()</param>
            /// <param name="headers">Header equals to "application/x-www-form-urlencoded" for default value.</param>
            /// <param name="hasEncryption">Model will be encrypted. NOTE that it must be handled by the specified URI.</param>
            /// <param name="needStringContent">Converting model to json serilized string. default value is true. FormUrlEncodedContent for setting to false</param>
            /// <returns></returns>
            public static async Task<S> PostDataAsync(string baseUrl, string apiUrl, T model, string token = "", List<string> headers = null, bool hasEncryption = false, bool needStringContent = true)
            {
                try
                {
                    var client = new HttpClientFactory();
                   //{
                        client.BaseAddress = new Uri(baseUrl);

                        if (headers == null)
                            headers = new List<string>();
                        headers.Add("application/x-www-form-urlencoded");

                        client.DefaultRequestHeaders.Accept.Clear();
                        foreach (var header in headers)
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));

                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            client.DefaultRequestHeaders.Add("Authorization", token);
                            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
                        }

                        var responseData = new HttpResponseMessage();

                        if (needStringContent)
                        {
                            StringContent content;
                            if (hasEncryption)
                                content = ConvertModelToEncryptedString(model);
                            else
                                content = ConvertModelToString(model);
                            responseData = await client.PostAsync(apiUrl, content);
                        }
                        else
                            responseData = await client.PostAsync(apiUrl, model as FormUrlEncodedContent);

                        if (responseData.IsSuccessStatusCode)
                        {
                            var response = await responseData.Content.ReadAsStringAsync();

                            if (hasEncryption)
                                return ConvertStringToModelDecrypted(response);
                            else
                                return ConvertStringToModel(response);
                        }

                        throw new HttpRequestException(responseData.ReasonPhrase);
                   // }
                }
                catch (HttpRequestException ex)
                {
                    throw new HttpRequestException(ex.Message, ex.InnerException);
                }
            }

            /// <summary>
            /// Send a POST request to the specified Uri.
            /// </summary>
            /// <param name="baseUrl">A string that identifies the resource to be represented by the System.Uri instance. Note that an IPv6 address in string form must be enclosed within brackets. For  example,  http://[2607:f8b0:400d:c06::69] .</param>
            /// <param name="apiUrl">The Uri the request is sent to. ex: api/Doctor/GetPatients/</param>
            /// <param name="model">The model that you want to send as content</param>
            /// <param name="token">Assigning token, if ever, to Request.Headers["Authorization"].ToString()</param>
            /// <param name="headers">Header equals to "application/x-www-form-urlencoded" for default value.</param>
            /// <param name="hasEncryption">Model will be encrypted. NOTE that it must be handled by the specified URI.</param>
            /// <param name="needStringContent">Converting model to json serilized string. default value is true. FormUrlEncodedContent for setting to false</param>
            /// <returns></returns>
            public static S PostData(string baseUrl, string apiUrl, T model, bool needStringContent = true, string token = "", List<string> headers = null, bool hasEncryption = false)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);

                        if (headers == null)
                            headers = new List<string>();
                        headers.Add("application/x-www-form-urlencoded");

                        client.DefaultRequestHeaders.Accept.Clear();
                        foreach (var header in headers)
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));

                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            client.DefaultRequestHeaders.Add("Authorization", token);
                            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
                        }

                        var responseData = new HttpResponseMessage();
                        if (needStringContent)
                        {
                            StringContent content;
                            if (hasEncryption)
                                content = ConvertModelToEncryptedString(model);
                            else
                                content = ConvertModelToString(model);
                            responseData = client.PostAsync(apiUrl, content).Result;
                        }
                        else
                            responseData = client.PostAsync(apiUrl, model as FormUrlEncodedContent).Result;

                        //var responseData = client.PostAsync(apiUrl, content).Result;
                        if (responseData.IsSuccessStatusCode)
                        {
                            var response = responseData.Content.ReadAsStringAsync();
                            if (hasEncryption)
                                return ConvertStringToModelDecrypted(response.Result);
                            else
                                return ConvertStringToModel(response.Result);
                        }

                        throw new HttpRequestException(responseData.ReasonPhrase);
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw new HttpRequestException(ex.Message, ex.InnerException);
                }
            }

            private static S ConvertStringToModelDecrypted(string value)
            {
                try
                {
                    var deserilizedData = ObjectSerializer<string>.DeserilizngObject(value);
                    var decryptedResult = deserilizedData.DecryptValue();

                    return ObjectSerializer<S>.DeserilizngObject(decryptedResult);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            private static S ConvertStringToModel(string value)
            {
                try
                {
                    return ObjectSerializer<S>.DeserilizngObject(value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Provides a base class for sending HTTP GET requests and receiving HTTP responses.
        /// </summary>
        /// <typeparam name="T">output model</typeparam>
        public static class Get<T> where T : class
        {

            /// <summary>
            /// Send a GET request to the specified Uri as an asynchronous operation.
            /// </summary>
            /// <param name="baseUrl">A string that identifies the resource to be represented by the System.Uri instance. Note that an IPv6 address in string form must be enclosed within brackets. For  example,  http://[2607:f8b0:400d:c06::69] .</param>
            /// <param name="apiUrl">The Uri the request is sent to. ex: api/Doctor/GetPatients/</param>
            /// <param name="values">List of pair of value that you want to send as query string</param>
            /// <param name="token">Assigning token, if ever, to Request.Headers["Authorization"].ToString()</param>
            /// <param name="headers">Header equals to "application/x-www-form-urlencoded" for default value.</param>
            /// <param name="hasEncryption">Values will be encrypted. NOTE that it must be handled by the specified URI.</param>
            /// <returns> T as output</returns>
            public static async Task<T> GetDataAsync(string baseUrl, string apiUrl, List<KeyValuePair<string, string>> values = null, string token = "", List<string> headers = null, bool hasEncryption = false)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);

                        if (headers == null)
                            headers = new List<string>();
                        headers.Add("application/x-www-form-urlencoded");

                        client.DefaultRequestHeaders.Accept.Clear();
                        foreach (var header in headers)
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));

                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            client.DefaultRequestHeaders.Add("Authorization", token);
                            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
                        }

                        string queryStr = string.Empty;
                        if (values != null && values.Count > 0)
                            foreach (var value in values)
                            {
                                if (string.IsNullOrWhiteSpace(queryStr))
                                    queryStr = $"?{value.Key}={(hasEncryption ? value.Value.EncryptValue() : value.Value)}";
                                else
                                    queryStr += $"&{value.Key}={(hasEncryption ? value.Value.EncryptValue() : value.Value)}";
                            }

                        var response = await client.GetAsync(apiUrl + queryStr);

                        if (response.IsSuccessStatusCode)
                        {
                            string data = await response.Content.ReadAsStringAsync();
                            return ObjectSerializer<T>.DeserilizngObject(data);
                        }

                        throw new HttpRequestException(response.ReasonPhrase);
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw new HttpRequestException(ex.Message, ex.InnerException);
                }
            }

            /// <summary>
            /// Send a GET request to the specified Uri.
            /// </summary>
            /// <param name="baseUrl">A string that identifies the resource to be represented by the System.Uri instance. Note that an IPv6 address in string form must be enclosed within brackets. For  example,  http://[2607:f8b0:400d:c06::69] .</param>
            /// <param name="apiUrl">The Uri the request is sent to. ex: api/Doctor/GetPatients/</param>
            /// <param name="values">List of pair of value that you want to send as query string</param>
            /// <param name="token">Assigning token, if ever, to Request.Headers["Authorization"].ToString()</param>
            /// <param name="headers">Header equals to "application/x-www-form-urlencoded" for default value.</param>
            /// <param name="hasEncryption">Values will be encrypted. NOTE that it must be handled by the specified URI.</param>
            /// <returns> T as output</returns>
            public static T GetData(string baseUrl, string apiUrl, List<KeyValuePair<string, string>> values, string token = "", List<string> headers = null, bool hasEncryption = false)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);

                        if (headers == null)
                            headers = new List<string>();
                        headers.Add("application/x-www-form-urlencoded");

                        client.DefaultRequestHeaders.Accept.Clear();
                        foreach (var header in headers)
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));

                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            client.DefaultRequestHeaders.Add("Authorization", token);
                            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
                        }

                        string queryStr = string.Empty;
                        if (values != null && values.Count > 0)
                            foreach (var value in values)
                            {
                                if (string.IsNullOrWhiteSpace(queryStr))
                                    queryStr = $"?{value.Key}={(hasEncryption ? EncDec.EncryptValue(value.Value) : value.Value)}";
                                else
                                    queryStr += $"&{value.Key}={(hasEncryption ? EncDec.EncryptValue(value.Value) : value.Value)}";
                            }

                        var response = client.GetAsync(apiUrl + queryStr).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            string data = response.Content.ReadAsStringAsync().Result;
                            return ObjectSerializer<T>.DeserilizngObject(data);
                        }

                        throw new HttpRequestException(response.ReasonPhrase);
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw new HttpRequestException(ex.Message, ex.InnerException);
                }

            }
        }
    }
}
