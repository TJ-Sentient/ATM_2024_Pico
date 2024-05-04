using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class NumMarker : Marker
{
    [ReadOnly] [SerializeField] private Canvas          _markerCanvas;
    [ReadOnly] [SerializeField] private TextMeshProUGUI _idDisplay;
    [ReadOnly] [SerializeField] private Material        _mat;

    
    private Vector3 _zoomScale    = new Vector3(180,180,180);
    private Vector3 _defaultScale = new Vector3(100,100,100);

    public override void Init(MarkerController markerController)
    {
        if (!int.TryParse(name, out int result)) return;
        ID = result;
        markerController.RegisterMarker(this);

        _markerCanvas = Instantiate(markerController.markerCanvas, transform);
        
        _idDisplay = _markerCanvas.GetComponentInChildren<TextMeshProUGUI>();
        _idDisplay.text = ID.ToString();
        _mat = GetComponent<MeshRenderer>().material;
    }

    public override void SelectMarker()
    {
        AnimateMarker(1,_zoomScale,_yPosRaised);
    }

    public override void DeSelectMarker()
    {
        AnimateMarker(0.5f,_defaultScale,_yPosDefault);
    }

    public override void ResetMarker()
    {
        AnimateMarker(1,_defaultScale,_yPosDefault);
    }

    private void AnimateMarker(float alpha,Vector3 scale, float yPos)
    {
        _mat.DOKill();
        _idDisplay.DOKill();
        transform.DOKill();
        
        _mat.DOFade(alpha, _duration);
        _idDisplay.DOFade(alpha, _duration);
        transform.DOScale(scale, _duration);
        transform.DOLocalMoveY(yPos, _duration);
    }
}