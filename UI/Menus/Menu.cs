using System;
using UnityEngine;

public abstract class Menu : MonoBehaviour, IActiveMenuChangedNotifier
{
	private bool _isActive;
	protected MenuSwitcher MenuSwitcher;
	
	public event Action<bool> OnChangeActive;
	public bool IsActive 
	{ 
		get => _isActive;
		
		private set 
		{
			_isActive = value;
			OnChangeActive?.Invoke(_isActive);
		} 
	}
	
	public void Initialize(MenuSwitcher menuSwitcher) 
	{
		MenuSwitcher = menuSwitcher;
	}
	public virtual void Show() 
	{
		IsActive = true;
	}
	
	public virtual void Hide() 
	{
		IsActive = false;
	}
}
