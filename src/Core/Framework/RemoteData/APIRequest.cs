using Framework.Security;
using Framework.Utilities;
using Polly;
using Polly.Fallback;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Framework.RemoteData.Dtos;
using Framework.Exceptions;

namespace Framework.RemoteData
{
    public class APIRequest
    {
        public static class Post<Input, Output>
        {
            private static StringContent ConvertModelToEncryptedString(Input model)
            {
                if (model == null)
                    return null;
                try
                {
                    var myContent = ObjectSerializer<Input>.SerializingObject(model);
                    var myContentEncrypted = myContent.EncryptValue();

                    return new StringContent("\"" + myContentEncrypted + "\"", Encoding.UTF8, "application/json");
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
                    return new StringContent(myContent, Encoding.UTF8, "application/json");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            /// <summary>
            /// Encrypting (as needed) data then send a POST request to the specified Uri as an asynchronous operation.
            /// </summary>
            /// <param name="PostClientNameDto<Input>">this model will get client factory name </param>
            /// <returns>Output as output</returns>
            public static async Task<RequestResultDto<Output>> PostDataAsync(PostClientRequestDto<Input> model)
            {
                var resultOutput = InitialOutputDto();

                try
                {
                    if (string.IsNullOrEmpty(model.ClientName) && string.IsNullOrEmpty(model.BaseUrl))
                        throw new AppException("Client or base url is not specified", (int)System.Net.HttpStatusCode.ExpectationFailed);

                    using (var client = !string.IsNullOrWhiteSpace(model.ClientName) ?
                                            model.ClientFactory.CreateClient(model.ClientName) :
                                             model.ClientFactory.CreateClient())
                    {
                        SetBaseUrl(model.BaseUrl, client);
                        SetTimeOut(model.TimeOut, client);
                        SetHeaders(model.Headers, client);
                        SetToken(model.Token, client);


                        var responseData = new HttpResponseMessage();
                        if (model.NeedStringContent)
                        {
                            StringContent content;
                            if (model.EncryptSender)
                                content = ConvertModelToEncryptedString(model.InputModel);
                            else
                                content = ConvertModelToString(model.InputModel);

                            responseData = await client.PostAsync(model.ApiUrl, content);
                        }
                        else
                            responseData = await client.PostAsync(model.ApiUrl, model as FormUrlEncodedContent);

                        if (responseData.IsSuccessStatusCode)
                        {
                            resultOutput.IsSucceeded = true;
                            resultOutput.Message = string.Empty;
                            resultOutput.StatusCode = responseData.StatusCode;

                            var response = await responseData.Content.ReadAsStringAsync();

                            if (response.GetType().IsValueType)
                                resultOutput.Output = (Output)Convert.ChangeType(response, typeof(Output));
                            else if (model.EncryptReceiver)
                                resultOutput.Output = ConvertStringToModelDecrypted(response);
                            else
                                resultOutput.Output = ConvertStringToModel(response);

                            return resultOutput;
                        }

                        resultOutput.IsSucceeded = false;
                        resultOutput.Message = responseData.ReasonPhrase;
                        resultOutput.StatusCode = responseData.StatusCode;
                        resultOutput.Output = ((Output)Convert.ChangeType(null, typeof(Output)));

                        return resultOutput;
                    }
                }
                catch (Exception ex)
                {
                    resultOutput.IsSucceeded = false;
                    resultOutput.Message = ex.Message;

                    return resultOutput;
                }
            }

            private static RequestResultDto<Output> InitialOutputDto()
            {
                return new RequestResultDto<Output>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = string.Empty,
                    IsSucceeded = false
                };
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

        public static class Get<Output>
        {
            /// <summary>
            /// Send a GET request to the specified Uri.
            /// </summary>
            /// <param name="GetClientNameDto">Values will be encrypted. NOTE that it must be handled by the specified URI.</param>
            /// <returns> Output as output</returns>
            public static async Task<RequestResultDto<Output>> GetDataAsync(GetClientRequestDto model)
            {
                var resultOutput = InitialOutputDto();

                try
                {
                    if (string.IsNullOrEmpty(model.ClientName) && string.IsNullOrEmpty(model.BaseUrl))
                        throw new AppException("Client or base url is not specified", (int)System.Net.HttpStatusCode.ExpectationFailed);

                    using (var client = !string.IsNullOrWhiteSpace(model.ClientName) ?
                                            model.ClientFactory.CreateClient(model.ClientName) :
                                             model.ClientFactory.CreateClient())
                    {
                        SetBaseUrl(model.BaseUrl, client);
                        SetTimeOut(model.Timeout, client);
                        SetHeaders(model.Headers, client);
                        SetToken(model.Token, client);

                        var queryStr = GenerateQueryString(model);
                        var response = await client.GetAsync(model.ApiUrl + queryStr);

                        if (response.IsSuccessStatusCode)
                        {
                            resultOutput.IsSucceeded = true;
                            resultOutput.Message = string.Empty;
                            resultOutput.StatusCode = response.StatusCode;

                            string data = await response.Content.ReadAsStringAsync();

                            if (data.GetType().IsValueType)
                                resultOutput.Output = (Output)Convert.ChangeType(data, typeof(Output));
                            else if (model.EncryptReceiver)
                                resultOutput.Output = ConvertStringToModelDecrypted(data);
                            else
                                resultOutput.Output = ConvertStringToModel(data);

                            return resultOutput;
                        }

                        resultOutput.IsSucceeded = false;
                        resultOutput.Message = response.ReasonPhrase;
                        resultOutput.StatusCode = response.StatusCode;
                        resultOutput.Output = ((Output)Convert.ChangeType(null, typeof(Output)));

                        return resultOutput;
                    }
                }
                catch (Exception ex)
                {
                    resultOutput.IsSucceeded = false;
                    resultOutput.Message = ex.Message;

                    return resultOutput;
                }
            }

            private static RequestResultDto<Output> InitialOutputDto()
            {
                return new RequestResultDto<Output>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = string.Empty,
                    IsSucceeded = false
                };
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

        private static string GenerateQueryString(GetClientRequestDto model)
        {
            string queryStr = string.Empty;
            if (model.InputModel != null && model.InputModel.Count > 0)
                foreach (var value in model.InputModel)
                    if (string.IsNullOrWhiteSpace(queryStr))
                        queryStr = $"?{value.Key}={(model.EncryptSender ? value.Value.EncryptValue() : value.Value)}";
                    else
                        queryStr += $"&{value.Key}={(model.EncryptSender ? value.Value.EncryptValue() : value.Value)}";

            return queryStr;
        }

        private static void SetToken(string token, HttpClient client)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", token);
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
            }
        }

        private static void SetHeaders(string[] headers, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            if (headers != null)
                foreach (var header in headers)
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
        }

        private static void SetBaseUrl(string baseUrl, HttpClient client)
        {
            if (string.IsNullOrWhiteSpace(client.BaseAddress.AbsolutePath)
                && !string.IsNullOrWhiteSpace(baseUrl))
                client.BaseAddress = new Uri(baseUrl);
        }

        private static void SetTimeOut(int timeOut, HttpClient client)
        {
            if (timeOut > 0)
                client.Timeout = new TimeSpan(timeOut);
        }
    }
}
