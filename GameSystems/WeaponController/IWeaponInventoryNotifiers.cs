using System;

public interface IWeaponInventoryNotifiers
{
    public event Action<IWeaponPickable, WeaponTypeForSlot> OnWeaponInSlotAdded;
    public event Action<WeaponTypeForSlot> OnWeaponInSlotRemoved;
    public event Action<IWeaponPickable> OnSelectedWeaponChanged;
}
