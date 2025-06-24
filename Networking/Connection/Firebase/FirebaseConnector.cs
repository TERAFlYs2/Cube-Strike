using System;
using System.Threading.Tasks;
using Firebase;

public class FirebaseConnector 
{
    public event Action<DependencyStatus> OnTryConnected;
    public static bool IsConnected { get; private set; }
    
    public FirebaseConnector()
    {
        if (!IsConnected)
            _= ConnectToFireBaseAsync();
    }
    
    private async Task ConnectToFireBaseAsync()
    {
        var status = await FirebaseApp.CheckAndFixDependenciesAsync();

        UnityEngine.Debug.Log("Status: " + status.ToString());
        
        if (status == DependencyStatus.Available) 
            IsConnected = true;
        
        OnTryConnected?.Invoke(status);
    }
}
