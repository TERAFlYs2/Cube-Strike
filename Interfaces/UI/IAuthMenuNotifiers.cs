using System;
using System.Threading.Tasks;

public interface IAuthMenuNotifiers
{
    public event Func<AuthenticationArgs, bool, Task> OnLoginButtonClicked;
}
