using System;
using Fusion;
using UnityEngine;

public abstract class Weapon : NetworkBehaviour, IWeaponNotifiers, IStateAuthorityChanged
{
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float AttackRate { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [SerializeField] protected AudioClip AttackSound;
    
    private float _lastTimeAttack = Mathf.NegativeInfinity;
    
    protected Transform AttackPoint { get; private set; }
    
    public Rigidbody Rigidbody { get; private set; }
    public AudioSource AudioSource{ get; private set; }
    
    public event Action<Weapon> OnAttack;
    public event Action<PlayerRef> OnStateAuthorityChanged;

    protected void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
    }
    protected virtual bool CanInteract() 
    {
        return _lastTimeAttack + AttackRate <= Time.time;
    }

    public void TryAttack(Transform attackPoint)
    {
        if (CanInteract()) 
        {
            AttackPoint = attackPoint;
            Attack();          

            RPC_PlayAttackSound();
            
            _lastTimeAttack = Time.time;
            
            OnAttack?.Invoke(this);
        }
    }
    
    protected abstract void Attack();

    public void StateAuthorityChanged()
    {
        OnStateAuthorityChanged?.Invoke(Object.StateAuthority);
    }
    
    [Rpc]
    private void RPC_PlayAttackSound() 
    {
        AudioSource.PlayOneShot(AttackSound);
    }
}
