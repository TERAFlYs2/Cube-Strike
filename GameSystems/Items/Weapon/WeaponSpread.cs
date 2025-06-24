using System;
using UnityEngine;

[Serializable]
public class WeaponSpread
{
    [SerializeField] private Vector3 _spreadForce;

    public Vector3 GetSpreadDirection(Transform firePoint) 
    {
        float offsetX = UnityEngine.Random.Range(-_spreadForce.x, _spreadForce.x);
        float offsetY = UnityEngine.Random.Range(-_spreadForce.y, _spreadForce.y);
        float offsetZ = UnityEngine.Random.Range(-_spreadForce.z, _spreadForce.z);
        
        Vector3 foward = firePoint.forward;
        
        return new Vector3(foward.x + offsetX, foward.y + offsetY, foward.z + offsetZ);
    }
}
