using System;
using Fusion;
public interface IWeaponNotifiers
{
    public event Action<Weapon> OnAttack;
    public event Action<PlayerRef> OnStateAuthorityChanged;
}
