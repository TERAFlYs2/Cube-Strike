using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenuGame : Menu, IGameMenuNotifiers
{
    [Header("Keyboard")]
    [SerializeField] private KeyCode _openButton = KeyCode.Escape;
    
    [Header("UI Elements")]
    [SerializeField] private Button _leaveSessionButton;
    [SerializeField] private Button _resumeSessionButton;
    
    private CustomUIAnimationHandler _customPanelAnimation;
    
    public event Func<Task> OnLeaveSessionButtonClickedEvent;

    private void Awake() 
    {
        _customPanelAnimation = new (GetComponent<RectTransform>());
            
        Hide();
    }
    private void Update()
    {
        if (Input.GetKeyDown(_openButton)) 
        {
            if (IsActive) 
                Hide();
            
            else 
                Show(); 
        }
    }
    
    private void OnEnable()
    {
        _leaveSessionButton.onClick.AddListener(OnLeaveSessionButtonClicked);
        _resumeSessionButton.onClick.AddListener(OnResumeSessionButtonClicked);
    }
    private void OnDisable() 
    {
        _leaveSessionButton.onClick.RemoveListener(OnLeaveSessionButtonClicked);
        _resumeSessionButton.onClick.RemoveListener(OnResumeSessionButtonClicked);
    }
    
    private void OnLeaveSessionButtonClicked() 
    {
        OnLeaveSessionButtonClickedEvent?.Invoke();
    }
    private void OnResumeSessionButtonClicked() 
    {
        Hide();
    }
    public override void Show()
    {
        Action showedAction = () => 
        {
            base.Show();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        };
        
        _customPanelAnimation?.Play(1, new Vector2(250, 0), DG.Tweening.Ease.InOutQuad, showedAction);  
    }
    
    public override void Hide()
    {
        Action hidingAction = () => 
        {
            base.Hide();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        };
        
        _customPanelAnimation?.Play(1, new Vector2(-250, 0), DG.Tweening.Ease.InOutQuad, hidingAction);    
    }
    
}
