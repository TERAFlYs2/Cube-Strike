using System;
public interface IHealthNotifiers
{
    public event Action<int> OnHealthChanged;
    public event Action OnDie;
    public int CurrentHealth { get; }
    public CharacterHealthConfig Config { get; }
}
