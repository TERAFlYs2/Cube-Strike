using UnityEngine;

public class CharacterAnimationsHandler : MonoBehaviour
{
    private Animator _animator;

    private ICharacterMovementNotifiers _characterMovementNotifiers;
    private IHealthNotifiers _healthNotifiers;
    
    public ICharacterMovementNotifiers CharacterMovementNotifiers 
    {
        get => _characterMovementNotifiers;
        
        set 
        {
            if (_characterMovementNotifiers != null) 
            {
                _characterMovementNotifiers.OnMovement -= OnMovement;
                _characterMovementNotifiers.OnJump -= OnJump;
                _characterMovementNotifiers.OnFall -= OnFall;
            }
            
            _characterMovementNotifiers = value;
            
            _characterMovementNotifiers.OnMovement += OnMovement;
            _characterMovementNotifiers.OnJump += OnJump;
            _characterMovementNotifiers.OnFall += OnFall;
        }
    }
    
    public IHealthNotifiers HealthNotifiers 
    {
        get => _healthNotifiers;
        
        set 
        {
            if (_healthNotifiers != null) 
            {
                _healthNotifiers.OnDie -= OnDie;
            }

            _healthNotifiers = value;
            
            _healthNotifiers.OnDie += OnDie;
        }
    }
     private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnMovement(float speed) 
    {
        _animator.SetFloat("MoveSpeed", speed);
    }
    
    private void OnJump() 
    {
        _animator.SetTrigger("Jump");
    }
    
    private void OnFall(bool isFall) 
    {
        _animator.SetBool("IsFall", isFall);
    }
    
    private void OnDie() 
    {
        _animator.SetTrigger("Die");
    }
}
