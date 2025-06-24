using System;
using UnityEngine;
public class CharacterMovement : IDisposable, ICharacterMovementNotifiers
{
    private readonly CharacterMovementConfig _config;
    private readonly UnityEngine.CharacterController _characterController;

    private Vector3 _gravityVelocity = Vector3.zero;
    
    private float _maximumFallingSpeed;
    private float _currentSpeedX = 0f;
    private float _currentSpeedZ = 0f;
    
    private IRotateNotifier _rotateNotifier;

    public event Action<float> OnMovement;
    public event Action OnJump;
    public event Action<bool> OnFall;
    
    public float GravityForce 
    {
        get => _gravityVelocity.y;
        
        private set 
        {
            _gravityVelocity.y = value;
            OnFall?.Invoke(!_characterController.isGrounded);
        }
    }

    public IRotateNotifier RotateNotifier 
    {
        get => _rotateNotifier;
        
        set 
        {
            if (_rotateNotifier != null) 
            {               
                _rotateNotifier.OnRotateY -= Rotate;
            }
             
            _rotateNotifier = value;
                
            _rotateNotifier.OnRotateY += Rotate;
            
        }
    }
    public CharacterMovement(CharacterMovementConfig config, UnityEngine.CharacterController characterController)
    {
        _config = config;
        _characterController = characterController;
        
        float dragCoefficient = 1.0f; // Коэффициент сопротивления (пример: человек ≈ 1.0)
        float area = 0.7f;        // Площадь поперечного сечения (м²) (пример: тело человека)

        // Вычисление предельной скорости
        _maximumFallingSpeed = Mathf.Sqrt(2 * _config.Mass * _config.Mass / (dragCoefficient * _config.Drag * area));
    }
    
    private void Rotate(Vector3 axis) 
    {
        _characterController.transform.rotation = Quaternion.Euler(axis);
    }
    
    public void CalculateMove(Vector3 direction, bool isRun, float timeDelta) 
    {  
        Vector3 right = _characterController.transform.right;
        Vector3 forward = _characterController.transform.forward;

        float currentMaxSpeed = isRun ? _config.RunSpeed : _config.WalkSpeed;

        float targetSpeedX = direction.x * currentMaxSpeed * _config.HorizontalSpeedModifier; 
        float targetSpeedZ = direction.z * currentMaxSpeed;

        targetSpeedZ = targetSpeedZ < 0 ? targetSpeedZ *= 0.2f : targetSpeedZ;
        
        float accelModeX = Mathf.Abs(_currentSpeedX) < Mathf.Abs(targetSpeedX) ? _config.Acceleration : _config.Deceleration;
        float accelModeZ = Mathf.Abs(_currentSpeedZ) < Mathf.Abs(targetSpeedZ) ? _config.Acceleration : _config.Deceleration;
        
        _currentSpeedX = Mathf.MoveTowards(_currentSpeedX, targetSpeedX, accelModeX * timeDelta);
        _currentSpeedZ = Mathf.MoveTowards(_currentSpeedZ, targetSpeedZ, accelModeZ * timeDelta);

        Vector3 move = (right * _currentSpeedX) + (forward  * _currentSpeedZ);

        _characterController?.Move(move * timeDelta);
        OnMovement?.Invoke(_characterController.velocity.magnitude);
    }
    
    public void CalculateJump(bool jumpBtnPressed) 
    {
        if (jumpBtnPressed && _characterController.isGrounded) 
        {
            _gravityVelocity.y = Mathf.Sqrt(_config.JumpForce * -2f * _config.Gravity); 
            OnJump?.Invoke();
        }
    }   

    public void ApplyGravity(float timeDelta) 
    {
        if (!_characterController.isGrounded) 
        {
            float force = _config.Gravity * timeDelta;
            
            GravityForce += force; 
            
            if (Mathf.Abs(_gravityVelocity.y) > _maximumFallingSpeed) 
                GravityForce = -Mathf.Abs(_maximumFallingSpeed);
            
        }
        else    
        {
            if (_gravityVelocity.y < 0)
                GravityForce = -2f;
            
        }

        _characterController?.Move(_gravityVelocity * timeDelta); 
    }

    public void Dispose()
    {
        _rotateNotifier.OnRotateY -= Rotate;
    }
}
