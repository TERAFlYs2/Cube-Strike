using System.Threading.Tasks;
using Fusion;
using UnityEngine;

public class SessionConnectionHandler : INetworkStartGame
{
	private readonly NetworkRunner _runner;
	private readonly NetworkSceneManagerDefault _sceneManager;
	private readonly SessionConnectionConfig _config;

	//public event Action<string> OnStartGameResult;
	
	public SessionConnectionHandler(NetworkRunner runner, NetworkSceneManagerDefault sceneManager, SessionConnectionConfig config)
	{
		_runner = runner;
		_sceneManager = sceneManager;
		
		_config = config;
	}
	
	public async Task<StartGameResult> StartGameAsync(string sessionName, DisplayTextHandler displayTextHandler) 
	{
		LoadingScreen.Instance.Show();
		
		StartGameArgs startGameArgs = new ()
		{
			GameMode = _config.GameMode,
			SessionName = sessionName,
			SceneManager = _sceneManager, 
			CustomLobbyName = _config.LobbyNameDefault,
			PlayerCount = _config.MaxCountPlayers
		};
	  	
	  	var startGameResult = await _runner.StartGame(startGameArgs);
	  	
	  	DisplayTextType displayTextType;
	  	string messageInfo;
	  	
	  	if (startGameResult.Ok) 
	  	{
	  	    messageInfo = "Успешное подключение и создание сессии";
	  	    displayTextType = DisplayTextType.Text;
	  	    
	  	    if (_runner.IsSceneAuthority) 
    		{
        		await _runner.LoadScene(SceneRef.FromIndex(1));
        		Debug.LogError("Сцена загрузилась");
    		}
			else
				Debug.LogWarning("Этот клиент не имеет прав на загрузку сцены");
	  	}
	  	else 
	  	{
	  	    messageInfo = "Не удалось подключиться к серверу или создать сессиию";
	  	    displayTextType = DisplayTextType.ErrorText;
	  	}

		PrintConnectionInfo(messageInfo, displayTextType, displayTextHandler);
		
		LoadingScreen.Instance.Hide();
		
		return startGameResult;
	}
	
	private void PrintConnectionInfo(string message, DisplayTextType displayTextType, DisplayTextHandler displayTextHandler) 
	{
		Debug.Log(message);
		
		displayTextHandler?.DisplayText(message, displayTextType, 3f);
	}
	
}
