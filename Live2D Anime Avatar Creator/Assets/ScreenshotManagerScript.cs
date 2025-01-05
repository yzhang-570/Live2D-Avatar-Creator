using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject successMsg;
    [SerializeField] private GameObject screenshotManager;
    [SerializeField] private GameObject screenshotCamera;

    public void TakeScreenshot()
    {
        screenshotCamera.SetActive(true);
        screenshotManager.GetComponent<ScreenshotHandler>().SS();
        successMsg.SetActive(true);
        StartCoroutine(DisplayWait());
        StartCoroutine(DisplayWaitMessage());
    }

    IEnumerator DisplayWait()
    {
        yield return new WaitForSeconds((float)1);
        screenshotCamera.SetActive(false);
    }

    IEnumerator DisplayWaitMessage()
    {
        yield return new WaitForSeconds(1);
        successMsg.SetActive(false);
    }
}
