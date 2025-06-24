using UnityEngine;

[CreateAssetMenu(fileName = "CharacterInputConfig", menuName = "Scriptable Objects/Character/CharacterMovementInputConfig")]
public class CharacterMovementInputConfig : ScriptableObject
{
    [Header("Keyboard")]
    [field: SerializeField] public KeyCode RunButton = KeyCode.LeftShift;
    [field: SerializeField] public KeyCode JumpButton = KeyCode.Space;
    [field: SerializeField] public KeyCode OpenGameMenuButton = KeyCode.Escape;
}
