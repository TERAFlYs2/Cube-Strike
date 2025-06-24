using Fusion;
using TMPro;
using UnityEngine;
public class ChatMessage : NetworkBehaviour
{
    [SerializeField] private TMP_Text _message;
    
    public string Message  
    {
        get => _message.text;
        
        set 
        {
            _message.text = value;
        }
    }
}
