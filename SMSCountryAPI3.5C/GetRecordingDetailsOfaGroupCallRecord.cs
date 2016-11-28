using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
  public  class GetRecordingDetailsOfaGroupCallRecord:GenericResult
    {
      public List<GetRecordingDetailsOfaGroupCallList> objGetRecordingDetailsOfaGroupCallList { get; set; }
      public GetSingleRecordingDetailsOfaGroupCallList objGetRecordingDetailsOfaGroupCallSingle { get; set; }
    }

  public class GetRecordingDetailsOfaGroupCallList
  {
      public string UUID { get; set; }
      public string Url { get; set; }
  }

  public class GetSingleRecordingDetailsOfaGroupCallList
  {
      public string RecordingID { get; set; }
      public string Url { get; set; }
  }

}
