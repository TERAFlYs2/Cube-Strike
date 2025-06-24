using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
	[SerializeField] private CanvasGroup  _loadingScreen;
	
	//private const float MinimumDisplayTime = 2f;
	
	public static LoadingScreen Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null) 
		{
			Destroy(gameObject);
		}
		else 
		{
			Instance = this;
			
			DontDestroyOnLoad(transform.parent);
		}
	}
	
	public void Hide() 
	{
		if (_loadingScreen == null) return;

        _loadingScreen.alpha = 0f;  
        _loadingScreen.blocksRaycasts = false; 
        _loadingScreen.interactable = false;
    }

    public void Show() 
	{
		if (_loadingScreen == null) return;
		
        _loadingScreen.alpha = 1f; 
        _loadingScreen.blocksRaycasts = true;
        _loadingScreen.interactable = true;
    }
}
