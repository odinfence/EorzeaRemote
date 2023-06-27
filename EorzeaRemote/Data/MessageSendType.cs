namespace EorzeaRemote.Data
{
    public enum MessageSendType
    {
        // ============== SENT TO SERVER ==============

        // Core - Issuing Commands
        IssueEmoteOrder,
        IssueSpeakOrder,
        IssueEmoteAndSpeakOrder,

        // Register Player
        RegisterNameRequest,

        // Authentication
        AuthorizationRequest,

        // ============== SENT TO CLIENT ==============

        // Core - Recieving Commands
        RecieveEmoteOrder,
        RecieveSpeakOrder,
        RecieveEmoteAndSpeakOrder,

        // Register Player
        NewPlayerRegistered,

        // Authentication
        AuthorizationResponse,

        // Disconnect Player
        PlayerDisconnected,

        // Errors
        GenericError,
    }
}
