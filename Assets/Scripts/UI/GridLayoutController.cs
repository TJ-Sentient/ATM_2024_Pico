using UnityEngine;

public class GridLayoutController : MonoBehaviour
{
    [SerializeField] private Transform column1;
    [SerializeField] private Transform column2;

    private int _btnCount = 0;

    public MarkerBtn SpawnMarkerBtn(MarkerBtn     _markerBtnPrefab)
    { 
        _btnCount++;
        return Instantiate(_markerBtnPrefab, _btnCount % 2 != 0 ? column1 : column2);
    }
}