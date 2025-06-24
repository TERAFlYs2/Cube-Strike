using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenu : Menu, IMainMenuNotifiers
{
	[SerializeField] private Button _exitButton;
	[SerializeField] private Button _logoutButton;
	[SerializeField] private Button _settingsButton;
	[SerializeField] private Button _startGameButton;
	
	[SerializeField] private SettingsMenu _settingsMenu;
	[SerializeField] private StartGameMenu _startGameMenu;
	[SerializeField] private AuthorizationMenu _authorizationMenu;
	
	[SerializeField] private TMP_Text _versionText;
	
	public event Action OnLogoutButtonClickedEvent;
	public string VersionText 
	{ 
		get => _versionText.text;
		set 
		{
			_versionText.text = "v. alpha " + value;
		}
	}
	private void OnEnable()
	{
		_exitButton.onClick.AddListener(OnExitButtonClicked);
		_logoutButton.onClick.AddListener(OnLogoutButtonClicked);
		_settingsButton.onClick.AddListener(OnSettingsButtonClicked);
		_startGameButton.onClick.AddListener(OnStartGameButtonClicked);
	}
	private void OnDisable() 
	{
		_exitButton.onClick.RemoveListener(OnExitButtonClicked);
		_logoutButton.onClick.RemoveListener(OnLogoutButtonClicked);
		_settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
		_startGameButton.onClick.RemoveListener(OnStartGameButtonClicked);
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
	
	private void OnSettingsButtonClicked() 
	{
		MenuSwitcher.OpenMenu(_settingsMenu);
	}
	private void OnExitButtonClicked() 
	{
		Debug.Log("Мы вышли!");
		Application.Quit();
	}
	
	private void OnStartGameButtonClicked() 
	{
		MenuSwitcher.OpenMenu(_startGameMenu);
	}
	
	private void OnLogoutButtonClicked() 
	{
		MenuSwitcher.OpenMenu(_authorizationMenu);
	    OnLogoutButtonClickedEvent?.Invoke();
	}
}
