using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
    public class GenericResult
    {
        private string _apiId = string.Empty;
        private bool _success = false;
        private string _message = string.Empty;
        //private System.Net.HttpStatusCode _statusCode;
        private string _statusCode;
        private byte _subStatusCode = 0;

      
        public string ApiId
        {
            get { return this._apiId; }
            set { this._apiId = value; }
        }
        public bool Success
        {
            get { return this._success; }
            set { this._success = value; }
        }
        public string Message
        {
            get { return this._message; }
            set { this._message = value; }
        }
        //public System.Net.HttpStatusCode StatusCode
        //{
        //    get { return this._statusCode; }
        //    set { this._statusCode = value; }
        //}

        public string StatusCode
        {
            get { return this._statusCode; }
            set { this._statusCode = value; }
        }
        public byte SubStatusCode
        {
            get { return this._subStatusCode; }
            set { this._subStatusCode = value; }
        }
    }
}
