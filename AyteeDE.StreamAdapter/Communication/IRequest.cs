using AyteeDE.StreamAdapter.Entities.StreamAdapter;

namespace AyteeDE.StreamAdapter.Communication;

public interface IRequest
{
    //Scenes Requests
    public Task<List<Scene>> GetScenes();
    public Task<Scene> GetCurrentProgramScene();
    public Task<bool> SetCurrentProgramScene(Scene scene);
    //Scenes Events
    public event EventHandler<Scene> OnCurrentProgramSceneChanged;
}
