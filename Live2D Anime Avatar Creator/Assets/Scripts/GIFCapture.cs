/** using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GetSocialSdk.Capture.Scripts;

public class GIFCapture : MonoBehaviour
{

    public GetSocialCapture capture;
    // start recording if something interesting happens in the game
    public void Recordgiffff() 
    {
        capture = GetComponent<GetSocialCapture>();
        capture.StartCapture();
        Debug.Log("Started recording");
        StartCoroutine(RecordAction());
    }

    // stop recording
    IEnumerator RecordAction()
    {
        yield return new WaitForSeconds(9);
        capture.StopCapture();
        Debug.Log("Ended recording");
        Debug.Log("Starting gif generation");
		Action<byte[]> result = bytes =>
		{
			
		};  
		capture.GenerateCapture(result);	
        Debug.Log("done");
    }
}
*/