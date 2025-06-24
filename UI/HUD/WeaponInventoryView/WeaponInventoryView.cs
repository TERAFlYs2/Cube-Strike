using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponInventoryView
{
    [SerializeField] private List<WeaponInventorySlot> _weaponInventorySlots;

    private IWeaponInventoryNotifiers _weaponInventoryNotifiers;
    
    public IWeaponInventoryNotifiers WeaponInventoryNotifiers 
    {
        get => _weaponInventoryNotifiers;
        
        set 
        {
            if (_weaponInventoryNotifiers != null) 
            {
                _weaponInventoryNotifiers.OnWeaponInSlotAdded -= WeaponInSlotAdded;
                _weaponInventoryNotifiers.OnWeaponInSlotRemoved -= WeaponInSlotRemoved;
                _weaponInventoryNotifiers.OnSelectedWeaponChanged -= ActivateSelectorForSlot;
            }
            
            _weaponInventoryNotifiers = value;
            
            _weaponInventoryNotifiers.OnWeaponInSlotAdded += WeaponInSlotAdded;
            _weaponInventoryNotifiers.OnWeaponInSlotRemoved += WeaponInSlotRemoved;
            _weaponInventoryNotifiers.OnSelectedWeaponChanged += ActivateSelectorForSlot;
        }
    }
    
    public WeaponInventoryView()
    {
        if (_weaponInventorySlots == null) return;
        
        foreach (var slot in _weaponInventorySlots) 
        {
            slot.Selector.SetActive(false);
        }
    }
    
    private void ActivateSelectorForSlot(IWeaponPickable weaponPickable) 
    {
        if (weaponPickable == null) return;
        
        WeaponTypeForSlot weaponTypeForSlot = weaponPickable.WeaponTypeForSlot;
        
        foreach (var slot in _weaponInventorySlots) 
        {
            if (slot.WeaponTypeForSlot == weaponTypeForSlot) 
            {
                slot.Selector.SetActive(true);
            }
            else 
            {
                slot.Selector.SetActive(false);
            }
        }
    }
    private void WeaponInSlotAdded(IWeaponPickable weaponPickable, WeaponTypeForSlot weaponTypeForSlot) 
    {
        Color32 colorSlot = new (255, 255, 255, 255);
        Texture textureSlot = weaponPickable.TextureForSlot;
        
        ChangeWeaponSlot(weaponTypeForSlot, colorSlot, textureSlot);
    }
    
    private void WeaponInSlotRemoved(WeaponTypeForSlot weaponTypeForSlot) 
    {
        Color32 colorSlot = new (255, 255, 255, 17);
        
        ChangeWeaponSlot(weaponTypeForSlot, colorSlot, null);
    }
    
    private void ChangeWeaponSlot(WeaponTypeForSlot weaponTypeForSlot, Color32 colorSlot, Texture textureSlot) 
    {
        foreach (var slot in _weaponInventorySlots) 
        {
            if (slot.WeaponTypeForSlot == weaponTypeForSlot) 
            {
                slot.Color = colorSlot;
                slot.WeaponTexture = textureSlot;
                break;
            }
        }
    }
}
