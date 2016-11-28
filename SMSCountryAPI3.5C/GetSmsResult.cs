using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
    public class GetSmsResult : GenericResult
    {
     
        private string _ApiId = string.Empty;
        private string _Message = string.Empty;   
        private string _MessageUUID = string.Empty;
        private string _Number = string.Empty;
        private string _Tool = string.Empty;
        private string _SenderId = string.Empty;
        private string _Text = string.Empty;
        private string _Status = string.Empty;
        private string _StatusTime = string.Empty;
        private string _Cost = string.Empty;
        private bool _Success;
      
        public string MessageUUID
        {
            get { return this._MessageUUID; }
            set { this._MessageUUID = value; }
        }
        public string ApiId
        {
            get { return this._ApiId; }
            set { this._ApiId = value; }
        }
        public string Message
        {
            get { return this._Message; }
            set { this._Message = value; }
        }
     
        public string Number
        {
            get { return this._Number; }
            set { this._Number = value; }
        }
        public string Tool
        {
            get { return this._Tool; }
            set { this._Tool = value; }
        }
        public string SenderId
        {
            get { return this._SenderId; }
            set { this._SenderId = value; }
        }

        public string Text
        {
            get { return this._Text; }
            set { this._Text = value; }
        }
        public string Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
        public string StatusTime
        {
            get { return this._StatusTime; }
            set { this._StatusTime = value; }
        }

        public string Cost
        {
            get { return this._Cost; }
            set { this._Cost = value; }
        }

        public bool Success
        {
            get { return this._Success; }
            set { this._Success = value; }
        }

    }

  
}
