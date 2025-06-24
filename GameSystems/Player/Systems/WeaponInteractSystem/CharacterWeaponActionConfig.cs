using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterWeaponActionConfig", menuName = "Scriptable Objects/Character/CharacterWeaponActionConfig")]
public class CharacterWeaponActionConfig : ScriptableObject
{
    [field: SerializeField] public KeyCode DefaultAttackKey { get; private set; } = KeyCode.Mouse0;
    [field: SerializeField] public KeyCode ReloadKey { get; private set; } = KeyCode.R;
    [field: SerializeField] public KeyCode DropKey { get; private set; } = KeyCode.Q;
    [field: SerializeField] public KeyCode PickupKey { get; private set; } = KeyCode.E;
    

}
