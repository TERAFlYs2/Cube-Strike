using System.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionDisconnectHandler : INetworkFinishGame
{
    private readonly NetworkRunner _runner;
    
    private IGameMenuNotifiers _gameMenuNotifiers;
    
    public IGameMenuNotifiers GameMenuNotifiers 
    {
        get => _gameMenuNotifiers;
        
        set 
        {
            if (_gameMenuNotifiers == null) 
            {
                _gameMenuNotifiers = value;
                
                _gameMenuNotifiers.OnLeaveSessionButtonClickedEvent += FinishGame;
            }
            else 
            {
                _gameMenuNotifiers.OnLeaveSessionButtonClickedEvent -= FinishGame;
                
                _gameMenuNotifiers = value;
                
                _gameMenuNotifiers.OnLeaveSessionButtonClickedEvent += FinishGame;
            }
        }
    }
    public SessionDisconnectHandler(NetworkRunner runner)
    {
        _runner = runner;
    }

    public async Task FinishGame()
    {
        if (_runner.IsInSession && _runner.IsConnectedToServer) 
        {
            if (_runner.IsClient) 
            {
                LoadingScreen.Instance.Show();
                await _runner.Shutdown();
                LoadingScreen.Instance.Hide();
                await SceneManager.LoadSceneAsync(0);
                
            }
        }
    }
}
