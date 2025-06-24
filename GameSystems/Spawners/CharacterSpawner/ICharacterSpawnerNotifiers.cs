using System;

public interface ICharacterSpawnerNotifiers
{
    public event Action<Game.Character.NetworkCharacterController> OnSpawned;
}
