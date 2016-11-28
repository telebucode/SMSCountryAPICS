using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
  public  class GroupCall:GenericResult
    {


       
        private string _Name = string.Empty;

        private string _WelcomeSound = string.Empty;

        private string _WaitSound = string.Empty;

        private string _StartGroupCallOnEnter = string.Empty;

        private string _EndGroupCallOnExit = string.Empty;

        private string _AnswerUrl = string.Empty;
        private string _GroupCallUUID = string.Empty;

        private string _ParticipantId = string.Empty;

        private string _File= string.Empty;
        private string _FileFormat = string.Empty;

        private string _RecordingUUID = string.Empty;
     
        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        public string WelcomeSound
        {
            get { return this._WelcomeSound; }
            set { this._WelcomeSound = value; }
        }

        public string WaitSound
        {
            get { return this._WaitSound; }
            set { this._WaitSound = value; }
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

        public string AnswerUrl
        {
            get { return this._AnswerUrl; }
            set { this._AnswerUrl = value; }
        }
        public string GroupCallUUID
        {
            get { return this._GroupCallUUID; }
            set { this._GroupCallUUID = value; }
        }

        public string ParticipantId
        {
            get { return this._ParticipantId; }
            set { this._ParticipantId = value; }
        }


        public string File
        {
            get { return this._File; }
            set { this._File = value; }
        }
        public string FileFormat
        {
            get { return this._FileFormat; }
            set { this._FileFormat = value; }
        }


        public string RecordingUUID
        {
            get { return this._RecordingUUID; }
            set { this._RecordingUUID = value; }
        }

        public List<Participatants> Participants { get; set; }

    }


    public class Participatants
    {
        public string Name { get; set; }
        public string Number { get; set; }
    }



}
