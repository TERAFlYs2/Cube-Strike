using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSpawnerConfig", menuName = "Scriptable Objects/Spawners/CharacterSpawnerConfig")]
public class CharacterSpawnerConfig : ScriptableObject
{
    [Serializable]
    public struct SpawnPoint 
    {
        public Vector3 Position;
        public Vector3 Rotation;
    }
    [field: SerializeField] public Game.Character.NetworkCharacterController Prefab { get; private set; }
    [field: SerializeField] public float RespawnCooldown { get; private set; }
    [field: SerializeField] public List<SpawnPoint> SpawnPoints { get; set; }
    
    [ContextMenu(nameof(CollectData))]
    private void CollectData() 
    {   
        string name = "SpawnPointsGroup";
    
        var childsSpawnPoints = GameObject.FindGameObjectWithTag(name).GetComponentsInChildren<Transform>();
      
        SpawnPoints.Clear();
      
        foreach (var child in childsSpawnPoints) 
        {
            if (child.name == "SpawnPoints") 
            {
                continue;
            }
        
            SpawnPoints.Add(new SpawnPoint
            {
                Position = child.position,
                Rotation = child.rotation.eulerAngles
            });
        }

      //EditorUtility.SetDirty(this);
    }
}
