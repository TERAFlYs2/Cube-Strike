using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Threading;

public sealed class StartGameMenu : Menu
{
	[SerializeField] private Button _backButton;
	[SerializeField] private Button _joinButton;
	[SerializeField] private Button _createButton;
	
	[SerializeField] private DisplayTextHandler _displayTextHandler;
	[SerializeField] private TMP_InputField _inputField;
	
	[SerializeField] private MainMenu _mainMenu;
	
	public INetworkStartGame NetworkStartGame  { get; set; }
	
	private void OnEnable() 
	{
		_backButton?.onClick.AddListener(OnBackButtonClicked);
		_joinButton?.onClick.AddListener(OnJoinOrCreateButtonClicked);
		_createButton?.onClick.AddListener(OnJoinOrCreateButtonClicked);
	}
	
	private void OnDisable() 
	{
		_backButton?.onClick.RemoveListener(OnBackButtonClicked);
		_joinButton?.onClick.RemoveListener(OnJoinOrCreateButtonClicked);
		_createButton?.onClick.RemoveListener(OnJoinOrCreateButtonClicked);
		
		_inputField.text = "";
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
	
	private async void OnJoinOrCreateButtonClicked() 
	{
		string sessionName = _inputField.text;
		
		if (!Validator.SessionNameValidator.IsValidSessionName(sessionName)) return;
		
		await NetworkStartGame?.StartGameAsync(sessionName, _displayTextHandler);

	}
	
}
