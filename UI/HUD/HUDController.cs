using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{   
    [SerializeField] private Slider _healthBar;
    
    [SerializeField] private TMP_Text _weaponMaxAmmoInfo;
    [SerializeField] private TMP_Text _weaponAmmoInMagazineInfo;

    [SerializeField] private RawImage _aimImage;
    
    [field: SerializeField] public WeaponInventoryView WeaponInventoryView { get; private set; }
    [SerializeField] private AimConfig _aimConfig;
    
    
    public AimHandler AimHandler { get; private set; }
    
    private IHealthNotifiers _healthNotifiers;
    private IWeaponPickupSystemNotifiers _weaponPickupSystemNotifiers;
    public IHealthNotifiers HealthNotifiers 
    {
        get => _healthNotifiers;
        
        set 
        {
            if (_healthNotifiers != null) 
            {
                _healthNotifiers.OnHealthChanged -= HealthBarUpdate;
            }
            
            _healthNotifiers = value;
            
            _healthNotifiers.OnHealthChanged += HealthBarUpdate;  
        }
    }
    
    public IWeaponPickupSystemNotifiers WeaponPickupSystemNotifiers 
    {
        get => _weaponPickupSystemNotifiers;
        
        set 
        {
            if(_weaponPickupSystemNotifiers != null) 
            {
                _weaponPickupSystemNotifiers.OnTaked -= SubscribeToWeaponEvent;
                _weaponPickupSystemNotifiers.OnDropped -= UnSubscribeToWeaponEvent;
            }
            
            _weaponPickupSystemNotifiers = value;
            
            _weaponPickupSystemNotifiers.OnTaked += SubscribeToWeaponEvent;
            _weaponPickupSystemNotifiers.OnDropped += UnSubscribeToWeaponEvent;
        }
    }
    private void Awake()
    {
        AimHandler = new(_aimConfig, _aimImage);
    }
    private void SubscribeToWeaponEvent(IWeaponPickable weaponPickable) 
    {
        if (weaponPickable is IRangedWeaponNotifiers rangedWeapon) 
        {
            rangedWeapon.CurrentMaxAmmoCountChanged += CurrentMaxAmmoCountUpdate;
            rangedWeapon.CurrentAmmoCountInMagazineChanged += CurrentAmmoCountInMagazineUpdate;
            
            CurrentMaxAmmoCountUpdate(rangedWeapon.CurrentMaxAmmoCount);
            CurrentAmmoCountInMagazineUpdate(rangedWeapon.CurrentAmmoCountInMagazine);
        }
    }
    
    private void UnSubscribeToWeaponEvent(IWeaponPickable weaponPickable) 
    {
        if (weaponPickable is IRangedWeaponNotifiers rangedWeapon) 
        {
            rangedWeapon.CurrentMaxAmmoCountChanged -= CurrentMaxAmmoCountUpdate;
            rangedWeapon.CurrentAmmoCountInMagazineChanged -= CurrentAmmoCountInMagazineUpdate;
            
            CurrentMaxAmmoCountUpdate(0);
            CurrentAmmoCountInMagazineUpdate(0);
        }
    }
    
    private void HealthBarUpdate(int amount)
    {
        Debug.Log("UI: " + amount);
        float ratio = (float)_healthNotifiers.CurrentHealth / _healthNotifiers.Config.MaxHealth;
        _healthBar.value = ratio;
    }
    
    private void CurrentMaxAmmoCountUpdate(int amount) 
    {
        _weaponMaxAmmoInfo.text = $"{amount}";
    }
    private void CurrentAmmoCountInMagazineUpdate(int amount) 
    {
        _weaponAmmoInMagazineInfo.text = $"{amount}";
    }

}
