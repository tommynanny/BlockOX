using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue  {

	public string name;

	public DialogueManager.CharacterEnum characterEnum = DialogueManager.CharacterEnum.NONE;
	public int specifiedCharacterIndex = -1;

	[TextArea(3,5)]
	public string[] sentences;
	
	
}
