using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
   public class CreateCallResult:GenericResult
    {
       private string _CallUUID = string.Empty;
       
       public string CallUUID
        {
            get { return this._CallUUID; }
            set { this._CallUUID = value; }
        }
    }
}
