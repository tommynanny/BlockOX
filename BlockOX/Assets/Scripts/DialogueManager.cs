using System.Collections;
using System.Collections.Generic;
using Ez.Pooly;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

	public Transform CharacterSlot;
	#region Singleton
	public static DialogueManager instance = null;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}
	#endregion
	public GameObject dialoguePanel;
	public GameObject[] JZD_Character_Prefabs;
	public GameObject[] DOG_Character_Prefabs;

	public AudioClip SigaySound;
	public enum CharacterEnum { JZD, DOG, NONE };

 private Queue<string> sentences;
 Transform character;

 private void Start ()
 {
 sentences = new Queue<string> ();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		//Debug.Log(dialogue.name + "starts a conversation.");
		GameManager.instance.PauseAll ();
		dialoguePanel.transform.Find ("NameText").GetComponent<Text> ().text = dialogue.name;
		//dialoguePanel.transform.Find("CharacterAnimation").GetComponent<Animator>().animation = dialogue.anima
		foreach (Transform child in CharacterSlot)
		{
			Pooly.Despawn (child);
		}

		if (dialogue.characterEnum == CharacterEnum.JZD)
		{
			SoundManager.instance.PlaySingle (SigaySound);
		}

		GetCharacter (dialogue);

		dialoguePanel.GetComponent<Animator> ().SetBool ("IsOpen", true);
		sentences.Clear ();
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue (sentence);
		}
		DisplayNextSentence ();

	}

	private GameObject[] GetCharacterArray (CharacterEnum input)
	{
		switch (input)
		{
			case CharacterEnum.JZD:
				return JZD_Character_Prefabs;
			case CharacterEnum.DOG:
				return DOG_Character_Prefabs;
			default:
				return JZD_Character_Prefabs;
		}
	}

	private void GetCharacter (Dialogue dialogue)
	{
		GameObject result = null;
		int index = dialogue.specifiedCharacterIndex;
		CharacterEnum characterEnum = dialogue.characterEnum;
		GameObject[] ChoosePool = GetCharacterArray (characterEnum);

		if (index == -1)
		{
			result = ChoosePool[Random.Range (0, ChoosePool.Length)];
		}

		if (index >= ChoosePool.Length)
		{
			result = ChoosePool[ChoosePool.Length - 1];
		}

		if (index >= 0 && index < ChoosePool.Length)
		{
			result = ChoosePool[index];
		}

		if (result != null)
		{
			Transform slot = CharacterSlot;
			character = Pooly.Spawn (result.transform, slot.transform.position, slot.transform.rotation, slot);
		}

	}
	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue ();
			return;
		}

		string sentence = sentences.Dequeue ();
		//dialoguePanel.transform.Find("SentenceText").GetComponent<Text>().text = sentence;
		StopAllCoroutines ();
		StartCoroutine (TypeSentence (sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialoguePanel.transform.Find ("SentenceText").GetComponent<Text> ().text = "";
		foreach (char letter in sentence.ToCharArray ())
		{
			dialoguePanel.transform.Find ("SentenceText").GetComponent<Text> ().text += letter;
			yield return null;
		}
	}
	void EndDialogue ()
	{
		dialoguePanel.GetComponent<Animator> ().SetBool ("IsOpen", false);
		Pooly.Despawn (character);
		Debug.Log ("End of Conversation");
		GameManager.instance.ResumeAll ();
	}
}