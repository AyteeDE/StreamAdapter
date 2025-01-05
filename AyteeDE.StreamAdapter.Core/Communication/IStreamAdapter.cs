using AyteeDE.StreamAdapter.Core.Entities;

namespace AyteeDE.StreamAdapter.Core.Communication;

public interface IStreamAdapter
{
    public Task<bool> ConnectAsync();
    //Scenes Requests
    public Task<List<Scene>> GetScenes();
    public Task<Scene> GetCurrentProgramScene();
    public Task SetCurrentProgramScene(Scene scene);
    //Stream Requests
    public Task<OutputStatusInformation> GetStreamStatus();
    /// <summary>
    /// Toggles the stream status.
    /// </summary>
    /// <returns>True is a running stream.</returns>
    public Task<bool> ToggleStream();
    public Task StartStream();
    public Task StopStream();
    public Task SendStreamCaption(string caption);
    //Record Requests
    public Task<OutputStatusInformation> GetRecordStatus();
    /// <summary>
    /// Toggles the record status.
    /// </summary>
    /// <returns>True is a running record.</returns>
    public Task<bool> ToggleRecord();
    public Task StartRecord();
    public Task StopRecord();
    /// <summary>
    /// Toggles the record pause status.
    /// </summary>
    /// <returns>True is a paused record.</returns>
    public Task<bool> ToggleRecordPause();
    public Task PauseRecord();
    public Task ResumeRecord();
    public Task SplitRecordFile();
    public Task CreateRecordChapter(string chapterName);
    //Scenes Events
    public event EventHandler<Scene> OnCurrentProgramSceneChanged;
}
