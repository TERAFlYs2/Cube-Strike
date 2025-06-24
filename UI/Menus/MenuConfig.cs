using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MenuConfig 
{
	[field: SerializeField] public Menu StartMenu { get; private set; }
	[field: SerializeField] public List<Menu> AllMenus { get; private set; }
	
	public T GetMenuByType<T>() where T : Menu
	{
	    return AllMenus.FirstOrDefault(a => a.GetType() == typeof(T)) as T;
	}
	
}
