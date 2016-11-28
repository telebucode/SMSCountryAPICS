using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace SMSCountryApi
{
    public class ApiClient : IApiActions
    {
        private string _authKey = string.Empty;
        private string _authToken = string.Empty;
        private string _version = ApiVersion.V_0_1;
        private HttpClient _httpClient = new HttpClient();
        private string _domain = "https://restapi.smscountry.com/";
        private string _baseUrl = string.Empty;
        public ApiClient()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["AuthKey"] == null || System.Configuration.ConfigurationManager.AppSettings["AuthToken"] == null)
                throw new KeyNotFoundException("Please setup AuthKey and AuthToken values in application configuration file under appsettings");
            if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["AuthKey"]) || string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["AuthKey"])
                 || string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["AuthToken"]) || string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["AuthToken"]))
                throw new ArgumentNullException("AuthKey and AuthToken must be a non-empty string");
            this._authKey = System.Configuration.ConfigurationManager.AppSettings["AuthKey"];
            this._authToken = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
        }
        public ApiClient(string authKey, string authToken, string version = null)
        {
            if (authKey.Trim() == null || string.IsNullOrEmpty(authKey.Trim()) || string.IsNullOrEmpty(authKey.Trim())
                || authToken.Trim() == null || string.IsNullOrEmpty(authToken.Trim()) || string.IsNullOrEmpty(authToken.Trim()))
                throw new ArgumentNullException("AuthKey and AuthToken must be a non-empty string");
            this._authKey = authKey;
            this._authToken = authToken;
            if (version != null)
                this._version = version;
        }


        //Send Text SMS to a number
        public SendSmsResult SendSms(Sms smsObj)
        {
            SendSmsResult result = new SendSmsResult();
            JObject httpResponse = null;
            if (smsObj.Text.Trim() == null || string.IsNullOrEmpty(smsObj.Text.Trim()) || string.IsNullOrEmpty(smsObj.Text.Trim()))
                throw new ArgumentNullException("Text property of SMS should not be empty");
            if (smsObj.Number.Trim() == null || string.IsNullOrEmpty(smsObj.Number.Trim()) || string.IsNullOrEmpty(smsObj.Number.Trim()))
                throw new ArgumentNullException("Number property of SMS should not be empty");


            JObject payload = new JObject();
            payload.Add(new JProperty("Text", smsObj.Text.Trim()));
            payload.Add(new JProperty("Number", smsObj.Number.Trim()));
            payload.Add(new JProperty("Tool", "CS"));
            //payload.Add(new JProperty("Tool", smsObj.Tool.Trim()));
            if (smsObj.SenderId != null && smsObj.SenderId.Length > 0)
                payload.Add(new JProperty("SenderId", smsObj.SenderId.Trim()));
            if (smsObj.DRNotifyUrl != null && smsObj.DRNotifyUrl.Length > 0)
                payload.Add(new JProperty("DRNotifyUrl", smsObj.DRNotifyUrl.Trim()));
            if (smsObj.DRNotifyMethod != null && smsObj.DRNotifyMethod.Length > 0)
                payload.Add(new JProperty("DRNotifyHttpMethod", smsObj.DRNotifyMethod.Trim()));
        
               

            
              
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.SendData(this.BaseUrl + "SMSes/", "POST", payload.ToString(), null);


            result.Message = httpResponse.SelectToken("Message").ToString();

            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();

            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
              
                result.Success = true;
              
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
                
                result.MessageUUID = httpResponse.SelectToken("MessageUUID").ToString();

                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }

        //Used to track the delivery status of an SMS
        public GetSmsCollectionDetailsResult GetSmsDetails(Sms smsObj)
        {
            GetSmsCollectionDetailsResult result = new GetSmsCollectionDetailsResult();
            JObject httpResponse = null;
            if (smsObj.MessageUUID.Trim() == null || string.IsNullOrEmpty(smsObj.MessageUUID.Trim()) || string.IsNullOrEmpty(smsObj.MessageUUID.Trim()))
                throw new ArgumentNullException("Please Enter MessageUUID");
            JObject payload = new JObject();
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.GetData(this.BaseUrl + "SMSes/" + smsObj.MessageUUID.Trim() + "/", "GET", payload.ToString(), null);


          
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
           
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                
           
                result.objSinlgeSMS = (GetSmsCollectionDetailsList)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("SMS").ToString(), typeof(GetSmsCollectionDetailsList));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }

        }

        //Used to get a list of SMS objects based on certain filters

        public GetSmsCollectionDetailsResult GetSmsCollection(Sms smsObj)
        {
            GetSmsCollectionDetailsResult result = new GetSmsCollectionDetailsResult();
            JObject httpResponse = null;
            //if (smsObj.Startdate == null || string.IsNullOrEmpty(smsObj.Startdate) || string.IsNullOrEmpty(smsObj.Startdate))
            //    throw new ArgumentNullException("Please Enter Startdate");
            //if (smsObj.Enddate == null || string.IsNullOrEmpty(smsObj.Enddate) || string.IsNullOrEmpty(smsObj.Enddate))
            //    throw new ArgumentNullException("Please Enter Enddate");
            JObject payload = new JObject();
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);

            if (smsObj.Tool.ToString() == "")
            {
                httpResponse = this._httpClient.GetData(this.BaseUrl + "SMSes/?FromDate=" + smsObj.Startdate.Trim() + "&ToDate=" + smsObj.Enddate.Trim() + "&Tool=All", "GET", payload.ToString(), null);


            }
            else
            {
                httpResponse = this._httpClient.GetData(this.BaseUrl + "SMSes/?FromDate=" + smsObj.Startdate.Trim() + "&ToDate=" + smsObj.Enddate.Trim() + "&Tool=" + smsObj.Tool, "GET", payload.ToString(), null);

            } 
            string resulnext = httpResponse.ToString();

            result.Message = httpResponse.SelectToken("Message").ToString();
            
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
        

            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                    if (resulnext.Contains("Next"))
                    result.Next = httpResponse.SelectToken("Next").ToString();
                
                result.objGetSmsCollectionDetailsList = (List<GetSmsCollectionDetailsList>)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("SMSes").ToString(), typeof(List<GetSmsCollectionDetailsList>));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }

        }

        

        public SendSmsResult SendBulkSms(Sms smsObj)
        {
            SendSmsResult result = new SendSmsResult();
            JObject httpResponse = null;
            if (smsObj.Text.Trim() == null || string.IsNullOrEmpty(smsObj.Text.Trim()) || string.IsNullOrEmpty(smsObj.Text.Trim()))
                throw new ArgumentNullException("Text property of SMS should not be empty");
            if (smsObj.Numbers.Trim() == null || string.IsNullOrEmpty(smsObj.Numbers.Trim()) || string.IsNullOrEmpty(smsObj.Numbers.Trim()))
                throw new ArgumentNullException("Numbers property of SMS should not be empty");
            //if (smsObj.Tool.Trim() == null || string.IsNullOrEmpty(smsObj.Tool.Trim()) || string.IsNullOrEmpty(smsObj.Tool.Trim()))
            //    throw new ArgumentNullException("Tool property of SMS should not be empty");
            JObject payload = new JObject();
            List<string> names = smsObj.Numbers.Split(',').ToList<string>();
            payload.Add(new JProperty("Text", smsObj.Text.Trim()));
            payload.Add(new JProperty("Numbers", names));
            payload.Add(new JProperty("Tool", "CS"));
            if (smsObj.SenderId != null && smsObj.SenderId.Length > 0)
                payload.Add(new JProperty("SenderId", smsObj.SenderId.Trim()));
            if (smsObj.DRNotifyUrl != null && smsObj.DRNotifyUrl.Length > 0)
                payload.Add(new JProperty("DRNotifyUrl", smsObj.DRNotifyUrl.Trim()));
            if (smsObj.DRNotifyMethod != null && smsObj.DRNotifyMethod.Length > 0)
                payload.Add(new JProperty("DRNotifyHttpMethod", smsObj.DRNotifyMethod.Trim()));
        


            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.SendGroupData(this.BaseUrl + "BulkSMSes/", "POST", payload.ToString(), null);
         
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
               

                result.Success = true;
                result.MessageUUIDs = (List<string>)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("MessageUUIDs").ToString(), typeof(List<string>));
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }



        }


        //Used to dial out a new call to a number. Calls Schema

        public CreateCallResult CreateCall(Call callObj)
        {
            CreateCallResult result = new CreateCallResult();
            JObject httpResponse = null;
            if (callObj.Number.Trim() == null || string.IsNullOrEmpty(callObj.Number.Trim()) || string.IsNullOrEmpty(callObj.Number.Trim()))
                throw new ArgumentNullException("Number property of Call should not be empty");
            if (callObj.AnswerUrl.Trim() == null || string.IsNullOrEmpty(callObj.AnswerUrl.Trim()) || string.IsNullOrEmpty(callObj.AnswerUrl.Trim()))
                throw new ArgumentNullException("AnswerUrl property of Call should not be empty");
            JObject payload = new JObject();
            payload.Add(new JProperty("Number", callObj.Number.Trim()));
            payload.Add(new JProperty("AnswerUrl", callObj.AnswerUrl.Trim()));
            string Xml = "<Response><Play>" + callObj.MessageUrl.Trim() + "</Play></Response>";
            payload.Add(new JProperty("Xml", Xml));
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.SendData(this.BaseUrl + "Calls/", "POST", payload.ToString(), null);
        
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
               
                result.CallUUID = httpResponse.SelectToken("CallUUID").ToString(); 
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }





        //Used to dial out a new call to a number. Calls Schema

        public CreateBulkCallResult CreateBulkCall(Call callObj)
        {
            CreateBulkCallResult result = new CreateBulkCallResult();
            JObject httpResponse = null;
            if (callObj.Number.Trim() == null || string.IsNullOrEmpty(callObj.Number.Trim()) || string.IsNullOrEmpty(callObj.Number.Trim()))
                throw new ArgumentNullException("Number property  should not be empty");
            if (callObj.MessageUrl.Trim() == null || string.IsNullOrEmpty(callObj.MessageUrl.Trim()) || string.IsNullOrEmpty(callObj.MessageUrl.Trim()))
                throw new ArgumentNullException("MessageUrl property  should not be empty");
            if (callObj.AnswerUrl.Trim() == null || string.IsNullOrEmpty(callObj.AnswerUrl.Trim()) || string.IsNullOrEmpty(callObj.AnswerUrl.Trim()))
                throw new ArgumentNullException("AnswerUrl property should not be empty");

            JObject payload = new JObject();
            List<string> number = callObj.Number.Split(',').ToList<string>();

            payload.Add(new JProperty("Numbers", number));
            payload.Add(new JProperty("AnswerUrl", callObj.AnswerUrl.Trim()));
            string Xml = "<Response><Play>" + callObj.MessageUrl.Trim() + "</Play></Response>";
            payload.Add(new JProperty("Xml", Xml));

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.SendData(this.BaseUrl + "BulkCalls/", "POST", payload.ToString(), null);

            

         
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
             
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
                result.CallUUIDs = (List<string>)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("CallUUIDs").ToString(), typeof(List<string>));

                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }

        }


        //Used to get a list of Calls objects based on certain filters

        public GetCallDetailsCollectionResult GetCallList(Call callObj)
        {
            GetCallDetailsCollectionResult result = new GetCallDetailsCollectionResult();
            JObject httpResponse = null;
            JObject payload = new JObject();
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.GetData(this.BaseUrl + "Calls/?FromDateTime=" + callObj.Startdate.Trim() + "&ToDateTime=" + callObj.Enddate.Trim()  , "GET", payload.ToString(), null);

            string resulnext = httpResponse.ToString();
            result.Message = httpResponse.SelectToken("Message").ToString();

            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();

            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                if (resulnext.Contains("Next"))
                    result.Next = httpResponse.SelectToken("Next").ToString();
                 
                result.Success = true;
                result.objGetCallDetailsCollectionList = (List<GetCallDetailsCollectionList>)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Calls").ToString(), typeof(List<GetCallDetailsCollectionList>));
              result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }

        }




        //Used to get the current status of the Call

        public GetCallDetailsCollectionResult GetCallDetails(Call callObj)
        {
            GetCallDetailsCollectionResult result = new GetCallDetailsCollectionResult();
            JObject httpResponse = null;
            if (callObj.callUUID.Trim() == null || string.IsNullOrEmpty(callObj.callUUID.Trim()) || string.IsNullOrEmpty(callObj.callUUID.Trim()))
                throw new ArgumentNullException("Please Enter callUUID");

            JObject payload = new JObject();

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.GetData(this.BaseUrl + "Calls/" + callObj.callUUID.Trim() + "/", "GET", payload.ToString(), null);

            string resulnext = httpResponse.ToString();
            result.Message = httpResponse.SelectToken("Message").ToString();

            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
      
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
                result.objGetCallDetailsCollectionSingle = (GetCallDetailsCollectionList)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Call").ToString(), typeof(GetCallDetailsCollectionList));

                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }




        }



        //Disconnect an on going call by specifying CallUUID

        public DisconnectCallResult DisconnectCall(Call callObj)
        {
            DisconnectCallResult result = new DisconnectCallResult();
            JObject httpResponse = null;
            if (callObj.callUUID.Trim() == null || string.IsNullOrEmpty(callObj.callUUID.Trim()) || string.IsNullOrEmpty(callObj.callUUID.Trim()))
                throw new ArgumentNullException("callUUID should not be empty");

            JObject payload = new JObject();


            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.DisconnectData(this.BaseUrl + "Calls/" + callObj.callUUID.Trim() + "/", "PATCH", payload.ToString(), null);
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
              
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
           

                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
          
        }


        //Used to created your own Group
        public GetCreateNewGroupResult CreateNewGroup(Group groupObj)
        {
            GetCreateNewGroupResult result = new GetCreateNewGroupResult();
            JObject httpResponse = null;
            JObject payload = new JObject();


            JArray jsonArray = new JArray();

            if (groupObj.Members == null )
                throw new ArgumentNullException("Members should not be empty");

            foreach (var i in groupObj.Members)
            {
                JObject formDetailsJson = new JObject();
                formDetailsJson.Add("Name", i.Name.Trim());
                formDetailsJson.Add("Number", i.Number.Trim());
               // formDetailsJson.Add("Number", i.Trim());
                jsonArray.Add(formDetailsJson);
            }
            var s = groupObj.Members.ToList();
            payload.Add(new JProperty("Members", jsonArray));

            payload.Add(new JProperty("Name", groupObj.Name.Trim()));
            payload.Add(new JProperty("StartGroupCallOnEnter", groupObj.StartGroupCallOnEnter.Trim()));
            payload.Add(new JProperty("EndGroupCallOnExit", groupObj.EndGroupCallOnExit.Trim()));
            if (groupObj.TinyName != null && groupObj.TinyName.Length > 0)
                payload.Add(new JProperty("TinyName", groupObj.TinyName.Trim()));
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.SendGroupData(this.BaseUrl + "Groups/", "POST", payload.ToString(), null);

            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
            
                result.Success = true;
                result.objGetCreateNewGroup = (GetCreateNewGroup)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Group").ToString(), typeof(GetCreateNewGroup));
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
          
         
        }



        //Used to get details of a specific group

        public GetGroupDetails GetGroupDetails(Group groupObj)
        {
            GetGroupDetails result = new GetGroupDetails();
            JObject httpResponse = null;
            if (groupObj.groupId.Trim() == null || string.IsNullOrEmpty(groupObj.groupId.Trim()) || string.IsNullOrEmpty(groupObj.groupId.Trim()))
                throw new ArgumentNullException("Please Enter GroupID");

            JObject payload = new JObject();

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.GetData(this.BaseUrl + "Groups/" + groupObj.groupId.Trim() + "/", "GET", payload.ToString(), null);
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
        
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                
                result.objGetGroupDetailsSingle = (GetGroupDetailsList)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Group").ToString(), typeof(GetGroupDetailsList));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }

        // Used to list all your Groups
        public GetGroupDetails GetGroupCollection(Group groupObj)
        {
            GetGroupDetails result = new GetGroupDetails();
            JObject httpResponse = null;
            JObject payload = new JObject();
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);



            httpResponse = this._httpClient.GetData(this.BaseUrl + "Groups/?nameLike=" + groupObj.Name.Trim() + "&startGroupCallOnEnter=" + groupObj.StartGroupCallOnEnter.Trim() + "&endGroupCallOnExit=" + groupObj.EndGroupCallOnExit.Trim() + "&tinyName=" + groupObj.TinyName.Trim(), "GET", payload.ToString(), null);

            
            
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
              
                result.objGetGroupDetailsList = (List<GetGroupDetailsList>)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Groups").ToString(), typeof(List<GetGroupDetailsList>));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }

        //Update group details such as name, tinyname etc
        public GetUpdateGroupResult UpdateGroup(Group GroupObj)
        {
            GetUpdateGroupResult result = new GetUpdateGroupResult();
            JObject httpResponse = null;

            if (GroupObj.groupId.Trim() == null || string.IsNullOrEmpty(GroupObj.groupId.Trim()) || string.IsNullOrEmpty(GroupObj.groupId.Trim()))
                throw new ArgumentNullException("Please Enter GroupID");
            JObject payload = new JObject();

            payload.Add(new JProperty("TinyName", GroupObj.TinyName.Trim()));
            payload.Add(new JProperty("Name", GroupObj.Name.Trim()));

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.UpdateData(this.BaseUrl + "Groups/" + GroupObj.groupId.Trim() + "/", "PATCH", payload.ToString(), null);
            if (httpResponse != null)
            {
               
                result.Message = httpResponse.SelectToken("Message").ToString();
                if (httpResponse.SelectToken("ApiId") != null)
                    result.ApiId = httpResponse.SelectToken("ApiId").ToString();
                if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
                {
                   

                    result.Success = true;
                    result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                    return result;
                }
                else
                {
                    result.Success = false;
                    result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                    return result;



                }
            }
            else
            {
                result.Message = "Group Updated Successfully";
            }
            return result;
        }

        //Delete a group by using GroupId
        public DeleteGroupDetails DeleteGroup(Group GroupObj)
        {
            DeleteGroupDetails result = new DeleteGroupDetails();
            JObject httpResponse = null;
            JObject payload = new JObject();
            if (GroupObj.groupId.Trim() == null || string.IsNullOrEmpty(GroupObj.groupId.Trim()) || string.IsNullOrEmpty(GroupObj.groupId.Trim()))
                throw new ArgumentNullException("Please Enter GroupID");
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.DeleteData(this.BaseUrl + "Groups/" + GroupObj.groupId.Trim() + "/", "DELETE", payload.ToString(), null);
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
                result.Message = httpResponse.SelectToken("Message").ToString();
            }
            else
            {
                result.Success = false;

                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
                result.Message = httpResponse.SelectToken("Message").ToString();
            }
            return result;
        }



        public GetDeleteGroupMemberDetails DeleteMemberfromGroup(Group GroupObj)
        {
            GetDeleteGroupMemberDetails result = new GetDeleteGroupMemberDetails();
            JObject httpResponse = null;
            JObject payload = new JObject();
            if (GroupObj.groupId.Trim() == null || string.IsNullOrEmpty(GroupObj.groupId.Trim()) || string.IsNullOrEmpty(GroupObj.groupId.Trim()))
                throw new ArgumentNullException("Please Enter GroupID");
            if (GroupObj.MemberId.Trim() == null || string.IsNullOrEmpty(GroupObj.MemberId.Trim()) || string.IsNullOrEmpty(GroupObj.MemberId.Trim()))
                throw new ArgumentNullException("Please Enter MemberId");
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.DeleteData(this.BaseUrl + "Groups/" + GroupObj.groupId.Trim() + "/Members/" + GroupObj.MemberId.Trim() + "/", "DELETE", payload.ToString(), null);
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
                result.Message = httpResponse.SelectToken("Message").ToString();
            }
            else
            {
                result.Success = false;

                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
                result.Message = httpResponse.SelectToken("Message").ToString();
            }
            return result;
        }



        //Get a particular member details by GroupId and MemberId

        public GetGroupMemberDetailsResult GetMemberDetails(Group groupObj)
        {
            GetGroupMemberDetailsResult result = new GetGroupMemberDetailsResult();
            JObject httpResponse = null;
            if (groupObj.groupId.Trim() == null || string.IsNullOrEmpty(groupObj.groupId.Trim()) || string.IsNullOrEmpty(groupObj.groupId.Trim()))
                throw new ArgumentNullException("Please Enter GroupId");
            if (groupObj.MemberId.Trim() == null || string.IsNullOrEmpty(groupObj.MemberId.Trim()) || string.IsNullOrEmpty(groupObj.MemberId.Trim()))
                throw new ArgumentNullException("Please Enter Memberid");
            JObject payload = new JObject();

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.GetData(this.BaseUrl + "Groups/" + groupObj.groupId.Trim() + "/Members/" + groupObj.MemberId.Trim() + "/", "GET", payload.ToString(), null);

            
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
              
                result.objGetGroupMemberDetailsSingle = (GetGroupMemberDetailsList)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Member").ToString(), typeof(GetGroupMemberDetailsList));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }



        //Get the details of all the members belongs to a group by using GroupId


        public GetGroupMemberDetailsResult GetallMemberodGroup(Group groupObj)
        {
            GetGroupMemberDetailsResult result = new GetGroupMemberDetailsResult();
            JObject httpResponse = null;
            if (groupObj.groupId.Trim() == null || string.IsNullOrEmpty(groupObj.groupId.Trim()) || string.IsNullOrEmpty(groupObj.groupId.Trim()))
                throw new ArgumentNullException("Please Enter GroupId");

            JObject payload = new JObject();

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.GetData(this.BaseUrl + "Groups/" + groupObj.groupId.Trim() + "/Members/" + groupObj.MemberId.Trim() + "/", "GET", payload.ToString(), null);

         
            result.Message = httpResponse.SelectToken("Message").ToString();

            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                result.objGetGroupMemberDetailsList = (List<GetGroupMemberDetailsList>)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Members").ToString(), typeof(List<GetGroupMemberDetailsList>));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }



        //Update specific member details by using GroupId and MemberId

        public GetUpdateMemberResult UpdateMemberDetails(Group GroupObj)
        {
            GetUpdateMemberResult result = new GetUpdateMemberResult();
            JObject httpResponse = null;
            if (GroupObj.groupId.Trim() == null || string.IsNullOrEmpty(GroupObj.groupId.Trim()) || string.IsNullOrEmpty(GroupObj.groupId.Trim()))
                throw new ArgumentNullException("Please Enter GroupId");
            if (GroupObj.MemberId.Trim() == null || string.IsNullOrEmpty(GroupObj.MemberId.Trim()) || string.IsNullOrEmpty(GroupObj.MemberId.Trim()))
                throw new ArgumentNullException("Please Enter Memberid");
            JObject payload = new JObject();
            payload.Add(new JProperty("Number", GroupObj.Number.Trim()));
            payload.Add(new JProperty("Name", GroupObj.Name.Trim()));
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.UpdateData(this.BaseUrl + "Groups/" + GroupObj.groupId.Trim() + "/Members/" + GroupObj.MemberId.Trim() + "/", "PATCH", payload.ToString(), null);

         
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
               
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }





        //Add a member to an existing group by using GroupId

        public GetGroupMemberDetailsResult AddMembertoExistingGroup(Group GroupObj)
        {
            GetGroupMemberDetailsResult result = new GetGroupMemberDetailsResult();
            JObject httpResponse = null;

            if (GroupObj.groupId.Trim() == null || string.IsNullOrEmpty(GroupObj.groupId.Trim()) || string.IsNullOrEmpty(GroupObj.groupId.Trim()))
                throw new ArgumentNullException("Please Enter GroupId");

            JObject payload = new JObject();

            payload.Add(new JProperty("Number", GroupObj.Number.Trim()));
            payload.Add(new JProperty("Name", GroupObj.Name.Trim()));

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.SendData(this.BaseUrl + "Groups/" + GroupObj.groupId.Trim() + "/Members/", "POST", payload.ToString(), null);

          
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
             
                result.objGetGroupMemberDetailsSingle = (GetGroupMemberDetailsList)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Member").ToString(), typeof(GetGroupMemberDetailsList));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }

        //Create a new group call by providing rth required information furnished below.

        public GetCreateGroupCallResult CreateaGroupCall(GroupCall groupObj)
        {
            GetCreateGroupCallResult result = new GetCreateGroupCallResult();
            JObject httpResponse = null;
            JObject payload = new JObject();
            if (groupObj.Name.Trim() == null || string.IsNullOrEmpty(groupObj.Name.Trim()) || string.IsNullOrEmpty(groupObj.Name.Trim()))
                throw new ArgumentNullException("Please Enter Name of Group");
            if (groupObj.AnswerUrl.Trim() == null || string.IsNullOrEmpty(groupObj.AnswerUrl.Trim()) || string.IsNullOrEmpty(groupObj.AnswerUrl.Trim()))
                throw new ArgumentNullException("Please Enter Answerurl");

            if (groupObj.Participants == null)
                throw new ArgumentNullException("Please Enter Participatants");
            JArray jsonArray = new JArray();

            if (groupObj.Name.Trim() != null && groupObj.Name.Length > 0)
                payload.Add(new JProperty("Name", groupObj.Name.Trim()));

            if (groupObj.WelcomeSound.Trim() != null && groupObj.WelcomeSound.Length > 0)
                payload.Add(new JProperty("WelcomeSound", groupObj.WelcomeSound.Trim()));

            if (groupObj.WaitSound.Trim() != null && groupObj.WaitSound.Length > 0)
                payload.Add(new JProperty("WaitSound", groupObj.WaitSound.Trim()));

            if (groupObj.StartGroupCallOnEnter.Trim() != null && groupObj.StartGroupCallOnEnter.Length > 0)
                payload.Add(new JProperty("StartGroupCallOnEnter", groupObj.StartGroupCallOnEnter.Trim()));

            if (groupObj.EndGroupCallOnExit.Trim() != null && groupObj.EndGroupCallOnExit.Length > 0)
                payload.Add(new JProperty("EndGroupCallOnExit", groupObj.EndGroupCallOnExit.Trim()));

            if (groupObj.AnswerUrl.Trim() != null && groupObj.AnswerUrl.Length > 0)
                payload.Add(new JProperty("AnswerUrl", groupObj.AnswerUrl.Trim()));


            foreach (var i in groupObj.Participants)
            {
                JObject formDetailsJson = new JObject();
                formDetailsJson.Add("Name", i.Name.Trim());
                formDetailsJson.Add("Number", i.Number.Trim());
                jsonArray.Add(formDetailsJson);
            }

            var s = groupObj.Participants.ToList();
            payload.Add(new JProperty("Participants", jsonArray));







            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.SendGroupData(this.BaseUrl + "GroupCalls/", "POST", payload.ToString(), null);
          
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                result.GroupCall = (GroupCall)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("GroupCall").ToString(), typeof(GroupCall));
               // result.GroupCall.Participants = (List<Participatants>)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Participants").ToString(), typeof(List<Participatants>));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
                return result;
            }
        }


        //Get group call list

        public GetGroupCallListResult GetGroupCallList()
        {
            GetGroupCallListResult result = new GetGroupCallListResult();
            JObject httpResponse = null;

            JObject payload = new JObject();

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.GetData(this.BaseUrl + "GroupCalls/", "GET", payload.ToString(), null);

         
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
               
                result.objGetGroupCallList = (List<GetGroupCallList>)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("GroupCalls").ToString(), typeof(List<GetGroupCallList>));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }






        }



        //Get group call details by using GroupCallUUID

        public GetGroupCallListResult GetGroupCallDetails(GroupCall groupObj)
        {
            GetGroupCallListResult result = new GetGroupCallListResult();
            JObject httpResponse = null;
            if (groupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(groupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(groupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");
            JObject payload = new JObject();

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.GetData(this.BaseUrl + "GroupCalls/" + groupObj.GroupCallUUID.Trim() + "/", "GET", payload.ToString(), null);
         
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
               
                result.objGetGroupCallSingle = (GetGroupCallList)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("GroupCall").ToString(), typeof(GetGroupCallList));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }




        //Get a participant details like name, number, calls details by using GroupCallUUID and ParticipantId

        public GetaparticipantDetailsFromGroupCallResult GetaParticipantDetailsFromGroupCall(GroupCall groupObj)
        {
            GetaparticipantDetailsFromGroupCallResult result = new GetaparticipantDetailsFromGroupCallResult();
            JObject httpResponse = null;
            if (groupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(groupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(groupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");
            if (groupObj.ParticipantId.Trim() == null || string.IsNullOrEmpty(groupObj.ParticipantId.Trim()) || string.IsNullOrEmpty(groupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  ParticipantId");
            JObject payload = new JObject();

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.GetData(this.BaseUrl + "GroupCalls/" + groupObj.GroupCallUUID.Trim() + "/Participants/" + groupObj.ParticipantId.Trim() + "/", "GET", payload.ToString(), null);
          
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
               
                result.objGetaparticipantDetailsFromGroupCallSingle = (GetaparticipantDetailsFromGroupCallList)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Participant").ToString(), typeof(GetaparticipantDetailsFromGroupCallList));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }

        }


        //Get all participant details like name, number, calls details by using GroupCallUUID

        public GetaparticipantDetailsFromGroupCallResult GetAllParticipantDetailsFromGroupCall(GroupCall groupObj)
        {
            GetaparticipantDetailsFromGroupCallResult result = new GetaparticipantDetailsFromGroupCallResult();
            JObject httpResponse = null;
            if (groupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(groupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(groupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");
            JObject payload = new JObject();
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.GetData(this.BaseUrl + "GroupCalls/" + groupObj.GroupCallUUID.Trim() + "/Participants/", "GET", payload.ToString(), null);
          
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
               
                result.objGetaparticipantDetailsFromGroupCallList = (List<GetaparticipantDetailsFromGroupCallList>)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Participants").ToString(), typeof(List<GetaparticipantDetailsFromGroupCallList>));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }

        }

        // Play a sound file into a Group Call that can be hered by all the participants
        public PlaySoundintogroupCallResult PlaySoundintoGroupCall(GroupCall GroupObj)
        {
            PlaySoundintogroupCallResult result = new PlaySoundintogroupCallResult();
            JObject httpResponse = null;

            if (GroupObj.File.Trim() == null || string.IsNullOrEmpty(GroupObj.File.Trim()) || string.IsNullOrEmpty(GroupObj.File.Trim()))
                throw new ArgumentNullException("Please Enter  File ");

            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");
            JObject payload = new JObject();

            payload.Add(new JProperty("File", GroupObj.File.Trim()));


            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.SendData(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/Play/", "POST", payload.ToString(), null);

            if (httpResponse.SelectToken("ApiId")!=null)
            result.ApiId = httpResponse.SelectToken("ApiId").ToString();
              
            
         
              
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
              
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }



        //Play a sound int oa Participant Call, without letting other participants here it by using GroupCallUUID and ParticipantId

        public PlaySoundintoParticipantCallinGroupCallResult PlaySoundintoParticipantCallinGroupCall(GroupCall GroupObj)
        {
            PlaySoundintoParticipantCallinGroupCallResult result = new PlaySoundintoParticipantCallinGroupCallResult();
            JObject httpResponse = null;

            if (GroupObj.File.Trim() == null || string.IsNullOrEmpty(GroupObj.File.Trim()) || string.IsNullOrEmpty(GroupObj.File.Trim()))
                throw new ArgumentNullException("Please Enter  File ");

            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");

            if (GroupObj.ParticipantId.Trim() == null || string.IsNullOrEmpty(GroupObj.ParticipantId.Trim()) || string.IsNullOrEmpty(GroupObj.ParticipantId.Trim()))
                throw new ArgumentNullException("Please Enter  ParticipantId");
            JObject payload = new JObject();
            payload.Add(new JProperty("File", GroupObj.File.Trim()));
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.SendData(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/Participants/" + GroupObj.ParticipantId.Trim() + "/Play/", "POST", payload.ToString(), null);
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
               
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }



        //Make all the participants Mute except Host (one who initiated the GroupCall) by using GroupCallUUID

        public GetMuteAllParticipantsinaGroupCallResult MuteAllParticipantsinaGroupCall(GroupCall GroupObj)
        {
            GetMuteAllParticipantsinaGroupCallResult result = new GetMuteAllParticipantsinaGroupCallResult();
            JObject httpResponse = null;
            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");
            JObject payload = new JObject();
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.PatchData(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/Mute/", "PATCH", payload.ToString(), null);
            result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
              

                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
           // result.FailedParticipantIds = httpResponse.SelectToken("FailedParticipantIds").ToString();

                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }
    




        //UnMute all participants who are in Mute state using GroupCallUUID

        public GetUnmuteMuteAllParticipantsinaGroupCall UnmuteMuteAllParticipantsinaGroupCall(GroupCall GroupObj)
        {
            GetUnmuteMuteAllParticipantsinaGroupCall result = new GetUnmuteMuteAllParticipantsinaGroupCall();
            JObject httpResponse = null;
            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");
            JObject payload = new JObject();
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.PatchData(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/UnMute/", "PATCH", payload.ToString(), null);
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                

                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
               // result.FailedParticipantIds = httpResponse.SelectToken("FailedParticipantIds").ToString();

                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }




        //Mute only a specific participant by using GroupCallUUID and ParticipantId

        public GetMuteParticipantInaGroupcall MuteParticipantsinaGroupCall(GroupCall GroupObj)
        {
            GetMuteParticipantInaGroupcall result = new GetMuteParticipantInaGroupcall();
            JObject httpResponse = null;

            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");

            if (GroupObj.ParticipantId.Trim() == null || string.IsNullOrEmpty(GroupObj.ParticipantId.Trim()) || string.IsNullOrEmpty(GroupObj.ParticipantId.Trim()))
                throw new ArgumentNullException("Please Enter  ParticipantId");

            JObject payload = new JObject();


            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.PatchData(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/Participants/" + GroupObj.ParticipantId.Trim() + "/Mute/", "PATCH", payload.ToString(), null);

            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
              
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }



        //UnMute participant who is in Mute state using GroupCallUUID and ParticipantId

        public GetUnmuteMuteParticipantsinaGroupCall UnmuteMuteParticipantsinaGroupCall(GroupCall GroupObj)
        {
            GetUnmuteMuteParticipantsinaGroupCall result = new GetUnmuteMuteParticipantsinaGroupCall();
            JObject httpResponse = null;

            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");

            if (GroupObj.ParticipantId.Trim() == null || string.IsNullOrEmpty(GroupObj.ParticipantId.Trim()) || string.IsNullOrEmpty(GroupObj.ParticipantId.Trim()))
                throw new ArgumentNullException("Please Enter  ParticipantId");
            JObject payload = new JObject();


            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.PatchData(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/Participants/" + GroupObj.ParticipantId.Trim() + "/UnMute/", "PATCH", payload.ToString(), null);

            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                

                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
             

                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }



        //Record the call conversation by using GroupCallUUID

        public GetStartRecordinginaGroupCallResult StartRecordinginaGroupCall(GroupCall GroupObj)
        {
            GetStartRecordinginaGroupCallResult result = new GetStartRecordinginaGroupCallResult();
            JObject httpResponse = null;

            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");

            if (GroupObj.FileFormat.Trim() == null || string.IsNullOrEmpty(GroupObj.FileFormat.Trim()) || string.IsNullOrEmpty(GroupObj.FileFormat.Trim()))
                throw new ArgumentNullException("Please Enter  FileFormat");
            JObject payload = new JObject();



            payload.Add(new JProperty("FileFormat", GroupObj.FileFormat.Trim()));

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.SendData(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/Recordings/", "POST", payload.ToString(), null);

            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
              
                result.objGetStartRecordinginaGroupCall = (GetStartRecordinginaGroupCall)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Recording").ToString(), typeof(GetStartRecordinginaGroupCall));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }



        }



        //Stop all running recordings on a group call in a single api request by using GroupCallUUID

        public GetStopRecordinginaGroupCallResult StopAllRecordinginaGroupCall(GroupCall GroupObj)
        {
            GetStopRecordinginaGroupCallResult result = new GetStopRecordinginaGroupCallResult();
            JObject httpResponse = null;
            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");

            JObject payload = new JObject();
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.PatchData(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/Recordings/", "PATCH", payload.ToString(), null);
           
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                
               // result.AffetcedRecordingUUIDs = httpResponse.SelectToken("AffetcedRecordingUUIDs").ToString();
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }


        }

        public GetStopRecordinginaGroupCallResult StopRecordinginGroupCall(GroupCall GroupObj)
        {
            GetStopRecordinginaGroupCallResult result = new GetStopRecordinginaGroupCallResult();
            JObject httpResponse = null;
            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");
            if (GroupObj.RecordingUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.RecordingUUID.Trim()) || string.IsNullOrEmpty(GroupObj.RecordingUUID.Trim()))
                throw new ArgumentNullException("Please Enter  RecordingUUID");
            JObject payload = new JObject();
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.PatchData(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/Recordings/" + GroupObj.RecordingUUID.Trim() + "/", "PATCH", payload.ToString(), null);
        
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }


        }
        //Get recording files that are under this Group Call by using GroupCallUUID and RecordingUUID

        public GetRecordingDetailsOfaGroupCallRecord GetRecordingDetailsOfaGroupCall(GroupCall groupObj)
        {
            GetRecordingDetailsOfaGroupCallRecord result = new GetRecordingDetailsOfaGroupCallRecord();
            JObject httpResponse = null;
            if (groupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(groupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(groupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");

            if (groupObj.RecordingUUID.Trim() == null || string.IsNullOrEmpty(groupObj.RecordingUUID.Trim()) || string.IsNullOrEmpty(groupObj.RecordingUUID.Trim()))
                throw new ArgumentNullException("Please Enter  RecordingUUID");

            JObject payload = new JObject();

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.GetData(this.BaseUrl + "GroupCalls/" + groupObj.GroupCallUUID.Trim() + "/Recordings/" + groupObj.RecordingUUID.Trim() + "/", "GET", payload.ToString(), null);




         
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
               
                result.objGetRecordingDetailsOfaGroupCallSingle = (GetSingleRecordingDetailsOfaGroupCallList)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Recording").ToString(), typeof(GetSingleRecordingDetailsOfaGroupCallList));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }

        //Get all recording files that are under this Group Call by using GroupCallUUID

        public GetRecordingDetailsOfaGroupCallRecord GetAllRecordingDetailsOfaGroupCall(GroupCall groupObj)
        {
            GetRecordingDetailsOfaGroupCallRecord result = new GetRecordingDetailsOfaGroupCallRecord();
            JObject httpResponse = null;
            if (groupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(groupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(groupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");

            JObject payload = new JObject();

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.GetData(this.BaseUrl + "GroupCalls/" + groupObj.GroupCallUUID.Trim() + "/Recordings/", "GET", payload.ToString(), null);

            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
           
            result.Message = httpResponse.SelectToken("Message").ToString();

            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
               
                result.objGetRecordingDetailsOfaGroupCallList = (List<GetRecordingDetailsOfaGroupCallList>)Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse.SelectToken("Recordings").ToString(), typeof(List<GetRecordingDetailsOfaGroupCallList>));
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }

        //Delete recording file by using GroupCallUUID and RecordingUUID

        public DeleteRecordingOfGroupCallResult DeleteRecordingOfGroupCall(GroupCall GroupObj)
        {
            DeleteRecordingOfGroupCallResult result = new DeleteRecordingOfGroupCallResult();
            JObject httpResponse = null;

            JObject payload = new JObject();
            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");
            if (GroupObj.RecordingUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.RecordingUUID.Trim()) || string.IsNullOrEmpty(GroupObj.RecordingUUID.Trim()))
                throw new ArgumentNullException("Please Enter  RecordingUUID");

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.DeleteData(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/Recordings/" + GroupObj.RecordingUUID.Trim() + "/", "DELETE", payload.ToString(), null);

            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
                result.Message = "Deleted";
                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
                result.Message = httpResponse.SelectToken("Message").ToString();
                return result;
            }
         
        }



        //Delete recording file by using GroupCallUUID
        public DeleteAllRecordingOfGroupCallResult DeleteAllRecordingOfGroupCall(GroupCall GroupObj)
        {
            DeleteAllRecordingOfGroupCallResult result = new DeleteAllRecordingOfGroupCallResult();
            JObject httpResponse = null;

            JObject payload = new JObject();
            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");


            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.DeleteData(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/Recordings/", "DELETE", payload.ToString(), null);
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
                result.Message = "Deleted";
                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();
                result.Message = httpResponse.SelectToken("Message").ToString();
                return result;
            }

            return result;
        }



        //Disconnect all participants from a Group Call using GroupCallUUID

        public GetAllDisconnectParticitantFromGroupCallResult DisconnectAllParticitantFromGroupCall(GroupCall GroupObj)
        {
            GetAllDisconnectParticitantFromGroupCallResult result = new GetAllDisconnectParticitantFromGroupCallResult();
            JObject httpResponse = null;


            JObject payload = new JObject();
            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");

            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.PatchDataDisconnectparticipitant(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/Hangup/", "PATCH", payload.ToString(), null);

            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
         
            result.Message = httpResponse.SelectToken("Message").ToString();

            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
              
                //result.AfftectedParticipantIds = httpResponse.SelectToken("AfftectedParticipantIds").ToString();
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }


        // Disconnect  participants from a Group Call using GroupCallUUID and ParticipantId
        public GetDisconnectParticitantFromGroupCallResult DisconnectParticitantFromGroupCall(GroupCall GroupObj)
        {
            GetDisconnectParticitantFromGroupCallResult result = new GetDisconnectParticitantFromGroupCallResult();
            JObject httpResponse = null;
            if (GroupObj.GroupCallUUID.Trim() == null || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()) || string.IsNullOrEmpty(GroupObj.GroupCallUUID.Trim()))
                throw new ArgumentNullException("Please Enter  GroupCallUUID");
            if (GroupObj.ParticipantId.Trim() == null || string.IsNullOrEmpty(GroupObj.ParticipantId.Trim()) || string.IsNullOrEmpty(GroupObj.ParticipantId.Trim()))
                throw new ArgumentNullException("Please Enter  ParticipantId");
            JObject payload = new JObject();
            this._httpClient.AuthorizationHeader(this._authKey, this._authToken);
            httpResponse = this._httpClient.PatchDataDisconnectparticipitant(this.BaseUrl + "GroupCalls/" + GroupObj.GroupCallUUID.Trim() + "/Participants/" + GroupObj.ParticipantId.Trim() + "/Hangup/", "PATCH", payload.ToString(), null);

            
            result.Message = httpResponse.SelectToken("Message").ToString();
            if (httpResponse.SelectToken("ApiId") != null)
                result.ApiId = httpResponse.SelectToken("ApiId").ToString();
            if (Convert.ToBoolean(httpResponse.SelectToken("Success")) == true)
            {
                
                result.Success = true;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();


                return result;
            }
            else
            {
                result.Success = false;
                result.StatusCode = httpResponse.SelectToken("StatusCode").ToString();

                return result;



            }
        }

        #region PROPERTIES
        private string BaseUrl
        {
            get
            {
                if (this._baseUrl.Length == 0)
                {
                    this._baseUrl = this._domain.EndsWith("/") ? this._domain : this._domain + "/";
                    this._baseUrl += this._version + "/Accounts/" + this._authKey + "/";
                }
                return this._baseUrl;
            }
        }
        #endregion
    }
}
