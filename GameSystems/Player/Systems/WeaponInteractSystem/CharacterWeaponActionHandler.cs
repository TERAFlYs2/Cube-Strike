using System;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterWeaponActionHandler : IPickupSystemInputNotifiers
{
    private readonly CharacterWeaponActionConfig _config;
    private Weapon _currentWeapon;
    private IWeaponInventoryNotifiers _weaponInventoryNotifiers;

    public event Func<Transform, Task> OnPickupInput;
    public event Func<Transform, Task> OnDropInput;
    
    public bool IsInputAllowed { get; set; } = true;
    
    public Transform AttackPoint { get; set; }

    public CharacterWeaponActionHandler(CharacterWeaponActionConfig config)
    {
        _config = config;
    }
    
    public IWeaponInventoryNotifiers WeaponInventoryNotifiers 
    {
        get => _weaponInventoryNotifiers;
        
        set 
        {
            if (_weaponInventoryNotifiers != null) 
            {
                _weaponInventoryNotifiers.OnSelectedWeaponChanged -= SetNewCurrentWeapon;
            }
            
            _weaponInventoryNotifiers = value;
            
            _weaponInventoryNotifiers.OnSelectedWeaponChanged += SetNewCurrentWeapon;
        }
    }
    
    public void HandlingInput() 
    {
        if (!IsInputAllowed) return;
        
        UpdateAttackInput();
        UpdateReloadInput();
    }
    
    public void HandlingPickupSystemInput(Transform pickupPoint, Transform dropPoint) 
    {
        if (Input.GetKeyDown(_config.PickupKey)) OnPickupInput?.Invoke(pickupPoint);
        
        if (Input.GetKeyDown(_config.DropKey)) OnDropInput?.Invoke(dropPoint);
    }
    private void SetNewCurrentWeapon(IWeaponPickable newCurrentWeapon) 
    {
        _currentWeapon = newCurrentWeapon as Weapon;
    }
    private void UpdateAttackInput() 
    {
        if (_currentWeapon == null) return;
    
        ShootMode shootMode = (_currentWeapon as WeaponRanged).ShootMode;
        
        bool attackInput = false;
        
        switch (shootMode) 
        {
            case ShootMode.Single:
                attackInput = Input.GetKeyDown(_config.DefaultAttackKey);
                break;
            
            case ShootMode.Auto:
                attackInput = Input.GetKey(_config.DefaultAttackKey);
                break;
        }

        if (attackInput) 
        {
            _currentWeapon?.TryAttack(AttackPoint);
        }
    }
    
    private void UpdateReloadInput() 
    {
        if (Input.GetKeyDown(_config.ReloadKey) && _currentWeapon is IReloadable reloadableWeapon) 
        {
            reloadableWeapon?.Reload();
        }
    }
}
