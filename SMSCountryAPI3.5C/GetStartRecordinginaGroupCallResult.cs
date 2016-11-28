using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
 
       public class GetStartRecordinginaGroupCallResult:GenericResult
       {
           public GetStartRecordinginaGroupCall objGetStartRecordinginaGroupCall { get; set; }
           public List<GetStartRecordinginaGroupCall> objGetStartRecordinginaGroupCallList { get; set; }
       }


       public class GetStartRecordinginaGroupCall
       {
           public string RecordingUUID { get; set; }
           public string Url { get; set; }
       }
     
     
    
}
