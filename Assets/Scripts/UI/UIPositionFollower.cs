using Sirenix.OdinInspector;
using UnityEngine;

public class UIPositionFollower : MonoBehaviour
{
    public Camera        uiCamera;
    public RectTransform uiElement;
    public Transform     target;
    public Vector3       worldOffset = new Vector3(0, 1, 0); // Adjust this as needed

    // void Update()
    // {
    //     PositionUIElement();
    // }

    [Button]
    private void PositionUIElement()
    {
        if (target == null || uiElement == null || uiCamera == null)
            return;

        // Convert the world position of the target into a screen position
        // Vector3 screenPosition = uiCamera.WorldToScreenPoint(target.position + worldOffset);
        uiElement.position = target.position + worldOffset;

        // Check if the target is behind the camera using screenPosition.z
        // if (screenPosition.z < 0)
        // {
        //     uiElement.gameObject.SetActive(false);
        //     return;
        // }

        // Set the UI element to active if it was deactivated
        // uiElement.gameObject.SetActive(true);

        // Convert the screen position to UI element position
        // RectTransformUtility.ScreenPointToLocalPointInRectangle(uiElement.parent as RectTransform, screenPosition, uiCamera, out Vector2 localPoint);

        // Apply the local point to the UI element
        // uiElement.localPosition = localPoint;

        // Anchor to the bottom right
        // uiElement.pivot = new Vector2(1, 0);  // Pivots the RectTransform to its bottom right corner
    }
}