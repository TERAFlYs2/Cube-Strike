using System;
using Game.Character;
using UnityEngine;

public class HUDFactory : IDisposable
{
    private readonly CharacterSpawner _characterSpawner;
    private readonly HUDFactoryConfig _config;
    public HUDFactory(HUDFactoryConfig config, CharacterSpawner characterSpawner)
    {
        _config = config;
        _characterSpawner = characterSpawner;
        
        _characterSpawner.OnSpawned += OnSpawned;
    }
    
    private void OnSpawned(NetworkCharacterController networkCharacterController) 
    {
        CreateHUD(networkCharacterController);
    }
    
    private void CreateHUD(NetworkCharacterController networkCharacterController) 
    {
        var HUDControllerPrefab = UnityEngine.Object.Instantiate(_config.HUDControllerPrefab);

        HUDControllerPrefab.HealthNotifiers = networkCharacterController.Damageble;
        Debug.Log("networkCharacterController.Damageble is null?" + networkCharacterController.Damageble == null);
        Debug.Log("HUD: подписка на здоровье. Current = " + networkCharacterController.Damageble.CurrentHealth);
        
        HUDControllerPrefab.WeaponInventoryView.WeaponInventoryNotifiers = networkCharacterController.WeaponInventory;
        HUDControllerPrefab.WeaponPickupSystemNotifiers = networkCharacterController.WeaponPickupSystem;
        HUDControllerPrefab.AimHandler.WeaponPickupSystemNotifiers = networkCharacterController.WeaponPickupSystem;
    }
    
    public void Dispose() 
    {
        _characterSpawner.OnSpawned -= OnSpawned;
    }
}
