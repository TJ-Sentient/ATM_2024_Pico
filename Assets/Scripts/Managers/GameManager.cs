using System;
using System.Collections;
using System.Collections.Generic;
using BUT.Utils;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public MarkerController markerController;
    public PathVisualizer   pathVisualizer;
    public UIPopup          uiPopup;
    
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
        uiPopup.Set(btn.MarkerName,marker.transform.position);
    }
}
