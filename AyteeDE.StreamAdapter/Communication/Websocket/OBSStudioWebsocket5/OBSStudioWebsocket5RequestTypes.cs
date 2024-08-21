namespace AyteeDE.StreamAdapter.Communication.Websocket;

public static class OBSStudioWebsocket5RequestTypes
{
    //General Requests
    public const string GetVersion = "GetVersion";
    public const string GetStats = "GetStats";
    public const string BroadcastCustomEvent = "BroadcastCustomEvent";
    public const string CallVendorRequest = "CallVendorRequest";
    public const string GetHotkeyList = "GetHotkeyList";
    public const string TriggerHotkeyByName = "TriggerHotkeyByName";
    public const string TriggerHotkeyByKeySequence = "TriggerHotkeyByKeySequence";
    public const string Sleep = "Sleep";
    //Scenes Requests
    public const string GetSceneList = "GetSceneList";
    public const string GetGroupList = "GetGroupList";
    public const string GetCurrentProgramScene = "GetCurrentProgramScene";
    public const string SetCurrentProgramScene = "SetCurrentProgramScene";
    public const string GetCurrentPreviewScene = "GetCurrentPreviewScene";
    public const string SetCurrentPreviewScene = "SetCurrentPreviewScene";
    public const string CreateScene = "CreateScene";
    public const string RemoveScene = "RemoveScene";
    public const string SetSceneName = "SetSceneName";
    public const string GetSceneSceneTransitionOverride = "GetSceneSceneTransitionOverride";
    public const string SetSceneSceneTransitionOverride = "SetSceneSceneTransitionOverride";
}

