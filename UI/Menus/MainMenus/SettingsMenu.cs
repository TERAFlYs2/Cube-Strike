using UnityEngine;
using UnityEngine.UI;

public sealed class SettingsMenu : Menu
{
	[SerializeField] private Button _backButton;
	[SerializeField] private MainMenu _mainMenu;
	
	private void OnEnable() 
	{
		_backButton.onClick.AddListener(OnBackButtonClicked);
	}
	
	private void OnDisable() 
	{
		_backButton.onClick.RemoveListener(OnBackButtonClicked);
	}
	public override void Show()
    {
        base.Show();
        gameObject.SetActive(true);
    }
    
    public override void Hide()
    {
        base.Hide();
        gameObject.SetActive(false);
    }
	private void OnBackButtonClicked() 
	{
		MenuSwitcher.OpenMenu(_mainMenu);
	}
}
