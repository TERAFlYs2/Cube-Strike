using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkObject))]
public class ChatController : NetworkBehaviour
{
    [Header("Keys")]

    [SerializeField] private KeyCode _openKey = KeyCode.T;
    [SerializeField] private KeyCode _closeKey = KeyCode.F1;

    [SerializeField] private KeyCode _sendKey = KeyCode.Return;

    [Header("UI elements")]
    [SerializeField] private CanvasGroup _chatCanvasGroup;

    [SerializeField] private TMP_InputField _chatInputField;

    [SerializeField] private ScrollRect _chatScrollRect;

    [SerializeField] private RectTransform _chatContent;

    [SerializeField] private ChatMessage _chatMessagePrefab;

    [Header("Chat options")]
    [SerializeField] private int _maxMessages = 10;

    private readonly Queue<ChatMessage> _messageQueue = new();
    private bool _isChatActive = false;

    private void Awake()
    {
        if (_chatCanvasGroup != null)
        {
            _chatCanvasGroup.alpha = 0f;
            _chatCanvasGroup.interactable = false;
            _chatCanvasGroup.blocksRaycasts = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_openKey) && !_isChatActive)
        {
            Open();
        }
        
        if (Input.GetKeyDown(_closeKey) && _isChatActive)
        {
            Close();
        }

        HandleSending();
    }

    private void HandleSending() 
    {
        if (_isChatActive && Input.GetKeyDown(_sendKey))
        {
            string msg = _chatInputField.text;
            _chatInputField.text = ""; 

            if (!string.IsNullOrWhiteSpace(msg))
            {
                string sender = Runner.LocalPlayer.ToString() ?? "Unknown";
                SendNetworkMessage(sender, msg);
            }

            _chatInputField.ActivateInputField();
        }
    }
    private void Open() 
    {
        _isChatActive = true;
        
        _chatCanvasGroup.alpha = 1f;
        _chatCanvasGroup.interactable = true;
        _chatCanvasGroup.blocksRaycasts = true;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _chatInputField.ActivateInputField();
    }
    
    private void Close() 
    {
        _isChatActive = false;
        
        _chatCanvasGroup.alpha = 0f;
        _chatCanvasGroup.interactable = false;
        _chatCanvasGroup.blocksRaycasts = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisplayMessage(string sender, string message)
    {
        if (_chatMessagePrefab == null || _chatContent == null) return;

        ChatMessage chatMessage = Instantiate(_chatMessagePrefab, _chatContent);
    
        chatMessage.Message = $"<b>{sender}:</b> {message}";

        _messageQueue.Enqueue(chatMessage);

        if (_messageQueue.Count > _maxMessages)
        {
            ChatMessage old = _messageQueue.Dequeue();
            if (old != null)
                Destroy(old);
        }

        Canvas.ForceUpdateCanvases();
        if (_chatScrollRect != null)
        {
            _chatScrollRect.verticalNormalizedPosition = 0f;
        }
    }

    private void SendNetworkMessage(string sender, string message)
    {
        if (Runner == null) return;
        RPC_ReceiveMessage(sender, message);
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    private void RPC_ReceiveMessage(string sender, string message, RpcInfo info = default)
    {
        DisplayMessage(sender, message);
    }
}
