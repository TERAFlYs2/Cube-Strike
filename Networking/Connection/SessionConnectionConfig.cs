using UnityEngine;

[CreateAssetMenu(fileName = "SessionConnectionConfig", menuName = "Scriptable Objects/Network/SessionConnectionConfig")]
public class SessionConnectionConfig : ScriptableObject
{
    [field: SerializeField, Range(0, 255)] public byte MaxCountPlayers { get; private set; }
    
    [field: SerializeField] public Fusion.GameMode GameMode { get; private set; }
    
    [field: SerializeField] public string LobbyNameDefault { get; private set; }
}
