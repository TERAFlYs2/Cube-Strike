using System;
using Firebase.Auth;

public interface IAuthenticationNotifiers
{
    public event Action<AuthResult> OnAuthenticationComplete;
    public event Action<AuthResult> OnAuthorizationComlpete;
    public event Action<AuthResult> OnRegistrationComlpete;
    public bool UserAlreadyAuthorized { get; }
}
