using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshotsave : MonoBehaviour
{
    private static screenshotsave instance;

    [SerializeField] private Camera myCamera;
    private bool takeScreenshotOnNextFrame;

    private void Awake() {
        instance = this;
        //myCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender() {
        if (takeScreenshotOnNextFrame) {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);
            NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(renderResult, "GalleryTest", "Image.png", (success, path) => Debug.Log ("Media save result: " + success + " " + path));

            Debug.Log("Permission result: " + permission);

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
         
            /*byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
            Debug.Log("Saved CameraScreenshot.png");*/

            
         
        }
    }

    private void TakeScreenshot(int width, int height) {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height) {
        instance.TakeScreenshot(width, height);
    }
}
