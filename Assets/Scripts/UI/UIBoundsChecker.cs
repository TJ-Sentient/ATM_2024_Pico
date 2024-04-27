using Sirenix.OdinInspector;
using UnityEngine;

public class UIBoundsChecker : MonoBehaviour
{
    public RectTransform uiElement;
    public RectTransform canvas;
    
    [Button]
    private void CheckAndRotateUI()
    {
        if (uiElement == null || canvas == null)
            return;

        // Calculate the edges of the UI element in world space
        Vector3[] corners = new Vector3[4];
        uiElement.GetWorldCorners(corners);
        float top = corners[1].y;
        float bottom = corners[3].y;
        float left = corners[0].x;
        float right = corners[2].x;

        // Calculate the edges of the canvas in world space
        Vector3[] canvasCorners = new Vector3[4];
        canvas.GetWorldCorners(canvasCorners);
        float canvasTop = canvasCorners[1].y;
        float canvasBottom = canvasCorners[3].y;
        float canvasLeft = canvasCorners[0].x;
        float canvasRight = canvasCorners[2].x;

        // Check bounds and rotate accordingly
        if (top > canvasTop || bottom < canvasBottom)
        {
            // Rotate 180 degrees around the X-axis
            uiElement.Rotate(new Vector3(180, 0, 0));
        }
        
        if (left < canvasLeft || right > canvasRight)
        {
            // Rotate 180 degrees around the Y-axis
            uiElement.Rotate(new Vector3(0, 180, 0));
        }
    }
}