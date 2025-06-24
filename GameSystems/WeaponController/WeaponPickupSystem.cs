using System;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;

public class WeaponPickupSystem : IWeaponPickupSystemNotifiers
{
    private readonly WeaponPickupSystemConfig _config;
    private readonly WeaponInventory _weaponInventory;

    private TaskCompletionSource<bool> _tcsWeaponOwnerChanged;

    private IPickupSystemInputNotifiers _pickupSystemInputNotifiers;
    
    public IPickupSystemInputNotifiers PickupSystemInputNotifiers 
    {
        get => _pickupSystemInputNotifiers;
        
        set 
        {
            if (_pickupSystemInputNotifiers != null) 
            {
                _pickupSystemInputNotifiers.OnPickupInput -= Pickup;
                _pickupSystemInputNotifiers.OnDropInput -= Drop;
            }
            
            _pickupSystemInputNotifiers = value;
            
            _pickupSystemInputNotifiers.OnPickupInput += Pickup;
            _pickupSystemInputNotifiers.OnDropInput += Drop;
        }
    }
    
    
    public event Action<IWeaponPickable> OnTaked;
    public event Action<IWeaponPickable> OnDropped;
    
    public WeaponPickupSystem(WeaponPickupSystemConfig config, WeaponInventory weaponInventory)
    {
        _config = config;
        _weaponInventory = weaponInventory;
    }
    
    public async Task Pickup(Transform holdingPoint) 
    {
        var wasHit = Physics.Raycast(holdingPoint.position, holdingPoint.forward, out RaycastHit hitInfo, _config.PickupDistance);

        if (wasHit && hitInfo.collider.TryGetComponent<IWeaponPickable>(out var weaponPickable)) 
        {     
            Weapon weapon = weaponPickable as Weapon;
            
            weapon.OnStateAuthorityChanged += OnStateAuthorityChanged;
            
            weapon.Object.RequestStateAuthority();
            _tcsWeaponOwnerChanged = new TaskCompletionSource<bool>();
            
            bool pickupSuccess = false;
            
            if (!weapon.HasStateAuthority) 
                pickupSuccess = await _tcsWeaponOwnerChanged.Task && _weaponInventory.TryAddWeapon(weaponPickable);
            
            else 
                pickupSuccess = _weaponInventory.TryAddWeapon(weaponPickable);
            
            if (pickupSuccess) 
            { 
                PickupViewWeapon(weaponPickable, holdingPoint);
                OnTaked?.Invoke(weaponPickable);      
            }      
        }
    }
    
    public async Task Drop(Transform dropPoint) 
    {
        Weapon weapon = _weaponInventory.SelectedWeapon as Weapon;

        if (weapon != null) 
        {
            if (weapon.HasStateAuthority && _weaponInventory.RemoveWeapon()) 
            {
                DropViewWeapon(weapon as IWeaponPickable, dropPoint); 
                weapon.OnStateAuthorityChanged -= OnStateAuthorityChanged;
                OnDropped?.Invoke(weapon as IWeaponPickable); 
            }
            
            await Task.Yield();
        }
    }

    private void PickupViewWeapon(IWeaponPickable weaponPickable, Transform holdingPoint) 
    {
        if (weaponPickable is Weapon weapon) 
        {
            ApplayPickupPhysics(weapon, weaponPickable, holdingPoint);
        }
    }
    
    private void DropViewWeapon(IWeaponPickable weaponPickable, Transform dropPoint) 
    {    
        if (weaponPickable is Weapon currentWeapon) 
        {   
            ApplayDropPhysics(currentWeapon, dropPoint);
        }
    }
    
    private void ApplayPickupPhysics(Weapon weapon, IWeaponPickable weaponPickable, Transform holdingPoint) 
    {
        weapon.transform.SetParent(holdingPoint);
        weapon.transform.SetLocalPositionAndRotation(weaponPickable.HoldingPoint, weaponPickable.Orientation);
        weapon.Rigidbody.isKinematic = true;
    }
    
    private void ApplayDropPhysics(Weapon weapon, Transform dropPoint) 
    {
        weapon.transform.SetParent(null);
        weapon.Rigidbody.isKinematic = false;
        weapon.Rigidbody.AddForce(dropPoint.forward * _config.DropForce, ForceMode.Impulse);
    }
    
    private void OnStateAuthorityChanged(PlayerRef owner) 
    {
        if (_tcsWeaponOwnerChanged != null && !_tcsWeaponOwnerChanged.Task.IsCompleted) 
        {
            _tcsWeaponOwnerChanged.SetResult(true);
            _tcsWeaponOwnerChanged = null;
        }
    }
    
}

