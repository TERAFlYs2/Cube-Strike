using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine;

public struct AuthenticationArgs 
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

public enum AuthenticationType 
{
    Authorization,
    Registration
}
public class AuthenticationHandler : IAuthenticationNotifiers
{
    private const string RememberMeKey = nameof(RememberMeKey);
    
    private DependencyStatus _status;
    private IRegisterMenuNotifiers _registerMenuNotifiers;
    private IAuthMenuNotifiers _authMenuNotifiers;
    private IMainMenuNotifiers _mainMenuNotifiers;
    
    private FirebaseUser _currentUser;
    
    public event Action<AuthResult> OnAuthenticationComplete;
    public event Action<AuthResult> OnAuthorizationComlpete;
    public event Action<AuthResult> OnRegistrationComlpete;
    public bool UserAlreadyAuthorized { get; private set; } = false;

    public IRegisterMenuNotifiers RegisterMenuNotifiers 
    {
        get => _registerMenuNotifiers;
        
        set 
        {
            if (_registerMenuNotifiers != null) 
            {
                _registerMenuNotifiers.OnRegisterButtonClicked -= TryToRegisterAsync;
            }
            
            _registerMenuNotifiers = value;
            _registerMenuNotifiers.OnRegisterButtonClicked += TryToRegisterAsync;
        }
    }
    
    public IAuthMenuNotifiers AuthMenuNotifiers 
    {
        get => _authMenuNotifiers;
        
        set 
        {
            if (_authMenuNotifiers != null) 
            {
                _authMenuNotifiers.OnLoginButtonClicked -= TryToSignInAsync;
            }
            
            _authMenuNotifiers = value;
            _authMenuNotifiers.OnLoginButtonClicked += TryToSignInAsync;
        }
    }
    
    public IMainMenuNotifiers MainMenuNotifiers 
    {
        get => _mainMenuNotifiers;
        
        set 
        {
            if (_mainMenuNotifiers != null) 
            {
                _mainMenuNotifiers.OnLogoutButtonClickedEvent -= SignOut;
            }
            
            _mainMenuNotifiers = value;
            _mainMenuNotifiers.OnLogoutButtonClickedEvent += SignOut;
            
        }
    }
    public AuthenticationHandler(FirebaseConnector firebaseConnector)
    { 
        CheckSavedUserAuthorization();
        
        firebaseConnector.OnTryConnected += (status) => 
        {
            _status = status;
        };
    }
    
    public async Task TryToRegisterAsync(AuthenticationArgs authenticationArgs, bool remember) 
    {
        await AuthenticateAsync(authenticationArgs, AuthenticationType.Registration, remember);
    }
    
    public async Task TryToSignInAsync(AuthenticationArgs authenticationArgs, bool remember) 
    {
        await AuthenticateAsync(authenticationArgs, AuthenticationType.Authorization, remember);
        
    }
    
    private async Task AuthenticateAsync(AuthenticationArgs authenticationArgs, AuthenticationType authenticationType, bool remember) 
    {
        if (!FirebaseConnector.IsConnected) 
        {
            Debug.LogError("Ошибка подключения: " + _status);
            return;
        }
        
        string username = authenticationArgs.Login;
        string email = authenticationArgs.Email;
        string password = authenticationArgs.Password;
        
        AuthResult result = null;
        
        switch (authenticationType) 
        {
            case AuthenticationType.Registration:
                
                Task<AuthResult> registerTask = FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password);

                result = await registerTask;
                
                if (registerTask.IsCompletedSuccessfully) 
                {
                    _currentUser = result.User;
                        
                    await TryRegisterUserNameAsync(username);
                    
                    OnRegistrationComlpete?.Invoke(result);
                }
                else 
                {
                    Debug.LogError("Ошибка регистрации: " ); 
                    return;
                }  
                
                break;
            
            case AuthenticationType.Authorization:

                Task<AuthResult> authTask = FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password);
                
                result = await authTask;
                
                    if (authTask.IsCompletedSuccessfully) 
                    {
                        _currentUser = result.User;
                        OnAuthorizationComlpete?.Invoke(result);
                        
                        Debug.Log("Вы успешно авторизировались!");
                    }
                        
                    else 
                        Debug.LogError("Ошибка авторизации");
                
                break;
        }
        
        SaveRememberMe(remember);
        OnAuthenticationComplete?.Invoke(result);
  
    }
    
    private async Task TryRegisterUserNameAsync(string username)
    {
        var profile = new UserProfile
        {
            DisplayName = username
        };

        try
        {
            await _currentUser.UpdateUserProfileAsync(profile);
            Debug.Log("Никнейм успешно установлен!");
            Debug.Log($"Ваш email: {_currentUser.Email}, Ваш ник: {_currentUser.DisplayName}");
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Ошибка при установке никнейма: " + ex.Message);
        }
    }
    
    private void CheckSavedUserAuthorization()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null && GetRememberMe())
            UserAlreadyAuthorized = true;
            
        else 
            UserAlreadyAuthorized = false;
    }
    
    public void SignOut() 
    {
        if (FirebaseConnector.IsConnected) 
        {
            SaveRememberMe(false);
            
            FirebaseAuth.DefaultInstance.SignOut();
            Debug.Log("Ми вийшли");
        }
        
    }
    
    private bool GetRememberMe()
    {
        if (!PlayerPrefs.HasKey(RememberMeKey))
            return false;

        return Convert.ToBoolean(PlayerPrefs.GetInt(RememberMeKey));
    }
    private void SaveRememberMe(bool remember)
    {
        int rememberMe = Convert.ToInt32(remember);
        
        PlayerPrefs.SetInt(RememberMeKey, rememberMe);
        
        PlayerPrefs.Save();
    }
}
