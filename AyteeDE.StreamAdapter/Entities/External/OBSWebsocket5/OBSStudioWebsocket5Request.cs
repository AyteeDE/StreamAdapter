using AyteeDE.StreamAdapter.Communication;
using AyteeDE.StreamAdapter.Configuration;
using AyteeDE.StreamAdapter.Entities.StreamAdapter;

namespace AyteeDE.StreamAdapter.Entities.External;

public class OBSStudioWebsocket5Request : IRequest
{
    private readonly EndpointConfiguration _configuration;
    public OBSStudioWebsocket5Request(EndpointConfiguration configuration)
    {
        _configuration = configuration;
    }
    #region Interface Implementation
    public Task<Scene> GetCurrentProgramScene()
    {
        throw new NotImplementedException();
    }

    public Task<List<Scene>> GetScenes()
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetCurrentProgramScene(Scene scene)
    {
        throw new NotImplementedException();
    }
    #endregion
}
