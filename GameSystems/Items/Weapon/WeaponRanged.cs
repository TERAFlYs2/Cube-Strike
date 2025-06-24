using System;
using System.Collections;
using Fusion;
using UnityEngine;
public enum ShootType 
{
    Raycast,
    Physics
}

public enum ShootMode 
{
    Single,
    Auto
}

public abstract class WeaponRanged : Weapon, IReloadable, IRangedWeaponNotifiers
{
    [Header("Params")]
    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public int AmmoCountInMagazine { get; private set; }
    [field: SerializeField] public int MaxAmmoCount { get; private set; }
    [field: SerializeField] public ShootMode ShootMode { get; private set; }
    
    [field: SerializeField] protected WeaponSpread Spread { get; private set; }
    
    [field: SerializeField] protected ShootType ShootType { get; private set; }
    
    [Header("Visual")]
    [field: SerializeField] protected AudioClip ReloadSound { get; private set; }
    [field: SerializeField] protected ParticleSystem MuzzleFlash { get; private set; }
    [field: SerializeField] protected ParticleSystem HitEffect { get; private set; }
    
    protected bool IsReloading { get; private set; }
    
    
    [Networked, OnChangedRender(nameof(NetworkCurrentMaxAmmoCountChanged))] 
    [HideInInspector] public int CurrentMaxAmmoCount { get; private set; }
    
    
    [Networked, OnChangedRender(nameof(NetworkCurrentAmmoCountInMagazineChanged))] 
    [HideInInspector] public int CurrentAmmoCountInMagazine { get; private set; }
    
    public event Action<int> CurrentAmmoCountInMagazineChanged;
    public event Action<int> CurrentMaxAmmoCountChanged;

    private new void Awake()
    {
        base.Awake();
    }

    public override void Spawned()
    {
        CurrentMaxAmmoCount = MaxAmmoCount;
        CurrentAmmoCountInMagazine = AmmoCountInMagazine;
    }
    private void NetworkCurrentMaxAmmoCountChanged() 
    {
        CurrentMaxAmmoCountChanged?.Invoke(CurrentMaxAmmoCount);
    }
    
    private void NetworkCurrentAmmoCountInMagazineChanged() 
    {
        CurrentAmmoCountInMagazineChanged?.Invoke(CurrentAmmoCountInMagazine);
    }
    
    protected virtual bool CanReload() 
    {
        bool thereAmmo = CurrentMaxAmmoCount > 0;
        bool thereAmmoInMagazine = CurrentAmmoCountInMagazine < AmmoCountInMagazine;
    
        return  thereAmmo && thereAmmoInMagazine && !IsReloading;
    }

    protected override bool CanInteract()
    {
        return base.CanInteract() && !IsReloading && CurrentAmmoCountInMagazine > 0;
    }
    public void Reload()
    {
        if (CanReload()) 
        {    
            StartCoroutine(ReloadRoutine());
        }
    }
    
    protected virtual void HitTarget(RaycastHit hitInfo) 
    {
        RPC_PlayHitEffect(hitInfo.point, hitInfo.normal);
    
        if (hitInfo.collider.TryGetComponent<IDamagebleProvider>(out var damagebleProvider)) 
        {
            damagebleProvider.Damageble.TakeDamage((int)Damage);
            Debug.Log("Я попал в игрока");
        }
    }
    
    protected virtual void RaycastShoot() 
    {
        Ray ray = new(AttackPoint.position, Spread.GetSpreadDirection(AttackPoint));

        if (Physics.Raycast(ray, out var hitInfo, Range)) 
        {
            HitTarget(hitInfo);
        }
    }
    
    protected virtual void PhysicsShoot() 
    {
       // фізична стрільба 
    }
    
    
    protected sealed override void Attack()
    {
        CurrentAmmoCountInMagazine--;
        RPC_PlayMuzzleFlash();
     
        switch (ShootType) 
        {
            case ShootType.Raycast:
                RaycastShoot();
                break;
                
            case ShootType.Physics:
                PhysicsShoot();
                break;
        }
        
    }
    private IEnumerator ReloadRoutine() 
    {
        IsReloading = true;
        RPC_PlayReloadSound();
        
        yield return new WaitForSeconds(ReloadTime);
        
        int requiredCountAmmoInMagazine = AmmoCountInMagazine - CurrentAmmoCountInMagazine;
        
        CurrentMaxAmmoCount -= requiredCountAmmoInMagazine;
        
        if (CurrentMaxAmmoCount < 0) 
        {
            CurrentMaxAmmoCount = 0;
            CurrentAmmoCountInMagazine = requiredCountAmmoInMagazine;
        }
        else 
            CurrentAmmoCountInMagazine = AmmoCountInMagazine;
        
        IsReloading = false;
    }
    
    [Rpc]
    private void RPC_PlayReloadSound() 
    {
        AudioSource.PlayOneShot(ReloadSound);
    }
    
    [Rpc]
    private void RPC_PlayMuzzleFlash() 
    {
        MuzzleFlash.Play();
    }
    
    [Rpc]
    private void RPC_PlayHitEffect(Vector3 point, Vector3 normal) 
    {
        var hitObject = Runner.Spawn(HitEffect.gameObject, point, Quaternion.LookRotation(normal));

        StartCoroutine(DespawnHitObject(hitObject, HitEffect.main.duration));
        
    }
    
    private IEnumerator DespawnHitObject(NetworkObject hitObj, float lifeTime) 
    {
        yield return new WaitForSeconds(lifeTime);
        
        Runner.Despawn(hitObj);
    }
}
