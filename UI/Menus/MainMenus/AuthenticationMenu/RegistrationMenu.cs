using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationMenu : AuthenticationMenu, IRegisterMenuNotifiers
{
    [SerializeField] private TMP_InputField _passwordConfirmationField;
    [SerializeField] private TMP_InputField _usernameField;
    [SerializeField] private Button _exitButton;
    
    [SerializeField] private Image image;
    
    public event Func<AuthenticationArgs, bool, Task> OnRegisterButtonClicked;

    protected override void OnEnable()
    {
        base.OnEnable();
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }
    protected override void ConfirmButtonClicked() 
    {
        if (!IsValidAuthenticationInput()) return;
        
        image.color = Color.yellow;
        
        /*/if (Validator.AuthenticationValidator.IsValidLogin(_usernameField.text)) 
        {
            Debug.LogError("Невірний формат логіну");
            return;
        }/*/
        
        if (_passwordConfirmationField.text != PasswordField.text) 
        {
            Debug.LogError("Паролі не співподають");
            return;
        }
            
        AuthenticationArgs registerArgs = new() 
        {
            Login = _usernameField.text,
            Password = PasswordField.text,
            Email = EmailField.text
        };
        
        OnRegisterButtonClicked?.Invoke(registerArgs, RememberMeToggle.isOn);
    }
    
    private void OnExitButtonClicked() 
    {
        Application.Quit();
    }
}
