using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
  public  class GetSmsCollectionDetailsList
    {
        public string ToolName { get; set; }

        public string Tool{ get; set; }
        public string Number { get; set; }
        public string MessageUUID { get; set; }
        public string Text { get; set; }
        public string SenderId { get; set; }
        public string Cost { get; set; }
        public string Status { get; set; }
        public string StatusTime { get; set; }
     
  

        }


  public class GetSmsCollectionDetailsResult : GenericResult
  {
      public string Next { get; set; }
      public GetSmsCollectionDetailsList objSinlgeSMS { get; set; }
      public List<GetSmsCollectionDetailsList> objGetSmsCollectionDetailsList { get; set; }

  }

  
}
