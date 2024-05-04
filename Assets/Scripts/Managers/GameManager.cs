using System;
using System.Collections;
using BUT.Utils;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public MarkerController markerController;
    public PathVisualizer   pathVisualizer;
    public UIPopup          uiPopup;
    public OSK              osk;
    public TMP_InputField   searchInput;
    public int              resetTime = 45;
    
    private MarkerBtn _currentPressedBtn;

    public override void Awake()
    {
        base.Awake();
        MarkerBtn.MarkerBtnPressed += OnMarkerBtnPressed;
    }

    private void OnDestroy()
    {
        MarkerBtn.MarkerBtnPressed -= OnMarkerBtnPressed;
    }

    private void OnMarkerBtnPressed(MarkerBtn btn)
    {
        if(btn == null) return;
        if(_currentPressedBtn == btn) return;
        if(_currentPressedBtn != null ) _currentPressedBtn.MarkerBtnUnSelected();
        _currentPressedBtn = btn;

        var marker = markerController.SelectMarker(btn.MarkerData.id);
        if (marker == null)
        {
            Debug.Log($"<color=orange> THE MARKER IS NOT FOUND {btn.MarkerData.id} </color>");
            return;
        }
        pathVisualizer.UpdatePath(marker.transform.position);
        uiPopup.Set(btn.MarkerData.id, btn.MarkerName,marker.transform.position);
        StartResetTimer();
    }

    private void StartResetTimer()
    {
        StopAllCoroutines();
        StartCoroutine(ResetPathOnDelay());
    }

    [Button]
    private void ResetPath()
    {
        if(_currentPressedBtn != null ) _currentPressedBtn.MarkerBtnUnSelected();
        _currentPressedBtn = null;
        markerController.ResetCurrentMarker();
        pathVisualizer.ResetSpline();
        uiPopup.ResetUI();
        // osk.Hide();
        // searchInput.text = "";
    }

    private IEnumerator ResetPathOnDelay()
    {
        yield return new WaitForSeconds(resetTime);
        ResetPath();
    }
}