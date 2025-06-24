using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventorySlot : MonoBehaviour
{
    private RawImage _rawImage;
    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
    }
    public Texture WeaponTexture 
    {
        get => _rawImage.texture;
        
        set 
        {
            _rawImage.texture = value;
        }
    }
    
    public Color Color 
    {
        get => _rawImage.color;
        
        set 
        {
            _rawImage.color = value;
        }
    }
    
    [field: SerializeField] public WeaponTypeForSlot WeaponTypeForSlot { get; private set; }

    [field: SerializeField] public GameObject Selector { get; private set; }
    
}
