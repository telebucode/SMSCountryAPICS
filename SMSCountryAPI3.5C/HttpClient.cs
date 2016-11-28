using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
namespace SMSCountryApi
{
    class HttpClient
    {
        public string authorizationHeader = string.Empty;
        public JObject SendData(string url, string httpMethod, string payload, Dictionary<string, string> requestHeaders)
        {
            JObject result = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader streamReader = null;
            StreamWriter streamWriter = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);         
                if (httpMethod.Equals("POST", StringComparison.CurrentCultureIgnoreCase))
                {
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.Headers.Add("authorization", authorizationHeader);
                    streamWriter = new StreamWriter(request.GetRequestStream());
                    streamWriter.Write(payload);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                response = (HttpWebResponse)request.GetResponse();
                streamReader = new StreamReader(response.GetResponseStream());
                result = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Close();
                result.Add(new JProperty("StatusCode", (int)response.StatusCode)); 
                
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                streamReader = new StreamReader(e.Response.GetResponseStream());
                result = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Close();
              
                result.Add(new JProperty("StatusCode", (int)response.StatusCode));
            }
            finally
            {
                request = null;
                response = null;
            }
            return result;
        }

        public JObject GetData(string url, string httpMethod, string payload, Dictionary<string, string> requestHeaders)
        {
            JObject result = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader streamReader = null;
         
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);                                                          
                if (httpMethod.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
                {
                    request.Method = "GET";               
                    request.Headers.Add("authorization", authorizationHeader);               
                }
                response = (HttpWebResponse)request.GetResponse();

                streamReader = new StreamReader(response.GetResponseStream());
               // string responseText = streamReader.ReadToEnd();
                
                result = JObject.Parse(streamReader.ReadToEnd());
            
                streamReader.Close();
                result.Add(new JProperty("StatusCode", (int)response.StatusCode));

              
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                streamReader = new StreamReader(e.Response.GetResponseStream());
                
                result = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Close();
                result.Add(new JProperty("StatusCode", (int)response.StatusCode));
            }
            finally
            {
                request = null;
                response = null;
            }
            return result;
        }


        public JObject SendGroupData(string url, string httpMethod, string payload, Dictionary<string, string> requestHeaders)
        {
            JObject result = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader streamReader = null;
            StreamWriter streamWriter = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);  
                if (httpMethod.Equals("POST", StringComparison.CurrentCultureIgnoreCase))
                {

                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.Headers.Add("authorization", authorizationHeader);
                    streamWriter = new StreamWriter(request.GetRequestStream());
                    streamWriter.Write(payload);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
               response = (HttpWebResponse)request.GetResponse();      
               streamReader = new StreamReader(response.GetResponseStream());
               result = JObject.Parse(streamReader.ReadToEnd());
             
               streamReader.Close();
               result.Add(new JProperty("StatusCode", (int)response.StatusCode));
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                streamReader = new StreamReader(e.Response.GetResponseStream());
                result = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Close();
                result.Add(new JProperty("StatusCode", (int)response.StatusCode));
            }
            finally
            {
                request = null;
                response = null;
            }
            return result;
        }


        protected class GroupSMS
        {
            public string Text { get; set; }
            public List<string> Numbers { get; set; }
        }

        public JObject DisconnectData(string url, string httpMethod, string payload, Dictionary<string, string> requestHeaders)
        {
            JObject result = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader streamReader = null;
            StreamWriter streamWriter = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                if (httpMethod.Equals("PATCH", StringComparison.CurrentCultureIgnoreCase))
                {
                    request.Method = "PATCH";
                    request.ContentType = "application/json";
                    request.Headers.Add("authorization", authorizationHeader);
                    streamWriter = new StreamWriter(request.GetRequestStream());
                    streamWriter.Write(payload);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                response = (HttpWebResponse)request.GetResponse();
                streamReader = new StreamReader(response.GetResponseStream());
                result = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Close();
                result.Add(new JProperty("StatusCode", (int)response.StatusCode));
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                streamReader = new StreamReader(e.Response.GetResponseStream());
                result = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Close();
                result.Add(new JProperty("StatusCode", (int)response.StatusCode));
            }
            finally
            {
                request = null;
                response = null;
            }
            return result;
        }


        public JObject UpdateData(string url, string httpMethod, string payload, Dictionary<string, string> requestHeaders)
        {
            JObject result = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader streamReader = null;
            StreamWriter streamWriter = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                if (httpMethod.Equals("PATCH", StringComparison.CurrentCultureIgnoreCase))
                {
                    request.Method = "PATCH";
                    request.ContentType = "application/json";
                    request.Headers.Add("authorization", authorizationHeader);
                    streamWriter = new StreamWriter(request.GetRequestStream());
                    streamWriter.Write(payload);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                response = (HttpWebResponse)request.GetResponse();
                streamReader = new StreamReader(response.GetResponseStream());
                result = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Close();
                result.Add(new JProperty("StatusCode", (int)response.StatusCode));
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                streamReader = new StreamReader(e.Response.GetResponseStream());
                result = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Close();
                result.Add(new JProperty("StatusCode", (int)response.StatusCode));
            }
            finally
            {
                request = null;
                response = null;
            }
            return result;
        }


        public JObject PatchData(string url, string httpMethod, string payload, Dictionary<string, string> requestHeaders)
        {
            JObject result = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader streamReader = null;
            StreamWriter streamWriter = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                if (httpMethod.Equals("PATCH", StringComparison.CurrentCultureIgnoreCase))
                {
                    request.Method = "PATCH";
                    request.ContentType = "application/json";
                    request.Headers.Add("authorization", authorizationHeader);
                    streamWriter = new StreamWriter(request.GetRequestStream());
                    streamWriter.Write(payload);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                response = (HttpWebResponse)request.GetResponse();
                streamReader = new StreamReader(response.GetResponseStream());
                result = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Close();
                result.Add(new JProperty("StatusCode", (int)response.StatusCode));
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                GenericResult onjResult = new GenericResult();
                onjResult.StatusCode = ((int)response.StatusCode).ToString();
                onjResult.Message = response.StatusDescription;
                string s = JsonConvert.SerializeObject(onjResult);
                result = JObject.Parse(s);
            }
            finally
            {
                request = null;
                response = null;
            }
            return result;
        }


        public JObject DeleteData(string url, string httpMethod, string payload, Dictionary<string, string> requestHeaders)
        {
            JObject result = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader streamReader = null;
            StreamWriter streamWriter = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                if (httpMethod.Equals("DELETE", StringComparison.CurrentCultureIgnoreCase))
                {
                    request.Method = "DELETE";
                    request.ContentType = "application/json";
                    request.Headers.Add("authorization", authorizationHeader);
                    streamWriter = new StreamWriter(request.GetRequestStream());
                    streamWriter.Write(payload);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                response = (HttpWebResponse)request.GetResponse();
           

                GenericResult onjResult = new GenericResult();
                onjResult.StatusCode = response.StatusCode.ToString();
                onjResult.Message = response.StatusDescription;
                onjResult.Success = true;
                string s = JsonConvert.SerializeObject(onjResult);
                result = JObject.Parse(s);
               // result.Add(new JProperty("StatusCode", response.StatusCode));
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                GenericResult onjResult = new GenericResult();
                onjResult.StatusCode = ((int)response.StatusCode).ToString();
                onjResult.Message = response.StatusDescription;
                string s = JsonConvert.SerializeObject(onjResult);
                result = JObject.Parse(s);
              //  result.Add(new JProperty("StatusCode", response.StatusCode));
            }
            finally
            {
                request = null;
                response = null;
            }
            return result;










        }

        public JObject DeleteDataResponse(string url, string httpMethod, string payload, Dictionary<string, string> requestHeaders)
        {
            JObject result = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader streamReader = null;
            StreamWriter streamWriter = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                if (httpMethod.Equals("DELETE", StringComparison.CurrentCultureIgnoreCase))
                {
                    request.Method = "DELETE";
                    request.ContentType = "application/json";
                    request.Headers.Add("authorization", authorizationHeader);
                    streamWriter = new StreamWriter(request.GetRequestStream());
                    streamWriter.Write(payload);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                response = (HttpWebResponse)request.GetResponse();
                streamReader = new StreamReader(response.GetResponseStream());
                result = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Close();
                result.Add(new JProperty("StatusCode", (int)response.StatusCode));
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                streamReader = new StreamReader(e.Response.GetResponseStream());
                result = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Close();
                //  result.Add(new JProperty("StatusCode", response.StatusCode));
            }
            finally
            {
                request = null;
                response = null;
            }
            return result;
        }


        public void AuthorizationHeader(string authKey, string authToken)
        {
            try
            {
                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(authKey + ":" + authToken);
                string base64 = System.Convert.ToBase64String(toEncodeAsBytes);

                authorizationHeader = "Basic " + base64;
            }
            catch(Exception ex)
            {
                authorizationHeader = "";
            }
        }

        public JObject PatchDataDisconnectparticipitant(string url, string httpMethod, string payload, Dictionary<string, string> requestHeaders)
        {
            JObject result = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader streamReader = null;
            StreamWriter streamWriter = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                if (httpMethod.Equals("PATCH", StringComparison.CurrentCultureIgnoreCase))
                {
                    request.Method = "PATCH";
                    request.ContentType = "application/json";
                    request.Headers.Add("authorization", authorizationHeader);
                    streamWriter = new StreamWriter(request.GetRequestStream());
                    streamWriter.Write(payload);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                response = (HttpWebResponse)request.GetResponse();
                streamReader = new StreamReader(response.GetResponseStream());
                 result = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Close();
                result.Add(new JProperty("StatusCode", (int)response.StatusCode));
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
             
             //   streamReader = new StreamReader(e.Response.GetResponseStream());
                GenericResult onjResult = new GenericResult();
                onjResult.StatusCode =((int)response.StatusCode).ToString();
                onjResult.Message = response.StatusDescription;
                string s= JsonConvert.SerializeObject(onjResult);
                result = JObject.Parse(s);
             
           
            }
            finally
            {
                request = null;
                response = null;
            }
            return result;
        }


    }
}
