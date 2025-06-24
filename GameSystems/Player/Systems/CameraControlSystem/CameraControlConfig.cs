using UnityEngine;

[CreateAssetMenu(fileName = "CameraControlConfig", menuName = "Scriptable Objects/Character/CameraControlConfig")]
public class CameraControlConfig : ScriptableObject
{
    [Header("Values")]
    [field: SerializeField][Range(0f, 1000f)] public float SensitivityX { get; private set; } = 100f;
    [field: SerializeField][Range(0f, 1000f)] public float SensitivityY { get; private set; } = 100f;
}
