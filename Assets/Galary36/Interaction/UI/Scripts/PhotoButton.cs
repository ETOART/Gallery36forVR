using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoButton : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    public void TakePhoto()
    {
        gameObject.GetComponent<Image>().color = new Color(0,0,0,0);
        StartCoroutine(TakeScreenshotAndSave());

        gameObject.GetComponent<Image>().color = new Color(1,1,1,1);
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        
        yield return new WaitForEndOfFrame();
       
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        // do something with texture
        NativeGallery.SaveImageToGallery(texture, "Gallary36", Convert.ToString(UnityEngine.Random.Range(0, 10000)));
        // cleanup
        Destroy(texture);
        
    }
}
