using Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Framework.Security;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Framework.RemoteData
{
    /// <summary>
    /// Provides a base class through http client factory for sending HTTP requests and receiving responses.
    /// NOTE: THIS CLASS IS DEPRECATED.. PLEASE USE "APIRequest" CLASS INSTEAD.
    /// </summary>
    //[Obsolete("use APIRequest class instead")]
    public class HttpClientFactoryRequest
    {
        /// <summary>
        /// Provides a base class for sending HTTP POST requests and receiving HTTP responses from a resource identified by a URI.
        /// </summary>
        /// <typeparam name="Input">input model type</typeparam>
        /// <typeparam name="Output">output model type</typeparam>
        public static class Post<Input, Output>// where Input : class where Output : class
        {
            private static StringContent ConvertModelToEncryptedString(Input model)
            {
                if (model == null)
                    return null;
                try
                {
                    var myContent = ObjectSerializer<Input>.SerializingObject(model);
                    var myContentEncrypted = myContent.EncryptValue();

                    return new StringContent("\"" + myContentEncrypted + "\"", System.Text.Encoding.UTF8, "application/json");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            private static StringContent ConvertModelToString(Input model)
            {
                if (model == null)
                    return null;
                try
                {
                    var myContent = ObjectSerializer<Input>.SerializingObject(model);
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
            /// <param name="hasEncryption">Model will be encrypted. NOTE that it must be handled by the specified URI.</param>
            /// <param name="needStringContent">Converting model to json serilized string. default value is true. FormUrlEncodedContent for setting to false</param>
            /// <param name="headers">Header equals to "application/x-www-form-urlencoded" for default value.</param>
            /// <returns>Output as output</returns>
            public static async Task<Output> PostDataByClientNameAsync(IHttpClientFactory clientFactory, string clientName,
                string apiUrl, Input model, string token = "", bool hasEncryption = false, bool needStringContent = true, 
                params string[] headers)
            {
                try
                {
                    using (var client = clientFactory.CreateClient(clientName))
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                        //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

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

                            if (response.GetType().IsValueType)
                                return (Output)Convert.ChangeType(response, typeof(Output));
                            else if (hasEncryption)
                                return ConvertStringToModelDecrypted(response);
                            else
                                return ConvertStringToModel(response);
                        }

                        throw new HttpRequestException(responseData.ReasonPhrase);
                    }
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
            /// <param name="hasEncryption">Model will be encrypted. NOTE that it must be handled by the specified URI.</param>
            /// <param name="needStringContent">Converting model to json serilized string. default value is true. FormUrlEncodedContent for setting to false</param>
            /// <param name="headers">Header equals to "application/x-www-form-urlencoded" for default value.</param>
            /// <returns>Output as output</returns>
            public static async Task<Output> PostDataByBaseUrlAsync(IHttpClientFactory clientFactory, string baseUrl, string apiUrl, Input model, string token = "", bool hasEncryption = false, bool needStringContent = true, params string[] headers)
            {
                try
                {
                    using (var client = clientFactory.CreateClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                      //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

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

                        //var responseData = client.PostAsync(apiUrl, content).Result;
                        if (responseData.IsSuccessStatusCode)
                        {
                            var response = await responseData.Content.ReadAsStringAsync();
                            
                            if (response.GetType().IsValueType)
                                return (Output)Convert.ChangeType(response, typeof(Output));
                            else if (hasEncryption)
                                return ConvertStringToModelDecrypted(response);
                            else
                                return ConvertStringToModel(response);
                        }

                        throw new HttpRequestException(responseData.ReasonPhrase);
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw new HttpRequestException(ex.Message, ex.InnerException);
                }
            }

            private static Output ConvertStringToModelDecrypted(string value)
            {
                try
                {
                    var deserilizedData = ObjectSerializer<string>.DeserilizngObject(value);
                    var decryptedResult = deserilizedData.DecryptValue();

                    return ObjectSerializer<Output>.DeserilizngObject(decryptedResult);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            private static Output ConvertStringToModel(string value)
            {
                try
                {
                    return ObjectSerializer<Output>.DeserilizngObject(value);
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
        /// <typeparam name="Output">output model</typeparam>
        public static class Get<Output>
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
            /// <returns> Output as output</returns>
            public static async Task<Output> GetDataByBaseUrlAsync(IHttpClientFactory clientFactory, string baseUrl, string apiUrl, List<KeyValuePair<string, string>> values = null, string token = "", bool hasEncryption = false, params string[] headers)
            {
                try
                {
                    using (var client = clientFactory.CreateClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);

                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

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
                            if (data.GetType().IsValueType)
                                return (Output)Convert.ChangeType(data, typeof(Output));

                            return ObjectSerializer<Output>.DeserilizngObject(data);
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
            /// <returns> Output as output</returns>
            public static async Task<Output> GetDataByClientNameAsync(IHttpClientFactory clientFactory, string clientName, string apiUrl, List<KeyValuePair<string, string>> values, string token = "", bool hasEncryption = false, params string[] headers)
            {
                try
                {
                    using (var client = clientFactory.CreateClient(clientName))
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                      //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

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
                            if (data.GetType().IsValueType)
                                return (Output)Convert.ChangeType(data, typeof(Output));

                            return ObjectSerializer<Output>.DeserilizngObject(data);
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

