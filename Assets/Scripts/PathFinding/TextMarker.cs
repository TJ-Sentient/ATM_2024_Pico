using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextMarker : Marker
{
    public CanvasGroup     markerCG;
    public TextMeshProUGUI markerName;
    public Image           markerFill;


    public override void Init(MarkerController markerController)
    {
        markerController.RegisterMarker(this);
    }

    public override void SelectMarker()
    {
        AnimateMarker(1,1,_yPosRaised);
    }

    public override void DeSelectMarker()
    {
        AnimateMarker(0.5f,0,_yPosDefault);
    }

    public override void ResetMarker()
    {
        AnimateMarker(1,0,_yPosRaised);
    }
    
    private void AnimateMarker(float alpha,float fillAlpha, float yPos)
    {
        markerCG.DOKill();
        transform.DOKill();
        markerFill.DOKill();
        
        markerCG.DOFade(alpha, _duration);
        // transform.DOScale(scale, _duration);
        transform.DOLocalMoveY(yPos, _duration);
        markerFill.DOFade(fillAlpha, _duration);
        markerName.fontWeight = FontWeight.Bold;
    }
}