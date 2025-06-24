using System;
using System.Collections.Generic;
public enum WeaponTypeForSlot 
{
    Main,
    Additional
}
public class WeaponInventory : IWeaponInventoryNotifiers
{
    private IWeaponPickable _selectedWeapon;
    private Dictionary<WeaponTypeForSlot, IWeaponPickable> _weaponsBySlot = new();
    
    public IWeaponPickable SelectedWeapon  
    {
        get => _selectedWeapon;
        
        private set 
        {
            _selectedWeapon = value;
            
            OnSelectedWeaponChanged?.Invoke(_selectedWeapon);
        }
    }
    
    public event Action<IWeaponPickable> OnSelectedWeaponChanged;
    
    public event Action<IWeaponPickable, WeaponTypeForSlot> OnWeaponInSlotAdded;
    public event Action<WeaponTypeForSlot> OnWeaponInSlotRemoved;

    public bool TryAddWeapon(IWeaponPickable weaponPickable) 
    {
        WeaponTypeForSlot typeSlot = weaponPickable.WeaponTypeForSlot;
    
        if (_weaponsBySlot.TryAdd(typeSlot, weaponPickable)) 
        {
            SelectedWeapon ??= weaponPickable;
            
            OnWeaponInSlotAdded?.Invoke(weaponPickable, typeSlot);
            
            return true;
        }

        return false;
    }
    
    public bool TryRemoveWeapon(out IWeaponPickable removedPickableWeapon)
    {
        if (RemoveWeapon()) 
        {
            removedPickableWeapon = SelectedWeapon;
            return true;
        }
        
        removedPickableWeapon = null;
        return false;
    }
    
    public bool RemoveWeapon() 
    {
        if (SelectedWeapon != null) 
        {
            WeaponTypeForSlot typeSlot = SelectedWeapon.WeaponTypeForSlot;
            
            if (_weaponsBySlot.Remove(typeSlot))
            {
                SelectedWeapon = null;
    
                OnWeaponInSlotRemoved?.Invoke(typeSlot);

                return true;
            }
        }

        return false;
    }
    
    public void ChooseSlot(WeaponTypeForSlot weaponSlotType)
    {
        foreach (var pickable in _weaponsBySlot.Values)
        {
            if (pickable is Weapon w)
                w.gameObject.SetActive(false);
        }

        if (_weaponsBySlot.TryGetValue(weaponSlotType, out var selected))
        {
            if (selected is Weapon weapon)
            {
                weapon.gameObject.SetActive(true);
                SelectedWeapon = selected;
            }
        }
            
    }
}
