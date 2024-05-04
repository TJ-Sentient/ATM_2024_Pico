using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Marker : MonoBehaviour
{
    [field: SerializeField] public int ID { get; protected set; }
    
    protected float _duration    = 0.5f;
    protected float _yPosDefault = 0.1f;
    protected float _yPosRaised  = 0.5f;

    public abstract void Init(MarkerController markerController);

    public abstract void SelectMarker();

    public abstract void DeSelectMarker();

    public abstract void ResetMarker();
}