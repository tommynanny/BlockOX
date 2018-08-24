using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

	public Dialogue dialogue;
	bool canTrigger = true;
	public void triggerDialogue ()
	{
		DialogueManager.instance.StartDialogue (dialogue);
	}

	private void Awake ()
	{

		GetComponent<Renderer> ().enabled = false;

	}

	IEnumerator OnTriggerEnter (Collider other)
	{
		if (canTrigger && other.tag == "Player")
		{
			yield return null;
			canTrigger = false;
			triggerDialogue ();
			Destroy (gameObject);
		}
	}
}