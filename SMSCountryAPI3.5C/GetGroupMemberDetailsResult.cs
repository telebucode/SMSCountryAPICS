using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
   public class GetGroupMemberDetailsResult:GenericResult
    {


       public GetGroupMemberDetailsList objGetGroupMemberDetailsSingle{ get; set; }

       public List<GetGroupMemberDetailsList> objGetGroupMemberDetailsList { get; set; }

    }


   public class GetGroupMemberDetailsList 
   {


       public string Id { get; set; }
       public string Name { get; set; }
       public string Number { get; set; }


   }
}
