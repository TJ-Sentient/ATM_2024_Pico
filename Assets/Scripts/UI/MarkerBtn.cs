using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarkerBtn : MonoBehaviour
{
    public static event Action<MarkerBtn> MarkerBtnPressed;
    
    public MarkerData MarkerData { get; private set; }
    public string     MarkerName { get; private set; }

    [SerializeField] private HorizontalLayoutGroup _layoutGroup;
    [SerializeField] private Button                _button;
    [SerializeField] private TextMeshProUGUI       _idDisplay;
    [SerializeField] private TextMeshProUGUI       _nameDisplay;
    [SerializeField] private RectTransform         _selectionLine;

    private readonly Color _white = new Color(1, 1, 1, 1);
    private readonly Color _purple = new Color(0.3843137f, 0.1686275f, 0.3843137f, 1);
    
    public void Init(MarkerData markerData)
    {
        MarkerData = markerData;
        MarkerName = markerData.name;
        _idDisplay.text = markerData.id > 0 ? markerData.id.ToString() : "";
        _nameDisplay.text = MarkerName;
        
        _button.onClick.AddListener(MarkerBtnSelected);
        gameObject.name = MarkerName + "_btn";

        _selectionLine.sizeDelta = new Vector2(0, 2);
        StartCoroutine(DisableLayoutNextFrame());
    }

    private IEnumerator DisableLayoutNextFrame()
    {
        yield return new WaitForEndOfFrame();
        _layoutGroup.enabled = false;
    }

    public void MarkerBtnSelected()
    {
        Debug.Log($"<color=cyan> MARKER SELECTED : {MarkerData.id} </color>");
        MarkerBtnPressed?.Invoke(this);
        // _nameDisplay.DOKill();
        _selectionLine.DOKill();
        _nameDisplay.fontWeight = FontWeight.Bold;
        // _nameDisplay.DOColor(_purple, 0.8f);
        _selectionLine.DOSizeDelta(new Vector2(300, 2), 0.8f);
    }

    public void MarkerBtnUnSelected()
    {
        // _nameDisplay.DOKill();
        _selectionLine.DOKill();
        _nameDisplay.fontWeight = FontWeight.Regular;
        // _nameDisplay.DOColor(_white, 0.3f);
        _selectionLine.DOSizeDelta(new Vector2(0, 2), 0.3f);
    }
}