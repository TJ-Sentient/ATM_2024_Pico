using System.Collections.Generic;
using UnityEngine;

public class PlayModeHandler : MonoBehaviour
{
    public List<GameObject> ObjectsToEnable;
    public List<GameObject> ObjectsToDisable;


    private void Awake()
    {
        foreach (var obj in ObjectsToEnable)
        {
            obj.SetActive(true);
        }
        
        foreach (var obj in ObjectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}