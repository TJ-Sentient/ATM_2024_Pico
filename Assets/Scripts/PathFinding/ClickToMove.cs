using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class ClickToMove : MonoBehaviour
{
    public Camera       cam;   // Assign this in the inspector with your scene camera
    public NavMeshAgent agent; // Assign your NavMeshAgent which is attached to the AI character
    // public PathDrawer   pathDrawer;
    public PathVisualizer pathVisualizer;

    private void Awake()
    {
#if !UNITY_EDITOR
        enabled = false;
#endif
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button was clicked
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // Create a ray from the camera to the mouse position
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Perform the raycast
            {
                // Debug.Log("Destination Set");
                // agent.SetDestination(hit.point); // Set the destination of the NavMeshAgent to the point where the raycast hit the collider
                //pathDrawer.Visualize(agent);
                pathVisualizer.UpdatePath(hit.point);
            }
        }
    }
}