using UnityEngine;

[CreateAssetMenu(fileName = "CharacterMovementConfig", menuName = "Scriptable Objects/Character/CharacterMovementConfig")]
public class CharacterMovementConfig : ScriptableObject
{
    [field: Header("Physics params")]
    [field: SerializeField] public float WalkSpeed { get; set; }
    [field: SerializeField] public float RunSpeed { get; set; }
    [field: SerializeField] public float JumpForce { get; set; }
    [field: SerializeField] public float Gravity { get; set; }
    [field: SerializeField] public float Drag { get; set; }
    
    [field: SerializeField] public float Mass { get; set; }
    [field: SerializeField] public float Acceleration { get; set; }
    [field: SerializeField] public float Deceleration { get; set; }
    
    [field: SerializeField][Range(0,1)] public float HorizontalSpeedModifier { get; set; }
}
