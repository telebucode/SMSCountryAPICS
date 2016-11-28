using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
    public class Group
    {
       
        private string _Name = string.Empty;

        private string _TinyName = string.Empty;

        private string _StartGroupCallOnEnter = string.Empty;

        private string _EndGroupCallOnExit = string.Empty;

        private string _Members = string.Empty;

        private string _groupId = string.Empty;
        private string _MemberId = string.Empty;

        private string _Number = string.Empty;


        //private string _MembersName = string.Empty;

        //private string _MembersNumber = string.Empty;
        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        public string Number
        {
            get { return this._Number; }
            set { this._Number = value; }
        }

        public string TinyName
        {
            get { return this._TinyName; }
            set { this._TinyName = value; }
        }
        public string StartGroupCallOnEnter
        {
            get { return this._StartGroupCallOnEnter; }
            set { this._StartGroupCallOnEnter = value; }
        }
        public string EndGroupCallOnExit
        {
            get { return this._EndGroupCallOnExit; }
            set { this._EndGroupCallOnExit = value; }
        }

        public string MemberId
        {
            get { return this._MemberId; }
            set { this._MemberId = value; }
        }
        public List<Members> Members { get; set; }

        public string groupId
        {
            get { return this._groupId; }
            set { this._groupId = value; }
        }


    }
       //public string Members
       // {
       //     get { return this._Members; }
       //     set { this._Members = value; }
       //}
       // public string MembersName
       // {
       //     get { return this._MembersName; }
       //     set { this._MembersName = value; }
       // }

       //     public string MembersNumber
       // {
       //     get { return this._MembersNumber; }
       //     set { this._MembersNumber = value; }
       // }

       // }
    public class Members
    {
        public string Name { get; set; }
        public string Number { get; set; }
    }
 }

        