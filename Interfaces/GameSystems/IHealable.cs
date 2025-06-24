public interface IHealable : IHealthNotifiers
{
    public bool CanTakeHeal { get; }
    public void TakeHeal(int amount);
}
