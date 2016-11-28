using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
    public  class GetCreateNewGroup
    {
      private string _Id = string.Empty;

      private string _Name = string.Empty;

      private string _EndGroupCallOnExit = string.Empty;
      private string _StartGroupCallOnEnter = string.Empty;
      private string _TinyName = string.Empty;

      public string Id
      {
          get { return this._Id; }
          set { this._Id = value; }
      }


      public string Name
      {
          get { return this._Name; }
          set { this._Name = value; }
      }


      public string EndGroupCallOnExit
      {
          get { return this._EndGroupCallOnExit; }
          set { this._EndGroupCallOnExit = value; }
      }

      public string StartGroupCallOnEnter
      {
          get { return this._StartGroupCallOnEnter; }
          set { this._StartGroupCallOnEnter = value; }
      }
      public string TinyName
      {
          get { return this._TinyName; }
          set { this._TinyName = value; }
      }
      public List<Member> Members { get; set; }
      
  
    }

    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Mobile { get; set; }
    }


    public class GetCreateNewGroupResult:GenericResult
    {

        public GetCreateNewGroup objGetCreateNewGroup { get; set; }


    }
}
