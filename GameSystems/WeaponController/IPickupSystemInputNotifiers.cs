using System;
using System.Threading.Tasks;
using UnityEngine;
public interface IPickupSystemInputNotifiers
{
    public event Func<Transform, Task> OnPickupInput;
    public event Func<Transform, Task> OnDropInput;
}
