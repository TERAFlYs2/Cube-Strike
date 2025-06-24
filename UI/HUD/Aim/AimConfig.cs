using UnityEngine;

[CreateAssetMenu(fileName = "HUD", menuName = "Scriptable Objects/HUD/AimConfig")]
public class AimConfig : ScriptableObject
{
    [field: SerializeField] public Texture2D WeaponAim { get; private set; }
    [field: SerializeField] public Texture2D DefaultAim { get; private set; }
    
}
