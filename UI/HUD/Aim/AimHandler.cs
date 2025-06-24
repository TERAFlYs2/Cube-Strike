using UnityEngine.UI;

public class AimHandler
{
    private readonly AimConfig _config;

    private IWeaponPickupSystemNotifiers _weaponPickupSystemNotifiers;
    
    public IWeaponPickupSystemNotifiers WeaponPickupSystemNotifiers 
    {
        get => _weaponPickupSystemNotifiers;
        
        set 
        {
            if (_weaponPickupSystemNotifiers != null) 
            {
                _weaponPickupSystemNotifiers.OnTaked -= SwitchAimForTake;
                _weaponPickupSystemNotifiers.OnDropped -= SwitchAimForDrop;
            }
            
            _weaponPickupSystemNotifiers = value;
            
            _weaponPickupSystemNotifiers.OnTaked += SwitchAimForTake;
            _weaponPickupSystemNotifiers.OnDropped += SwitchAimForDrop;
        }
    }

    public RawImage CurrentAimImage { get; private set; }
    public AimHandler(AimConfig config, RawImage currentAimImage)
    {
        _config = config;
        CurrentAimImage = currentAimImage;
        
        CurrentAimImage.texture = _config.DefaultAim;
    }
    
    private void SwitchAimForTake(IWeaponPickable weaponPickable) 
    {
       CurrentAimImage.texture = _config.WeaponAim; 
    }
    
    private void SwitchAimForDrop(IWeaponPickable weaponPickable) 
    {
        CurrentAimImage.texture = _config.DefaultAim;
    }
}
