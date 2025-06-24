using System.Text.RegularExpressions;
public class AuthenticationValidator
{
    public const byte LoginMinLength = 3;
    public const byte LoginMaxLength = 12;
    
    public const byte PasswordMinLength = 6;
    public const byte PasswordMaxLength = 18;
    
    public const byte EmailMinLength = 6;
    public const byte EmailMaxLength = 16;
    
    
    public bool IsValidLogin(string login) 
    {
        string loginPattern = "^[A-Za-z][A-Za-z0-9_]*$";
    
        bool isLengthValid = 
            !string.IsNullOrEmpty(login) && 
            login.Length >= LoginMinLength && 
            login.Length <= LoginMaxLength;

        return isLengthValid && Regex.IsMatch(login, loginPattern);
    }
    
    public bool IsValidPassword(string password) 
    {
        string pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]).*$";

        return !string.IsNullOrEmpty(password)
            && password.Length >= PasswordMinLength
            && password.Length <= PasswordMaxLength
            && Regex.IsMatch(password, pattern);
    }
    
    public bool IsValidEmail(string email) 
    {
        string emailPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
        return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, emailPattern);
    }
    
}
