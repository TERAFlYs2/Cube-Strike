using System;

public interface IRangedWeaponNotifiers
{
    public event Action<int> CurrentAmmoCountInMagazineChanged;
    public event Action<int> CurrentMaxAmmoCountChanged;
    
    public int CurrentMaxAmmoCount { get; }
    public int CurrentAmmoCountInMagazine { get; }
    
}
