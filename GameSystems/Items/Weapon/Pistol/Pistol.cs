using UnityEngine;

public class Pistol : WeaponRanged, IWeaponPickable
{
    public WeaponTypeForSlot WeaponTypeForSlot => WeaponTypeForSlot.Additional;
    [field: SerializeField] public Texture TextureForSlot { get; private set; }

    [field: SerializeField] public Vector3 HoldingPoint { get; private set; }
    [field: SerializeField] public Quaternion Orientation { get; private set; }

    protected override void HitTarget(RaycastHit hitInfo)
    {
        base.HitTarget(hitInfo);
        Debug.Log(typeof(Pistol) + " попал в цель!");
    }
}
