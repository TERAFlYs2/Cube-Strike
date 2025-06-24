using System;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    private int _frameCount = 0;
    private float _timer = 0f;
    private int _currentFPS = 0;

    public int CurrentFPS
    {
        get => _currentFPS;
        
        set 
        {
            _currentFPS = value;
            OnCurrentFPSChanged?.Invoke(_currentFPS);
        }
    }
    public event Action<float> OnCurrentFPSChanged;
    private void Update()
    {
        _frameCount++;
        _timer += Time.deltaTime;

        if (_timer >= 1f)
        {
            CurrentFPS = _frameCount;
            _frameCount = 0;
            _timer = 0f; // или = 0f, если не нужна точность при дробных секундах
        }
    } 
}
