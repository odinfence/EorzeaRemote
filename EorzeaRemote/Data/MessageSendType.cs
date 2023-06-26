namespace EorzeaRemote.Data
{
    public enum MessageSendType
    {
        // Core - Issuing Commands
        IssueEmoteOrder,
        IssueSpeakOrder,
        IssueEmoteAndSpeakOrder,

        // Core - Recieving Commands
        RecieveEmoteOrder,
        RecieveSpeakOrder,
        RecieveEmoteAndSpeakOrder,

        // UI
        RegisterName,

        // Authentication
        SeekAuthorization,
        GrantedAuthorization
    }
}
