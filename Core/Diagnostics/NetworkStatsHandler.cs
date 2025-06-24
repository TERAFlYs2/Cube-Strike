using System;
using Fusion;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ExitGames.Client.Photon;

public class NetworkStatsHandler : IDisposable
{
    private readonly NetworkRunner _runner;
    private readonly float _updateTime;
    public event Action<int> OnCurrentPingChanged;
    
    public NetworkStatsHandler(NetworkRunner runner, float updateTime)
    {
        _runner = runner;
        _updateTime = updateTime;
        
        _= StartPingCounterAsync();
    }
    
    private async Task StartPingCounterAsync() 
    {
        //string currentRegionName = _runner.SessionInfo.Region;
        
        while (true) 
        {
            if (_runner != null && _runner.IsRunning) 
            {          
                //List<RegionInfo> availablRegions = await NetworkRunner.GetAvailableRegions();
                
                //RegionInfo currentRegion = availablRegions.FirstOrDefault((a) => a.RegionCode == currentRegionName);
                
                //int ping = currentRegion.RegionPing;
                
                OnCurrentPingChanged?.Invoke((int)(_runner.GetPlayerRtt(_runner.LocalPlayer) * 1000f));
                
                await Task.Delay(TimeSpan.FromSeconds(_updateTime));
            }
            
            else
                await Task.Yield();
        }
    }

    public void Dispose()
    {
        
    }
}
