using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    [SerializeField] private Camera          _viewCam;
    [SerializeField] private RectTransform   _canvas;
    [SerializeField] private TextMeshProUGUI _markerName;

    [Space(10)]
    [SerializeField] private Vector3 _worldOffset;
    
    private CanvasGroup   _canvasGroup;
    private RectTransform _uiElement;
    private Coroutine     _delayedShowRoutine;
    private int           _currentMarkerID;

    private void Awake()
    {
        _canvasGroup = _canvas.GetComponent<CanvasGroup>();
        _uiElement = GetComponent<RectTransform>();
    }

    private void Start()
    {
        UIController.AnimateAlpha(_canvasGroup, 0, 0, Ease.Linear);
        PathVisualizer.AgentTraversing += OnAgentTraversing;
    }

    private void OnDestroy()
    {
        PathVisualizer.AgentTraversing -= OnAgentTraversing;
    }

    private void OnAgentTraversing(float tweenDuration)
    {
        Debug.Log($"<color=orange> AGENT TRAVELLING WITH DELAY {tweenDuration} </color>");
        if(_delayedShowRoutine != null) StopCoroutine(_delayedShowRoutine);
        _delayedShowRoutine = StartCoroutine(ShowWithDelay(tweenDuration));
    }

    public void Set(int id, string name, Vector3 location)
    {
        _canvasGroup.DOKill();
        UIController.AnimateAlpha(_canvasGroup, 0, 0.1f, Ease.Linear).OnComplete(() =>
        {
            Debug.Log($"<color=cyan> POP RECEIVED ID {id} </color>");
            _currentMarkerID = id;
            if (id < 0) return;
            _markerName.text = name;
            transform.position = location + _worldOffset;
            StartCoroutine(CheckAndRotateUI());
        });
    }

    public void ResetUI()
    {
        if(_delayedShowRoutine != null) StopCoroutine(_delayedShowRoutine);
        _delayedShowRoutine = null;
        _currentMarkerID = 0;
        _canvasGroup.DOKill();
        UIController.AnimateAlpha(_canvasGroup, 0, 0.1f, Ease.Linear);
    }

    private IEnumerator ShowWithDelay(float tweenDuration)
    {
        yield return new WaitForSeconds(tweenDuration);
        if (_currentMarkerID < 0)
        {
            _delayedShowRoutine = null;
            yield break;
        }
        Debug.Log($"<color=orange> SHOWING WITH DELAY </color>");
        UIController.AnimateAlpha(_canvasGroup, 1, 0.5f, Ease.Linear);
        yield return new WaitForSeconds(15);
        UIController.AnimateAlpha(_canvasGroup, 0, 0.5f, Ease.Linear);
        _delayedShowRoutine = null;
    }
    
    private IEnumerator CheckAndRotateUI()
    {
        if (_uiElement == null || _canvas == null) yield break;

        yield return new WaitForEndOfFrame();
        
        _uiElement.localRotation = Quaternion.identity;
        _markerName.transform.localRotation = Quaternion.identity;
        
        yield return null;

        // Calculate the edges of the UI element in world space
        Vector3[] corners = new Vector3[4];
        _uiElement.GetWorldCorners(corners);
        float top = corners[1].y;
        float bottom = corners[3].y;
        float left = corners[0].x;
        float right = corners[2].x;

        // Calculate the edges of the canvas in world space
        Vector3[] canvasCorners = new Vector3[4];
        _canvas.GetWorldCorners(canvasCorners);
        float canvasTop = canvasCorners[1].y;
        float canvasBottom = canvasCorners[3].y;
        float canvasLeft = canvasCorners[0].x;
        float canvasRight = canvasCorners[2].x;

        // Check bounds and rotate accordingly
        // if (top > canvasTop || bottom < canvasBottom)
        // {
        //     // Rotate 180 degrees around the X-axis
        //     _uiElement.Rotate(new Vector3(180, 0, 0));
        //     _markerName.transform.Rotate(new Vector3(180, 0, 0));
        // }
        
        if (left < canvasLeft)
        {
            // Rotate 180 degrees around the Y-axis
            _uiElement.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            _markerName.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }
    
}