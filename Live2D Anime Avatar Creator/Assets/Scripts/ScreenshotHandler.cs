using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    public void SS() 
    {
        screenshotsave.TakeScreenshot_Static(3000,3000);
    }
}
