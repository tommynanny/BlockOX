using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareButton : MonoBehaviour {

	// Use this for initialization
	public void ShareButtonTask()
	{
		StartCoroutine(SaveScreenshot());
	}

	IEnumerator SaveScreenshot()
	{
		//	Wait	until	the	end	of	frame
		yield return new WaitForEndOfFrame();
		//	The	SaveScreenshot()	method	returns	the	path	of	the	saved	image
		//	The	provided	file	name	will	be	added	a	".png"	extension	automatically
		Texture2D texture = Sharing.CaptureScreenshot();
		Sharing.ShareTexture2D(texture, "BlockOX", "I score " + GameManager.instance.Score + " in BlockOX! Check it out!");
		Debug.Log("ScreenShot Shared!");
	}
}

