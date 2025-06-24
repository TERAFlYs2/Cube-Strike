using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponPickupSystemConfig", menuName = "Scriptable Objects/WeaponController/WeaponPickupSystemConfig")]
public class WeaponPickupSystemConfig : ScriptableObject
{
    [field: SerializeField] public float DropForce { get; private set; } = 5f;
    [field: SerializeField] public float PickupDistance { get; private set; } = 15f;
}
