using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class CustomUIAnimationHandler
{
    private readonly RectTransform _rectTransform;
    
    public bool IsAnimationComplete {get; private set;} = true;
    
    public CustomUIAnimationHandler(RectTransform rectTransform)
    {
        _rectTransform = rectTransform;
    }
    
    public async Task Play(float duration, Vector2 endPosition, Ease animationType = Ease.Linear, Action afterAnimationAction = null) 
    {
        Debug.Log("IsAnimationComplete: " + IsAnimationComplete);
        if (IsAnimationComplete) 
        { 
            IsAnimationComplete = false;
            
            await _rectTransform.DOAnchorPos(endPosition, duration).SetEase(animationType).AsyncWaitForCompletion();
            
            IsAnimationComplete = true;
            afterAnimationAction?.Invoke();
            
        }
    }
   
}
