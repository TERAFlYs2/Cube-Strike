using System.Threading.Tasks;
using Fusion;
public interface INetworkStartGame 
{
    public Task<StartGameResult> StartGameAsync(string sessionName, DisplayTextHandler displayTextHandler);
}
