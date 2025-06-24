using System;
using UnityEngine;
public class CharacterHealth : IDamageble, IHealable
{
    public CharacterHealthConfig Config { get; private set; }

    private int _currentHealth;
    public int CurrentHealth 
    {
        get => _currentHealth;
        
        private set 
        {
            _currentHealth = value;
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }

    public bool CanTakeDamage { get; set; }

    public bool CanTakeHeal { get; set; }

    public event Action<int> OnHealthChanged;
    public event Action OnDie;
    public CharacterHealth(CharacterHealthConfig config)
    {
        Config = config;
        
        CurrentHealth = config.MaxHealth;
        
        CanTakeDamage = true;
        CanTakeHeal = true;
    }

    public void TakeDamage(int amount)
    {
        int validAmount = Mathf.Abs(amount);
    
        if (!CanTakeDamage) return;
    
        if (CurrentHealth - validAmount > 0)
            CurrentHealth -= Mathf.Abs(validAmount);
            
        else 
        {
            CurrentHealth = 0;
            OnDie?.Invoke();
        }
            
    }

    public void TakeHeal(int amount)
    {
        if (CurrentHealth + amount < Config.MaxHealth)
            CurrentHealth += Mathf.Abs(amount);
            
        else
            CurrentHealth = Config.MaxHealth;
    }
    
    
}
