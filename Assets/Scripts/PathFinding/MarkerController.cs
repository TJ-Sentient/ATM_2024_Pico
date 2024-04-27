using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MarkerController : SerializedMonoBehaviour
{
    public Canvas markerCanvas;
    
    [SerializeField] [ReadOnly] private Dictionary<int, Marker> _markersDict;

    private Marker _currentMarker;
    
    private void Start()
    {
        _markersDict = new Dictionary<int, Marker>();
        
        // Get all children
        foreach (Transform child in transform)
        {
            // Add Marker component to each child if it does not already have one
            Marker marker = child.gameObject.GetComponent<Marker>();
            if (marker == null) // Check if the child does not already have a Marker component
            {
                marker = child.gameObject.AddComponent<Marker>();
                marker.Init(this);
            }
        }
    }

    public void RegisterMarker(Marker marker)
    {
        _markersDict.Add(marker.ID, marker);
    }

    public Marker SelectMarker(int id)
    {
        if (!_markersDict.ContainsKey(id))
        {
            ResetCurrentMarker();
            return null;
        }
        
        foreach (var marker in _markersDict)
        {
            if(marker.Key == id) marker.Value.SelectMarker();
            else marker.Value.DeSelectMarker();
        }

        return _markersDict[id];
    }

    private void ResetCurrentMarker()
    {
        if(_currentMarker!= null) _currentMarker.ResetMarker();
    }
}
