using UnityEngine;

[CreateAssetMenu(fileName = "CharacterHealthConfig", menuName = "Scriptable Objects/Character/CharacterHealthConfig")]
public class CharacterHealthConfig : ScriptableObject
{
    [SerializeField] private int _maxHealth;
    [property: SerializeField] public int MaxHealth 
    {
        get => _maxHealth;
        
        private set 
        {
            _maxHealth = Mathf.Max(value, 0);
        }
    }
}
