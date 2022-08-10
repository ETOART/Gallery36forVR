using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShareButton : MonoBehaviour
{
	[SerializeField] private PaintingAnotation paintingAnotation;
    // Start is called before the first frame update
    public void Share()
    {
		StartCoroutine(TakeScreenshotAndShare());
		//new NativeShare().AddFile(paintingAnotation.PaintingData.texturePath)
		//	.SetSubject("Gallary36").SetText(paintingAnotation.PaintingData.Name+" "+ paintingAnotation.PaintingData.Price).SetUrl(" ")
		//	.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
		//	.Share();
	}

	private IEnumerator TakeScreenshotAndShare()
	{
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		// To avoid memory leaks
		Destroy(ss);

		new NativeShare().AddFile(filePath)
			.SetSubject("Gallery 36").SetText("Look that I find!")
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();

		// Share on WhatsApp only, if installed (Android only)
		//if( NativeShare.TargetExists( "com.whatsapp" ) )
		//	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
	}

}
