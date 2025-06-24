using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
public class PlayerJoinHandler : IDisposable
{   
    private readonly CharacterSpawner _characterSpawner;

    private readonly Dictionary<PlayerRef, NetworkObject> _joinedPlayers = new();

    private IPlayerConnectNotifiers _playerConnectNotifier;
    
    public IPlayerConnectNotifiers PlayerConnectNotifier 
    {
        get => _playerConnectNotifier;
        
        set 
        {
            if (_playerConnectNotifier != null) 
            {      
                _playerConnectNotifier.OnPlayerJoinedEvent -= OnPlayerJoined;
                _playerConnectNotifier.OnPlayerLeftEvent -= OnPlayerLeft;
            }
                
            _playerConnectNotifier = value;
                
            _playerConnectNotifier.OnPlayerJoinedEvent += OnPlayerJoined;
            _playerConnectNotifier.OnPlayerLeftEvent += OnPlayerLeft;
        }
    }
    public PlayerJoinHandler(CharacterSpawner characterSpawner)
    {
        _characterSpawner = characterSpawner;
    }

    public void Dispose()
    {
        _playerConnectNotifier.OnPlayerJoinedEvent -= OnPlayerJoined;
        _playerConnectNotifier.OnPlayerLeftEvent -= OnPlayerLeft;
        
        _joinedPlayers.Clear();
        
        _playerConnectNotifier = null;
    }

    private void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.LocalPlayer != player) 
        {
            return;
        }
    
        var character = _characterSpawner.SpawnNetwork(runner, player);
        _joinedPlayers.Add(player, character.GetComponent<NetworkObject>()); 
    }

    private void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (runner.LocalPlayer != player) 
        {
            return;
        }

        if (_joinedPlayers.TryGetValue(player, out var networkObject)) 
        {
            runner.Despawn(networkObject);
            _joinedPlayers.Remove(player);
        }

    }
    
    
}
