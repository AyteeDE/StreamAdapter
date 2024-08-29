using AyteeDE.StreamAdapter.Core.Entities;

namespace AyteeDE.StreamAdapter.Core.Communication;

public interface IStreamAdapter
{
    public Task<bool> ConnectAsync();
    //Scenes Requests
    public Task<List<Scene>> GetScenes();
    public Task<Scene> GetCurrentProgramScene();
    public Task<bool> SetCurrentProgramScene(Scene scene);
    //Scenes Events
    public event EventHandler<Scene> OnCurrentProgramSceneChanged;
}
