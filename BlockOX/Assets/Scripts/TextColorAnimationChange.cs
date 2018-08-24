using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextColorAnimationChange : MonoBehaviour {

	// Use this for initialization
	void ChangeToWhite()
	{
		GetComponent<TextMesh>().color = Color.white;
	}

	void ChangeToYellow()
	{
		GetComponent<TextMesh>().color = Color.yellow;
	}
}
