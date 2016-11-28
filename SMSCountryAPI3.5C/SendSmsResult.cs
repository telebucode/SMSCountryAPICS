using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
    public class SendSmsResult : GenericResult
    {
        private string _MessageUUID = string.Empty;

        public string MessageUUID
        {
            get { return this._MessageUUID; }
            set { this._MessageUUID = value; }
        }

        public List<string> MessageUUIDs
        {
            get;
            set;
        }

          public string BatchId
        {
            get;
            set;
        }
   

    
    }

  

    

}
