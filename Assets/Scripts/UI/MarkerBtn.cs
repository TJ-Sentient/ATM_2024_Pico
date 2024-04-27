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
    
    public void Init(MarkerData markerData)
    {
        MarkerData = markerData;
        MarkerName = markerData.name;
        _idDisplay.text = markerData.id.ToString();
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
        _selectionLine.DOKill();
        _nameDisplay.fontWeight = FontWeight.Bold;
        _selectionLine.DOSizeDelta(new Vector2(300, 2), 0.8f);
    }

    public void MarkerBtnUnSelected()
    {
        _selectionLine.DOKill();
        _nameDisplay.fontWeight = FontWeight.Regular;
        _selectionLine.DOSizeDelta(new Vector2(0, 2), 0.3f);
    }
}