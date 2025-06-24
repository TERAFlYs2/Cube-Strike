using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CharacterSpawnerConfig _characterSpawnerConfig;
    [SerializeField] private MenuConfig _gameMenuConfig;
    [SerializeField] private HUDFactoryConfig _hudFactoryConfig;
    
    
    private static EntryPoint _instance;
    
    private NetworkRunner _runner;
    private readonly List<IDisposable> _disposables = new();

    private void Awake()
    {
        if (_instance != null) 
        {
            Destroy(gameObject);
        }

        _instance = this;
        _runner = NetworkRunner.Instances.Count > 0 ? NetworkRunner.Instances[0] : null;
        if (_runner == null) Debug.LogError("Runner is NULL!");
        
        Run();
    }

    private void OnDestroy()
    {
        foreach (var instance in _disposables) 
        {
            instance.Dispose();
        }
    }

    private void Run() 
    {
        InitMainMenuGame();
        
        var mainMenuGame = _gameMenuConfig.GetMenuByType<MainMenuGame>();
       
        var gamePerfomanceStatsMenu = _gameMenuConfig.GetMenuByType<GamePerfomanceStatsMenu>();
        
        var perfomanceStatsHandler = Init_PerfomanceStatsHandler();
        
        gamePerfomanceStatsMenu.GamePerfomanceStatsNotifier = perfomanceStatsHandler;
        
        
        var networkCallbacksHandler = Init_NetworkCallbacksHandler();
        
        var characterSpawner = Init_CharacterSpawner(mainMenuGame, networkCallbacksHandler);
        
        var sessionDisconnectHandler = Init_SessionDisconnectHandler(mainMenuGame);
        
        var playerJoinHandler = Init_PlayerJoinHandler(characterSpawner, networkCallbacksHandler); 
        var HUDFactory = Init_HUDFactory(characterSpawner);
    }

    private PerfomanceStatsHandler Init_PerfomanceStatsHandler() 
    {
        var perfomanceStatsHandler = new PerfomanceStatsHandler();
        perfomanceStatsHandler.CreateNetworkStatsHandler(_runner, 1f);
    
    
        return perfomanceStatsHandler; 
    }
    private CharacterSpawner Init_CharacterSpawner(MainMenuGame mainMenuGame, NetworkCallbacksHandler networkCallbacksHandler) 
    {
        CharacterDependencies characterDependencies = new() 
        {
            ActiveMenuChangedNotifier = mainMenuGame
        };

        return new CharacterSpawner(_characterSpawnerConfig, characterDependencies);
    }
    
    private NetworkCallbacksHandler Init_NetworkCallbacksHandler() 
    {
        NetworkCallbacksHandler networkCallbacksHandler = new ();
        _runner.AddCallbacks(networkCallbacksHandler);
        

        return networkCallbacksHandler;
    }
    
    private PlayerJoinHandler Init_PlayerJoinHandler(CharacterSpawner characterSpawner, NetworkCallbacksHandler networkCallbacksHandler) 
    {
        PlayerJoinHandler playerJoinHandler = new(characterSpawner) 
        {
            PlayerConnectNotifier = networkCallbacksHandler
        };
        
        _disposables.Add(playerJoinHandler);
        
        return playerJoinHandler;
    }
    
    private SessionDisconnectHandler Init_SessionDisconnectHandler(MainMenuGame mainMenuGame) 
    {
        return new(_runner) 
        {
            GameMenuNotifiers = mainMenuGame
        };
    }
    private void InitMainMenuGame() 
    {
        _= new MenuSwitcher(_gameMenuConfig.AllMenus);      
    }
    
   private HUDFactory Init_HUDFactory(CharacterSpawner characterSpawner) 
   {
       HUDFactory hudFactory = new(_hudFactoryConfig, characterSpawner);
       _disposables.Add(hudFactory);
       return hudFactory;
   }
   
}   
