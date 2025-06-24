using System;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;

public struct CharacterDependencies 
{
    public IActiveMenuChangedNotifier ActiveMenuChangedNotifier;
}
public class CharacterSpawner : ICharacterSpawnerNotifiers
{
    private readonly CharacterSpawnerConfig _config;
    private readonly CharacterDependencies _characterDependencies;

    private Game.Character.NetworkCharacterController _character;
    
    public event Action <Game.Character.NetworkCharacterController> OnSpawned;
    public CharacterSpawner(CharacterSpawnerConfig config, in CharacterDependencies dependencies)
    {
        _config = config;
        _characterDependencies = dependencies;  
    }
    
    public Game.Character.NetworkCharacterController SpawnNetwork(NetworkRunner runner, PlayerRef player) 
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Quaternion spawnRotation = Quaternion.Euler(GetRandomSpawnRotation()); 
        
        Debug.Log("Spawn player ");
        
        _character = runner.Spawn(_config.Prefab, spawnPosition, spawnRotation, player);

        _character.CharacterController.Move(spawnPosition);
        
        _character.CharacterMovementInput.ActiveMenuChangedNotifier = _characterDependencies.ActiveMenuChangedNotifier;
        _character.Damageble.OnDie += Respawn;
        
        OnSpawned?.Invoke(_character);
        return _character;
    }
    
    private void Respawn() 
    {
        _ = ApplyRespawnCooldown();
    }
    
    private async Task ApplyRespawnCooldown() 
    {
        Vector3 respawnPosition = GetRandomSpawnPosition();
        Quaternion respawnRotation = Quaternion.Euler(GetRandomSpawnRotation()); 
        
        _character.CharacterMovementInput.IsInputAllowed = false;
        _character.CharacterWeaponActionHandler.IsInputAllowed = false;

        _= _character.WeaponPickupSystem.Drop(_character.Camera.transform);
        
        await Task.Delay(TimeSpan.FromSeconds(_config.RespawnCooldown));
        
        _character.CharacterMovementInput.IsInputAllowed = true;
        _character.CharacterWeaponActionHandler.IsInputAllowed = true;

        _character.CharacterController.Move(respawnPosition);
        _character.transform.localRotation = respawnRotation;
    }
    private Vector3 GetRandomSpawnPosition() 
    {
        int index = UnityEngine.Random.Range(0, _config.SpawnPoints.Count);

        return _config.SpawnPoints[index].Position;
    }

    private Vector3 GetRandomSpawnRotation() 
    {
        int index = UnityEngine.Random.Range(0, _config.SpawnPoints.Count);

        return _config.SpawnPoints[index].Rotation;
    }
    
}
