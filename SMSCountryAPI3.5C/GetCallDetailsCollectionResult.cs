using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
  public   class GetCallDetailsCollectionResult:GenericResult
    {

      public GetCallDetailsCollectionList objGetCallDetailsCollectionSingle { get; set; }
      public List<GetCallDetailsCollectionList> objGetCallDetailsCollectionList { get; set; }
      private string _Next = string.Empty;
      public string Next
      {
          get { return this._Next; }
          set { this._Next = value; }
      }
    }

  public class GetCallDetailsCollectionList 
  {
      public string test { get;set; }
      private string _Number = string.Empty;
      private string _CallUUID = string.Empty;
      private string _CallerId = string.Empty;
      private string _Status = string.Empty;
      private string _RingTime = string.Empty;
      private string _AnswerTime = string.Empty;
      private string _EndTime = string.Empty;
      private string _EndReason = string.Empty;
      private string _Cost = string.Empty;
      private string _Direction = string.Empty;
      private string _Pulse = string.Empty;
      private string _Pulses = string.Empty;
      private string _PricePerPulse = string.Empty;
   

      public string Number
      {
          get { return this._Number; }
          set { this._Number = value; }
      }
      public string CallUUID
      {
          get { return this._CallUUID; }
          set { this._CallUUID = value; }
      }
      public string CallerId
      {
          get { return this._CallerId; }
          set { this._CallerId = value; }
      }

      public string Status
      {
          get { return this._Status; }
          set { this._Status = value; }
      }
      public string RingTime
      {
          get { return this._RingTime; }
          set { this._RingTime = value; }
      }
      public string AnswerTime
      {
          get { return this._AnswerTime; }
          set { this._AnswerTime = value; }
      }

      public string EndTime
      {
          get { return this._EndTime; }
          set { this._EndTime = value; }
      }
      public string EndReason
      {
          get { return this._EndReason; }
          set { this._EndReason = value; }
      }
      public string Cost
      {
          get { return this._Cost; }
          set { this._Cost = value; }
      }

      public string Direction
      {
          get { return this._Direction; }
          set { this._Direction = value; }
      }

      public string Pulses
      {
          get { return this._Pulses; }
          set { this._Pulses = value; }
      }
      public string Pulse
      {
          get { return this._Pulse; }
          set { this._Pulse = value; }
      }
      public string PricePerPulse
      {
          get { return this._PricePerPulse; }
          set { this._PricePerPulse = value; }
      }

      

  }
}
