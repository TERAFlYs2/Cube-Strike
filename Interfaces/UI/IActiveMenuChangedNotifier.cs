using System;
public interface IActiveMenuChangedNotifier
{
    public event Action<bool> OnChangeActive;
}
