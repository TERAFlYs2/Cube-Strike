using System;
using Fusion;
public class NetworkCharacterHealth : NetworkBehaviour, IDamageble, IHealable
{
    private CharacterHealth _characterHealth;

    [Networked][OnChangedRender(nameof(OnNetworkHealthChanged))]
    public int NetworkCurrentHealth { get; private set; }

    public bool CanTakeDamage => _characterHealth.CanTakeDamage;
    public bool CanTakeHeal => _characterHealth.CanTakeHeal;

    public int CurrentHealth => _characterHealth.CurrentHealth;

    public CharacterHealthConfig Config => _characterHealth.Config;

    public event Action<int> OnHealthChanged;
    public event Action OnDie;
    private CharacterHealthConfig _config;

    public void Initialize(CharacterHealthConfig config)
    {
        _config = config;
    }

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            _characterHealth = new CharacterHealth(_config);
            _characterHealth.OnHealthChanged += HandleLogicHealthChanged;
            _characterHealth.OnDie += OnDieChange;
            
            NetworkCurrentHealth = _characterHealth.CurrentHealth;
            
            
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        if (_characterHealth != null)
        {
            _characterHealth.OnHealthChanged -= HandleLogicHealthChanged;
            _characterHealth.OnDie -= OnDieChange;
            _characterHealth = null;
        }
    }

    private void HandleLogicHealthChanged(int newHealth)
    {
        NetworkCurrentHealth = newHealth;
    }
    private void OnNetworkHealthChanged()
    {
        OnHealthChanged?.Invoke(CurrentHealth);
    }
    
    private void OnDieChange() 
    {
        OnDie?.Invoke();
    }
    
    public void TakeDamage(int amount)
    {
        Rpc_TakeDamage(amount);
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)]
    private void Rpc_TakeDamage(int amount, RpcInfo info = default)
    {
        if (!Object.HasStateAuthority) 
            return;

        _characterHealth.TakeDamage(amount);
    }

    public void TakeHeal(int amount)
    {
        
    }
}
