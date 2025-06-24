public sealed class MenuController
{
    private readonly MenuConfig _config;
    private readonly MenuSwitcher _menuSwitcher;
    
    public MenuController(MenuConfig config)
    {
        _config = config;
        
        _menuSwitcher = new MenuSwitcher(_config.AllMenus); 
    }
    public void OpenDefaultMenu() 
    {
        _menuSwitcher.OpenMenu(_config.StartMenu);
    }
}
