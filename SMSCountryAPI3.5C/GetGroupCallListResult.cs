using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
   public class GetGroupCallListResult:GenericResult
    {



       public List<GetGroupCallList> objGetGroupCallList { get; set; }

       public GetGroupCallList objGetGroupCallSingle { get; set; }
    }

   public class GetGroupCallList
   {



       public string GroupCallUUID { get; set; }
       public string Name { get; set; }
       public string WelcomeSound { get; set; }
       public string WaitSound { get; set; }
       public string StartGroupCallOnEnter { get; set; }
       public string EndGroupCallOnExit { get; set; }

   }

}
