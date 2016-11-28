using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
   public class GetGroupDetails:GenericResult
    {

       public List<GetGroupDetailsList> objGetGroupDetailsList { get; set; }
       public GetGroupDetailsList objGetGroupDetailsSingle { get; set; }
     

    }



   public class GetGroupDetailsList 
   {


       public string Id { get; set; }
       public string Name { get; set; }
       public string TinyName { get; set; }
       public string StartGroupCallOnEnter { get; set; }
       public string EndGroupCallOnExit { get; set; }

   }
}
