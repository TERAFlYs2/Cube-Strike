using System;
public interface IWeaponPickupSystemNotifiers
{
    public event Action<IWeaponPickable> OnTaked;
    public event Action<IWeaponPickable> OnDropped;
}
