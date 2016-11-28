using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
  public  class Call
    {
        private string _MessageUrl = string.Empty;

        private string _Number = string.Empty;

        private string _AnswerUrl = string.Empty;
       
        private string _callUUID = string.Empty;
        private string _Startdate = string.Empty;

        private string _Enddate = string.Empty;
        public string MessageUrl
        {
            get { return this._MessageUrl; }
            set { this._MessageUrl = value; }
        }
      

        public string Number
        {
            get { return this._Number; }
            set { this._Number = value; }
        }



        public string AnswerUrl
        {
            get { return this._AnswerUrl; }
            set { this._AnswerUrl = value; }
        }

        public string callUUID
        {
            get { return this._callUUID; }
            set { this._callUUID = value; }
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
