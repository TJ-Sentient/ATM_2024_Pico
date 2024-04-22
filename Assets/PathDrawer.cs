using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PathDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    
    public void Visualize(NavMeshAgent agent)
    {
        StartCoroutine(UpdatePath(agent));
    }
    
    IEnumerator UpdatePath(NavMeshAgent agent)
    {
        yield return new WaitForEndOfFrame(); // Ensure the path has been updated in the NavMesh system

        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            DrawPath(agent.path);
        }
    }

    void DrawPath(NavMeshPath path)
    {
        lineRenderer.positionCount = path.corners.Length; // Set the number of positions to the number of corners in the path
        lineRenderer.SetPositions(path.corners);          // Set the positions of the LineRenderer to the corners of the path
        lineRenderer.enabled = true;                      // Enable the LineRenderer
    }
}