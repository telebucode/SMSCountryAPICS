using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
   public class CallList
    {

      
            public string CallStatus { get; set; }
            public string EndReason { get; set; }
            public object AnswerTime { get; set; }
            public string EndTime { get; set; }
            public string Cost { get; set; }
            public int Pulse { get; set; }
            public double PricePerPulse { get; set; }
        

       
    }
   public class GetaparticipantDetailsFromGroupCallList
   {
       public string Name { get; set; }
       public string Number { get; set; }
       public List<CallList> Calls { get; set; }
   }

   public class GetaparticipantDetailsFromGroupCallResult : GenericResult
   {
       public List<GetaparticipantDetailsFromGroupCallList> objGetaparticipantDetailsFromGroupCallList { get; set; }
       public GetaparticipantDetailsFromGroupCallList objGetaparticipantDetailsFromGroupCallSingle { get; set; }
   }
   
}
