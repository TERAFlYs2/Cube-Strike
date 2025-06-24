using TMPro;
using UnityEngine;

public class GamePerfomanceStatsMenu : Menu
{
    [SerializeField] private TMP_Text _fpsText;
    [SerializeField] private TMP_Text _pingText;

    private IGamePerfomanceStatsNotifier _gamePerfomanceStatsNotifier;
    
    public IGamePerfomanceStatsNotifier GamePerfomanceStatsNotifier 
    {
        get => _gamePerfomanceStatsNotifier;
        
        set 
        {
            if (_gamePerfomanceStatsNotifier == null) 
            {
                _gamePerfomanceStatsNotifier = value;
                
                _gamePerfomanceStatsNotifier.OnFpsChanged += UpdateFpsText;
                _gamePerfomanceStatsNotifier.OnPingChanged += UpdatePingText;
            }
            else 
            {
                _gamePerfomanceStatsNotifier.OnFpsChanged -= UpdateFpsText;
                _gamePerfomanceStatsNotifier.OnPingChanged -= UpdatePingText;
                
                _gamePerfomanceStatsNotifier = value;
                
                _gamePerfomanceStatsNotifier.OnFpsChanged += UpdateFpsText;
                _gamePerfomanceStatsNotifier.OnPingChanged += UpdatePingText;
            }
        }
    }
    
    private void UpdateFpsText(float countFps) 
    {
        _fpsText.text = $"{countFps} fps";
    }
    
    private void UpdatePingText(int ping) 
    {
        _pingText.text = $"{ping} ms";
    }
}
