using System;
using Fusion;

public interface IPlayerConnectNotifiers
{
    public event Action<NetworkRunner, PlayerRef> OnPlayerJoinedEvent;
    public event Action<NetworkRunner, PlayerRef> OnPlayerLeftEvent;
}
