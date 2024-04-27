using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewManager : MonoBehaviour
{
    public ScrollRect scrollRect;

    private void Awake()
    {
        MarkerBtn.MarkerBtnPressed += OnMarkerBtnPressed;
    }
    
    private void OnDestroy()
    {
        MarkerBtn.MarkerBtnPressed -= OnMarkerBtnPressed;
    }

    private void OnMarkerBtnPressed(MarkerBtn btn)
    {
        UpdateScrollViewContent();
    }

    // Call this method before making changes to the content of the scroll view
    public void PreserveScrollPosition()
    {
        // Save the current scroll position
        PlayerPrefs.SetFloat("LastScrollPosition", scrollRect.verticalNormalizedPosition);
    }

    // Call this method after changes to the content are done
    public void RestoreScrollPosition()
    {
        // Restore the saved scroll position
        float savedPosition = PlayerPrefs.GetFloat("LastScrollPosition", 1.0f); // Default to 1.0 (top) if not found
        scrollRect.verticalNormalizedPosition = savedPosition;
    }

    // Example usage:
    // Suppose you update the content in your ScrollView like adding or removing elements
    public void UpdateScrollViewContent()
    {
        PreserveScrollPosition();

        // Perform your content update logic here

        // After updating, wait a frame to let the UI system handle the layout update, then restore position
        StartCoroutine(RestoreScrollPositionNextFrame());
    }

    private IEnumerator RestoreScrollPositionNextFrame()
    {
        yield return new WaitForEndOfFrame(); // Ensuring layout recalculates before restoring the scroll position
        // Yield again to ensure we are at the very end of the frame rendering cycle
        yield return new WaitForEndOfFrame();
        RestoreScrollPosition();
    }
}