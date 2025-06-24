using System;
using System.Threading.Tasks;

public interface IRegisterMenuNotifiers
{
    public event Func<AuthenticationArgs, bool, Task> OnRegisterButtonClicked;
}
