using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponListConfig", menuName = "Scriptable Objects/WeaponListConfig")]
public class WeaponListConfig : ScriptableObject
{
    [field: SerializeField] public List<IWeaponPickable> AllWeapons { get; private set; }
}
