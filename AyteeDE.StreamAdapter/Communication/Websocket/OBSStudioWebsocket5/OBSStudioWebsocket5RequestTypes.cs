using System;

namespace AyteeDE.StreamAdapter.Communication.Websocket.OBSStudioWebsocket5;

public static class OBSStudioWebsocket5RequestTypes
{
    //General Requests
    public static string GetVersion => "GetVersion";
    public static string GetStats => "GetStats";
    public static string BroadcastCustomEvent => "BroadcastCustomEvent";
    public static string CallVendorRequest => "CallVendorRequest";
    public static string GetHotkeyList => "GetHotkeyList";
    public static string TriggerHotkeyByName => "TriggerHotkeyByName";
    public static string TriggerHotkeyByKeySequence => "TriggerHotkeyByKeySequence";
    public static string Sleep => "Sleep";
    //Scenes Requests
    public static string GetSceneList => "GetSceneList";
    public static string GetGroupList => "GetGroupList";
    public static string GetCurrentProgramScene => "GetCurrentProgramScene";
    public static string SetCurrentProgramScene => "SetCurrentProgramScene";
    public static string GetCurrentPreviewScene => "GetCurrentPreviewScene";
    public static string SetCurrentPreviewScene => "SetCurrentPreviewScene";
    public static string CreateScene => "CreateScene";
    public static string RemoveScene => "RemoveScene";
    public static string SetSceneName => "SetSceneName";
    public static string GetSceneSceneTransitionOverride => "GetSceneSceneTransitionOverride";
    public static string SetSceneSceneTransitionOverride => "SetSceneSceneTransitionOverride";
}
