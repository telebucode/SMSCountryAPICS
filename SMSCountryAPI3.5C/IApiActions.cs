using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCountryApi
{
    interface IApiActions
    {
        SendSmsResult SendSms(Sms smsObj);

        GetSmsCollectionDetailsResult GetSmsDetails(Sms smsObj);

        GetSmsCollectionDetailsResult GetSmsCollection(Sms smsObj);

        SendSmsResult SendBulkSms(Sms smsObj);

        CreateCallResult CreateCall(Call callObj);

        CreateBulkCallResult CreateBulkCall(Call callObj);

        GetCallDetailsCollectionResult GetCallList(Call callObj);


        GetCallDetailsCollectionResult GetCallDetails(Call callObj);

        DisconnectCallResult DisconnectCall(Call callObj);
        GetCreateNewGroupResult CreateNewGroup(Group groupObj);


        GetGroupDetails GetGroupDetails(Group groupObj);


        GetGroupDetails GetGroupCollection(Group groupObj);

        GetUpdateGroupResult UpdateGroup(Group GroupObj);

        DeleteGroupDetails DeleteGroup(Group GroupObj);

        GetDeleteGroupMemberDetails DeleteMemberfromGroup(Group GroupObj);

        GetGroupMemberDetailsResult GetMemberDetails(Group groupObj);


        GetGroupMemberDetailsResult GetallMemberodGroup(Group groupObj);


        GetUpdateMemberResult UpdateMemberDetails(Group GroupObj);


        GetGroupMemberDetailsResult AddMembertoExistingGroup(Group GroupObj);


        GetCreateGroupCallResult CreateaGroupCall(GroupCall groupObj);


        GetGroupCallListResult GetGroupCallList();


        GetGroupCallListResult GetGroupCallDetails(GroupCall groupObj);


        GetaparticipantDetailsFromGroupCallResult GetaParticipantDetailsFromGroupCall(GroupCall groupObj);


        GetaparticipantDetailsFromGroupCallResult GetAllParticipantDetailsFromGroupCall(GroupCall groupObj);

        PlaySoundintogroupCallResult PlaySoundintoGroupCall(GroupCall GroupObj);

        PlaySoundintoParticipantCallinGroupCallResult PlaySoundintoParticipantCallinGroupCall(GroupCall GroupObj);

        GetMuteAllParticipantsinaGroupCallResult MuteAllParticipantsinaGroupCall(GroupCall GroupObj);

        GetUnmuteMuteAllParticipantsinaGroupCall UnmuteMuteAllParticipantsinaGroupCall(GroupCall GroupObj);

        GetMuteParticipantInaGroupcall MuteParticipantsinaGroupCall(GroupCall GroupObj);

        GetUnmuteMuteParticipantsinaGroupCall UnmuteMuteParticipantsinaGroupCall(GroupCall GroupObj);


        GetStartRecordinginaGroupCallResult StartRecordinginaGroupCall(GroupCall GroupObj);


        GetStopRecordinginaGroupCallResult StopAllRecordinginaGroupCall(GroupCall GroupObj);


        GetRecordingDetailsOfaGroupCallRecord GetRecordingDetailsOfaGroupCall(GroupCall groupObj);


        GetRecordingDetailsOfaGroupCallRecord GetAllRecordingDetailsOfaGroupCall(GroupCall groupObj);

        DeleteRecordingOfGroupCallResult DeleteRecordingOfGroupCall(GroupCall GroupObj);

        DeleteAllRecordingOfGroupCallResult DeleteAllRecordingOfGroupCall(GroupCall GroupObj);

        GetAllDisconnectParticitantFromGroupCallResult DisconnectAllParticitantFromGroupCall(GroupCall GroupObj);

        GetDisconnectParticitantFromGroupCallResult DisconnectParticitantFromGroupCall(GroupCall GroupObj);
    }
}
