namespace EorzeaRemote.Data
{
    public enum MessageSendType
    {
        // ============== SERVER ==============

        // Core - Issuing Commands
        IssueEmoteOrder,
        IssueSpeakOrder,
        IssueEmoteAndSpeakOrder,

        // Register Player
        RegisterName,

        // Authentication
        SeekAuthorization,

        // ============== CLIENT ==============

        // Core - Recieving Commands
        RecieveEmoteOrder,
        RecieveSpeakOrder,
        RecieveEmoteAndSpeakOrder,

        // Register Player
        NewPlayerRegistered,

        // Authentication
        GrantedAuthorization,

        // Disconnect Player
        PlayerDisconnected,
    }
}
