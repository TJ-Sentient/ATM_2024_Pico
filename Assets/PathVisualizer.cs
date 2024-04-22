using System.Collections;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class PathVisualizer : MonoBehaviour
{
    public NavMeshAgent   agent;          // Assign your NavMeshAgent in the Inspector
    public SplineComputer splineComputer; // SplineComputer to draw the path
    public SplineRenderer splineRenderer;
    
    private LineRenderer lineRenderer;   // LineRenderer to draw the path
    private Tweener      splineTween;
    

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>(); // Get the LineRenderer attached to this GameObject
    }

    // Call this method to set a new destination and update the path visualization
    public void UpdatePath(Vector3 destination)
    {
        if (agent != null)
        {
            //agent.SetDestination(destination); // Set the destination for the agent
            // StartCoroutine(WaitAndUpdatePath());
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(destination, path);
            // DrawPath(path);
            DrawSpline(path);
        }
    }

    IEnumerator WaitAndUpdatePath()
    {
        // Wait until path is calculated
        while (agent.pathPending)
        {
            yield return null;
        }

        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            DrawPath(agent.path);
        }
        else
        {
            lineRenderer.enabled = false; // Disable line rendering if path is invalid
        }
    }

    void DrawPath(NavMeshPath path)
    {
        lineRenderer.positionCount = path.corners.Length; // Set the number of positions to the number of corners in the path
        lineRenderer.SetPositions(path.corners);          // Set the positions of the LineRenderer to the corners of the path
        lineRenderer.enabled = true;                      // Enable the LineRenderer
    }

    void DrawSpline(NavMeshPath path)
    {
        splineRenderer.clipTo = 0;
        SplinePoint[] points = new SplinePoint[path.corners.Length];
        for (int i = 0; i < path.corners.Length; i++)
        {
            points[i] = new SplinePoint(path.corners[i]);
        }

        // splineComputer.type = Spline.Type.Linear; // Set to Linear for direct paths, Bezier for smoother curves
        splineComputer.SetPoints(points, SplineComputer.Space.World);
        splineComputer.RebuildImmediate(); // Rebuild the spline to update the visualization immediately
        AnimateSpline();
    }

    private void AnimateSpline()
    {
        splineTween?.Kill();
        splineTween = DOTween.To(()=> splineRenderer.clipTo, x=> splineRenderer.clipTo = x, 1, 3);
    }
        
}
