using System.Collections;
using TMPro;
using UnityEngine;

public enum DisplayTextType 
{
	Text,
	ErrorText
}
public class DisplayTextHandler : MonoBehaviour
{
	private const string ErrorTextType = "Error: ";

    [SerializeField] private Color _textColor;
    [SerializeField] private Color _errorTextColor;

    private Coroutine _currentDisplayRoutine;
    private TMP_Text _textComponent;

    private void Awake()
    {
        _textComponent = GetComponent<TMP_Text>();
    }

    public void DisplayText(string message, DisplayTextType displayTextType, float displayTime = -1f) 
    {
        if (this == null) return;

        Display(message, displayTextType);

        if (displayTime > 0 && gameObject.activeSelf)
        {
            if (_currentDisplayRoutine != null)
            {
                StopCoroutine(_currentDisplayRoutine);
                _currentDisplayRoutine = null;
            }
			
            _currentDisplayRoutine = StartCoroutine(LiveTimeTextRoutine(displayTime));
        }
    }

    private void Display(string message, DisplayTextType displayTextType) 
    {
        _textComponent.text = displayTextType == DisplayTextType.ErrorText 
            ? ErrorTextType + message 
            : message;

        _textComponent.color = displayTextType == DisplayTextType.ErrorText 
            ? _errorTextColor 
            : _textColor;
    }

    private IEnumerator LiveTimeTextRoutine(float displayTime) 
    {
        yield return new WaitForSeconds(displayTime);
        _textComponent.text = "";
        _currentDisplayRoutine = null;
    }
}
