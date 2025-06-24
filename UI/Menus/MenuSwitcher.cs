using System.Collections.Generic;

public sealed class MenuSwitcher
{
	private readonly List<Menu> _menus;
	
	private Menu _currentMenu;
	public MenuSwitcher(List<Menu> menus)
	{
		_menus = menus;
		
		InitializeMenus();
	}
	
	private void InitializeMenus() 
	{
	    foreach (var menu in _menus) 
		{
			menu?.Hide();
			menu?.Initialize(this);
		}
	}
	public void OpenMenu(Menu menu) 
	{
		for (int i = 0; i < _menus.Count; i++) 
		{
			if (_menus[i] == menu) 
			{
				if (_currentMenu != null) 
				{
					_currentMenu.Hide();
				}
				
				menu.Show();
				_currentMenu = menu;
				
				break;
			}
		}	
	}
	
}
