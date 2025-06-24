public interface IDamageble : IHealthNotifiers
{
    public bool CanTakeDamage { get; }
    public void TakeDamage(int amount);
}
