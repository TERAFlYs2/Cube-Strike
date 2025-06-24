using System;
using UnityEngine;

public class CharacterMovementInput : IDisposable
{
    private const string NameVerticalAxis = "Vertical";
    private const string NameHorizontalAxis = "Horizontal";
    private const string NameMouseXAxis = "Mouse X";
    private const string NameMouseYAxis = "Mouse Y";

    private readonly CharacterMovementInputConfig _config;
    
    private IActiveMenuChangedNotifier _activeMenuChangedNotifier;
    
    private Vector3 _direction;
    
    public Vector3 Direction => _direction;

    public bool IsInputAllowed { get; set; } = true;
    
    public bool IsRunInput => IsInputAllowed ? Input.GetKey(_config.RunButton) : false;
    public bool IsJumpInput => IsInputAllowed ? Input.GetKeyDown(_config.JumpButton) : false;
    
    public IActiveMenuChangedNotifier ActiveMenuChangedNotifier 
    {
        get { return _activeMenuChangedNotifier; }
        set 
        {
            if (_activeMenuChangedNotifier == null) 
            {
                _activeMenuChangedNotifier = value;
                _activeMenuChangedNotifier.OnChangeActive += InputPermissionSwitch;
            }
            else 
            {
                _activeMenuChangedNotifier.OnChangeActive -= InputPermissionSwitch;
                _activeMenuChangedNotifier = value;
                _activeMenuChangedNotifier.OnChangeActive += InputPermissionSwitch;
            }
        }
    }
    
    public CharacterMovementInput(CharacterMovementInputConfig config)
    {
        _config = config;
    }

    public void UpdateMovementInput(bool isSmoothing = true) 
    {
        if (!IsInputAllowed) 
        {
            _direction = Vector3.zero;
            return;
        } 
    
        float dirX = isSmoothing ? Input.GetAxis(NameHorizontalAxis) :
            Input.GetAxisRaw(NameHorizontalAxis);
        
        float ditZ = isSmoothing ? Input.GetAxis(NameVerticalAxis) :
            Input.GetAxisRaw(NameVerticalAxis);

        _direction = new Vector3(dirX, 0f, ditZ).normalized;
    }

    public float GetInputMouseX(bool isSmoothing = true) 
    {
        if (!IsInputAllowed) return 0f;
    
        return isSmoothing ? Input.GetAxis(NameMouseXAxis) : Input.GetAxisRaw(NameMouseXAxis);
    }
    
    public float GetInputMouseY(bool isSmoothing = true) 
    {
        if (!IsInputAllowed) return 0f;
    
        return isSmoothing ? Input.GetAxis(NameMouseYAxis) : Input.GetAxisRaw(NameMouseYAxis);
    }
    
    private void InputPermissionSwitch(bool isActiveMenu) 
    {
        IsInputAllowed = !isActiveMenu;
    }

    public void Dispose()
    {
        _activeMenuChangedNotifier.OnChangeActive -= InputPermissionSwitch;
    }
}
