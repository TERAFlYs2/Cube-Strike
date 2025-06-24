using System;
using UnityEngine;

public class CameraControl : IRotateNotifier
{
    private readonly CameraControlConfig _config;
    private readonly Camera _camera;
    
    private float _mouseX = 0f;
    private float _mouseY = 0f;
    
    public event Action<Vector3> OnRotateY;
    
    public CameraControl(CameraControlConfig config, Camera camera)
    {
        _config = config;
        _camera = camera;
        
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void UpdateControl(float mouseInputX, float mouseInputY, float timeDelta) 
    {   
        _mouseX -= mouseInputY * _config.SensitivityX * timeDelta;
        _mouseY += mouseInputX * _config.SensitivityY * timeDelta;
        
        _mouseX = Mathf.Clamp(_mouseX, -90, 90);
        
        _camera.transform.localRotation = Quaternion.Euler(_mouseX, 0f , 0f);
        
        OnRotateY?.Invoke(new Vector3(0f, _mouseY, 0f));
    }
    
    public void LateUpdateFollowThePoint(Transform target) 
    {       
        _camera.transform.position = target.position;
    }
    
    public void DisableForOtherClient(Fusion.NetworkObject Object) 
    {
        if (Object.HasStateAuthority) 
        {
            _camera.enabled = true;
        }
        else 
        {
            _camera.enabled = false;
        }
    }
}
