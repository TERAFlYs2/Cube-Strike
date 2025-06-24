using System;
using UnityEngine;

public class PerfomanceStatsHandler : IGamePerfomanceStatsNotifier
{
    private readonly FpsCounter _fpsCounter;
    private NetworkStatsHandler _networkStatsHandler;
    
    public event Action<float> OnFpsChanged 
    {
        add 
        {
            _fpsCounter.OnCurrentFPSChanged += value;
        }
        
        remove 
        {
            _fpsCounter.OnCurrentFPSChanged -= value;
        }
    }
    public event Action<int> OnPingChanged 
    {
        add 
        {
            _networkStatsHandler.OnCurrentPingChanged += value;
        }
        
        remove 
        {
            _networkStatsHandler.OnCurrentPingChanged -= value;
        }
    }
    
    public PerfomanceStatsHandler()
    {
        GameObject fpsCounter = new GameObject("FpsCounter");
        
        _fpsCounter = fpsCounter.AddComponent<FpsCounter>();
        
        
    }
    
    public NetworkStatsHandler CreateNetworkStatsHandler(Fusion.NetworkRunner runner, float updateTime) 
    {
        _networkStatsHandler = new NetworkStatsHandler(runner, updateTime);
        return _networkStatsHandler;
    }

    
}
