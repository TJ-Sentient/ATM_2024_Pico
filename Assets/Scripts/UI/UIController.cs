using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private MarkerBtn      _markerBtnPrefab;
    [SerializeField] private Transform      _gridParent;
    [SerializeField] private OSK            _osk;
    [SerializeField] private GameObject     _interactionBlocker;
    [SerializeField] private ScrollRect     _scrollView; 

    [Header("Animation References")]
    [SerializeField] private CanvasGroup _scrollCG;
    [SerializeField] private float       _duration = 1f;
    [SerializeField] private Ease        _ease     = Ease.Linear;
    

    [ReadOnly] [SerializeField] private List<MarkerBtn> _markerBtns;

    private void Awake()
    {
        CsvParser.MarkerDataLoaded += OnMarkerDataLoaded;
    }

    private void Start()
    {
        _osk.ClearInput();
        _osk.Hide();
        _interactionBlocker.SetActive(false);
    }

    private void OnDestroy()
    {
        CsvParser.MarkerDataLoaded -= OnMarkerDataLoaded;
    }

    private void OnMarkerDataLoaded(List<MarkerData> markersData)
    {
        Populate(markersData);
    }


    [Button]
    public void Populate(List<MarkerData> markersData)
    {
        foreach (var markerData in markersData)
        {
            // var markerBtn = _gridController.SpawnMarkerBtn(_markerBtnPrefab);
            var markerBtn = Instantiate(_markerBtnPrefab,_gridParent);
            markerBtn.Init(markerData);
            _markerBtns.Add(markerBtn);
        }
    }
    
    public void FilterBasedOnInput(string searchText) {
        foreach (var marker in _markerBtns) {
            // Assuming you have a way to show/hide or activate/deactivate markers
            bool isMatch = marker.MarkerName.ToLower().Contains(searchText.ToLower());
            marker.gameObject.SetActive(isMatch); // This line assumes each marker is a GameObject
        }
    }

    public void OnSearchSelected()
    {
        // Fade Out All Other elements
        AnimateAlpha(_scrollCG,0.4f, _duration,_ease);
        // Show OSK
        _osk.Show();
        _interactionBlocker.SetActive(true);
    }

    public void CloseOSK()
    {
        AnimateAlpha(_scrollCG,1f, _duration,_ease);
        _osk.Hide();
        _interactionBlocker.SetActive(false);
    }

    public void ClearInput()
    {
        _scrollView.verticalNormalizedPosition = 0;
        _osk.ClearInput();
        CloseOSK();
    }

    public static Tweener AnimateAlpha(CanvasGroup cg, float alpha, float duration, Ease ease, float delay = 0)
    {
        cg.DOKill();
        cg.interactable = alpha > 0;
        cg.blocksRaycasts = alpha > 0;
        return cg.DOFade(alpha, duration).SetEase(ease).SetDelay(delay);
    }
}