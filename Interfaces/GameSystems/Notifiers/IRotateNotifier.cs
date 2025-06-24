using System;
using UnityEngine;

public interface IRotateNotifier
{
    public event Action<Vector3> OnRotateY;
}
