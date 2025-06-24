using System;

public interface ICharacterMovementNotifiers
{
    public event Action<float> OnMovement;
    public event Action OnJump;
    public event Action<bool> OnFall;
}
