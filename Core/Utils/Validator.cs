public static class Validator
{
    static Validator() 
    {
        SessionNameValidator = new SessionNameValidator();
        AuthenticationValidator = new AuthenticationValidator();
    }
    public static SessionNameValidator SessionNameValidator { get; private set; }
    public static AuthenticationValidator AuthenticationValidator { get; private set; }
    
}
