using UnityEngine;

[CreateAssetMenu(fileName = "HUDFactoryConfig", menuName = "Scriptable Objects/HUDFactoryConfig")]
public class HUDFactoryConfig : ScriptableObject
{
    [field: SerializeField] public HUDController HUDControllerPrefab { get; private set; }
}
