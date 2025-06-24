using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

public class AuthorizationMenu : AuthenticationMenu, IAuthMenuNotifiers
{   
    [SerializeField] private Button _registerMenuButton;
    [SerializeField] private RegistrationMenu _registrationMenu;
    public event Func<AuthenticationArgs, bool, Task> OnLoginButtonClicked;

    private void Start()
    {
    
        if (_authenticationNotifiers.UserAlreadyAuthorized) OpenMainMenu();
    }
    protected override void ConfirmButtonClicked() 
    {
        if (!IsValidAuthenticationInput()) return;
        
        AuthenticationArgs loginArgs = new()
        {
            Email = EmailField.text,
            Password = PasswordField.text
        };
        
        OnLoginButtonClicked?.Invoke(loginArgs, RememberMeToggle.isOn);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _registerMenuButton.onClick.AddListener(RegisterMenuButtonClicked);
    }
    
    protected override void OnDisable() 
    {
        base.OnDisable();
        _registerMenuButton.onClick.RemoveListener(RegisterMenuButtonClicked);
    }
    
    private void RegisterMenuButtonClicked() 
    {
        MenuSwitcher.OpenMenu(_registrationMenu);
    }
}
