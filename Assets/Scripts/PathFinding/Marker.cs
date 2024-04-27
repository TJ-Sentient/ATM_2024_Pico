using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public int ID { get; private set; }

    [ReadOnly] [SerializeField] private Canvas          _markerCanvas;
    [ReadOnly] [SerializeField] private TextMeshProUGUI _idDisplay;
    [ReadOnly] [SerializeField] private Material        _mat;

    private float   _duration     = 0.5f;
    private float   _yPos         = 0.5f;
    private Vector3 _zoomScale    = new Vector3(180,180,180);
    private Vector3 _defaultScale = new Vector3(100,100,100);

    public void Init(MarkerController markerController)
    {
        if (!int.TryParse(name, out int result)) return;
        ID = result;
        markerController.RegisterMarker(this);

        _markerCanvas = Instantiate(markerController.markerCanvas, transform);
        
        _idDisplay = _markerCanvas.GetComponentInChildren<TextMeshProUGUI>();
        _idDisplay.text = ID.ToString();
        _mat = GetComponent<MeshRenderer>().material;
    }

    public void SelectMarker()
    {
        AnimateMarker(1,_zoomScale);
    }

    public void DeSelectMarker()
    {
        AnimateMarker(0.3f,_defaultScale);
    }

    public void ResetMarker()
    {
       AnimateMarker(1,_defaultScale);
    }

    private void AnimateMarker(float alpha,Vector3 scale)
    {
        _mat.DOKill();
        _idDisplay.DOKill();
        transform.DOKill();
        
        _mat.DOFade(alpha, _duration);
        _idDisplay.DOFade(alpha, _duration);
        transform.DOScale(scale, _duration);
        transform.DOLocalMoveY(_yPos, _duration);
    }
}
