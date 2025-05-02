using UnityEditor;
using UnityEngine;

public class SnapSceneViewToCamera
{
    [MenuItem("Tools/Snap Scene View to Main Camera %#&c")] // Ctrl + Alt + Shift + C (or Cmd on Mac)
    static void SnapToMainCamera()
    {
        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogWarning("No Main Camera found in the scene.");
            return;
        }

        SceneView sceneView = SceneView.lastActiveSceneView;
        if (sceneView != null)
        {
            sceneView.pivot = mainCam.transform.position + mainCam.transform.forward * 10f;
            sceneView.rotation = mainCam.transform.rotation;
            sceneView.size = 10f;
            sceneView.Repaint();
        }
    }
}
