using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
    public class Sms
    {
        private string _text = string.Empty;
        private string _number = string.Empty;
        private string _numbers = string.Empty;
        private string _Startdate = string.Empty;

        private string _Enddate = string.Empty;
        private string _senderId = string.Empty;
        private string _drNotifyUrl = string.Empty;
        private string _drNotifyMethod = string.Empty;
        private string _messageUUID = string.Empty;
        private string _tool = string.Empty;
        private string _status = string.Empty;
        private long _statusTime = 0;
        private string _cost = string.Empty;
    
      

        public Sms()
        { }
        public Sms(string text, string number, string senderId = "", string drNotifyUrl = "", string drNotifyMethod = "")
        {
            this._text = text;
            this._number = number;
            this._senderId = senderId;
            this._drNotifyUrl = drNotifyUrl;
            this._drNotifyMethod = drNotifyMethod;
        }
        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }
        public string Number
        {
            get { return this._number; }
            set { this._number = value; }
        }

        public string Numbers
        {
            get { return this._numbers; }
            set { this._numbers = value; }
        }
        public string SenderId
        {
            get { return this._senderId; }
            set { this._senderId = value; }
        }
        public string DRNotifyUrl
        {
            get { return this._drNotifyUrl; }
            set { this._drNotifyUrl = value; }
        }
        public string DRNotifyMethod
        {
            get { return this._drNotifyMethod; }
            set { this._drNotifyMethod = value; }
        }
        public string MessageUUID
        {
            get { return this._messageUUID; }
            set { this._messageUUID = value; }
        }
        public string Tool
        {
            get { return this._tool; }
            set { this._tool = value; }
        }
        public string Status
        {
            get { return this._status; }
            set { this._status = value; }
        }
        public long StatusTime
        {
            get { return this._statusTime; }
        }
        public string Cost
        {
            get { return this._cost; }
            set { this._cost = value; }
        }

        public string Startdate
        {
            get { return this._Startdate; }
            set { this._Startdate = value; }
        }

        public string Enddate
        {
            get { return this._Enddate; }
            set { this._Enddate = value; }
        }

 

    }
}
