using System;
using System.Threading.Tasks;
public interface IGameMenuNotifiers
{
    public event Func<Task> OnLeaveSessionButtonClickedEvent;
}
