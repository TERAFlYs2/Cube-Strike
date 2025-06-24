using UnityEngine;

public interface IWeaponPickable
{
    public WeaponTypeForSlot WeaponTypeForSlot { get; }
    public Texture TextureForSlot { get; }
    
    public Vector3 HoldingPoint { get; }
    public Quaternion Orientation { get; }
}
