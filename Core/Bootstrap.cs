using System;
using System.Threading.Tasks;
using Fusion;
using Fusion.Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

public sealed class Bootstrap : MonoBehaviour
{
	private const double SimulatedLoadingTime = 3;
	private static Bootstrap _instance;
	
	[Header("Network")]
	[SerializeField] private PhotonAppSettings _photonAppSettings;
	[SerializeField] private SessionConnectionConfig _sessionConnectionConfig;
	
	[Header("Client")]
	[SerializeField] private MenuConfig _menuConfig;
	
	private void Awake() 
	{
		if (_instance != null) 
		{
			return;
		}
		
    	_instance = this;

		ApplyWindowSettings();
    	_ = StartGameAsync();
	}
	
	private void ApplyWindowSettings() 
	{
	    Application.runInBackground = true;
		QualitySettings.vSyncCount = 0;                  
    	Application.targetFrameRate = 300;
	}
	private async Task StartGameAsync() 
	{	
		LoadingScreen.Instance.Show();
		Debug.Log("Загрузка...");	
		
		await Init_ServicesAsync();
		
		LoadingScreen.Instance.Hide();
		Debug.Log("Загрузка завершена...");
	}
	
	private async Task Init_ServicesAsync() 
	{
		await Task.Delay(TimeSpan.FromSeconds(SimulatedLoadingTime));
		
		
		var runner = Init_NetworkRunner();
		
		var netSceneManager = Init_NetworkSceneManager(runner);
		
		var menuController = Init_MenuController();
		
		var firebaseConnector = Init_FirebaseConnector();
		
		var sessionConnectionHandler = Init_SessionConnectionHandler(runner, netSceneManager);
		var mainMenu = Init_MainMenu();
		
		
		var authenticationHandler = Init_AuthenticationHandler(firebaseConnector);

		
		
		var startGameMenu = Init_StartGameMenu(sessionConnectionHandler);
		
		
		authenticationHandler.MainMenuNotifiers = mainMenu;
	}
	
	private NetworkRunner Init_NetworkRunner() 
	{
		GameObject networkRunner = new GameObject(typeof(NetworkRunner).Name);
		
		return networkRunner.AddComponent<NetworkRunner>();
	}
	
	private NetworkSceneManagerDefault Init_NetworkSceneManager(NetworkRunner runner) 
	{
	    return runner.AddComponent<NetworkSceneManagerDefault>();
	}
	private MenuController Init_MenuController() 
	{
		MenuController menuController = new MenuController(_menuConfig);
		menuController.OpenDefaultMenu();
		
		return menuController;
	}
	
	private SessionConnectionHandler Init_SessionConnectionHandler(NetworkRunner runner, NetworkSceneManagerDefault networkSceneManagerDefault) 
	{
		SessionConnectionHandler sessionConnectionHandler = new (runner, networkSceneManagerDefault, _sessionConnectionConfig);
		
	
	    return sessionConnectionHandler;
	}
	
	private StartGameMenu Init_StartGameMenu(SessionConnectionHandler sessionConnectionHandler) 
	{
	    var startGameMenu = _menuConfig.GetMenuByType<StartGameMenu>();
	    
		startGameMenu.NetworkStartGame = sessionConnectionHandler;
		
		return startGameMenu;
	}
	private MainMenu Init_MainMenu() 
	{
	    var mainMenu = _menuConfig.GetMenuByType<MainMenu>();
	    
	    mainMenu.VersionText = _photonAppSettings.AppSettings.AppVersion;
	    return mainMenu;
	}
	
	private FirebaseConnector Init_FirebaseConnector() 
	{
	    FirebaseConnector firebaseConnector = new FirebaseConnector();
	    
	    return firebaseConnector;
	}
	
	private AuthenticationHandler Init_AuthenticationHandler(FirebaseConnector firebaseConnector) 
	{
	    AuthenticationHandler authenticationHandler = new AuthenticationHandler(firebaseConnector);
	    
	    RegistrationMenu registrationMenu = _menuConfig.GetMenuByType<RegistrationMenu>();
	    AuthorizationMenu authorizationMenu = _menuConfig.GetMenuByType<AuthorizationMenu>();
	    
	    registrationMenu.AuthenticationNotifiers = authenticationHandler;
	    authorizationMenu.AuthenticationNotifiers = authenticationHandler;
	    
	    authenticationHandler.RegisterMenuNotifiers = registrationMenu;
	    authenticationHandler.AuthMenuNotifiers = authorizationMenu;
	    
	    
	    
	    return authenticationHandler;
	}
}
