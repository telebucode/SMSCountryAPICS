using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
   public class CreateBulkCallResult:GenericResult
    {
       private string _CallUUIDs;

       public string CallUUID
        {
            get { return this._CallUUIDs; }
            set { this._CallUUIDs = value; }
        }
       public List<string> CallUUIDs
       {
           get;
           set;
       }
    }
}
