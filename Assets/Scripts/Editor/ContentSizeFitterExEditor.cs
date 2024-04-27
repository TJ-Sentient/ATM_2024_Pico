using UnityEditor;

[CustomEditor(typeof(ContentSizeFitterEx))]
public class ContentSizeFitterExEditor : Editor {
    // override the editor to be able to show the public variables on the inspector.
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
    }
}