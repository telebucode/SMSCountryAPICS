using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{


    public class fff
    {
        public List<GetAllParticipantsFromGroupCallDetailsResult> Participants { get; set; }
    }





    public class CallParticipant
        {
            public string CallStatus { get; set; }
            public string AnswerTime { get; set; }
            public string EndTime { get; set; }
            public string EndReason { get; set; }
            public string Cost { get; set; }
            public int Pulse { get; set; }
            public double PricePerPulse { get; set; }
        }

    public class GetAllParticipantsFromGroupCallDetailsResult : GenericResult
        {
            public string Name { get; set; }
            public string Number { get; set; }
            public List<CallParticipant> Calls { get; set; }
        }


    
}
