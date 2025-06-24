using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AuthenticationMenu : Menu
{
    [SerializeField] private MainMenu _mainMenu;
    
    [SerializeField] private Button _confirmButton;
    
    [SerializeField] protected TMP_InputField EmailField;
    [SerializeField] protected TMP_InputField PasswordField;
    [SerializeField] protected Toggle RememberMeToggle;
    
    protected IAuthenticationNotifiers _authenticationNotifiers;
    
    public IAuthenticationNotifiers AuthenticationNotifiers 
    {
        get => _authenticationNotifiers;
        
        set 
        {
            if (_authenticationNotifiers != null) 
            {
                _authenticationNotifiers.OnAuthenticationComplete -= OnAuthenticationComplete;
            }
            
            _authenticationNotifiers = value;
            
            _authenticationNotifiers.OnAuthenticationComplete += OnAuthenticationComplete;
        }
    }
    protected virtual void OnEnable()
    {
        _confirmButton.onClick.AddListener(ConfirmButtonClicked);
    }

    protected virtual void OnDisable() 
    {
        _confirmButton.onClick.RemoveListener(ConfirmButtonClicked);
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
    protected abstract void ConfirmButtonClicked();

    protected bool IsValidAuthenticationInput() 
    {
        if (!Validator.AuthenticationValidator.IsValidEmail(EmailField.text)) 
        {
            Debug.LogError("Невірний формат пошти");
            return false;
        }
    
        if (!Validator.AuthenticationValidator.IsValidPassword(PasswordField.text)) 
        {
            Debug.LogError("Невірний формат паролю");
            return false;
        }
        
        return true;
    }
    
    protected void OpenMainMenu() 
    {
        MenuSwitcher.OpenMenu(_mainMenu);
    }
   
    private void OnAuthenticationComplete(Firebase.Auth.AuthResult authResult) 
    {
        OpenMainMenu(); 
    }
}
