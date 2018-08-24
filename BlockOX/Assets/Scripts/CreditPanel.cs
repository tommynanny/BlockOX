using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditPanel : MonoBehaviour
{

	public InputField Body;
	public InputField Name;
	public InputField Email;
	// Use this for initialization
	public void SendButton()
	{
		GameManager.instance.SendEmail("Contact", Body.text, Name.text, Email.text);
	}

	private void Start()
	{
		GameManager.instance.RotateAppear(transform,-1, delegate { });
	}

	public void CloseButton()
	{
		GameManager.instance.RotateDisappear(transform, -1, delegate { Destroy(transform.gameObject); });
	}
	

}

