using System;

public interface IGamePerfomanceStatsNotifier
{
    public event Action<float> OnFpsChanged;
    public event Action<int> OnPingChanged;
}
