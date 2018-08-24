using Chronos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenuEffect : BaseBehaviour
{
	public AudioClip IconDropSound;
	public Text CountDownText;
	public Timeline CountDownTimeline;
	public AudioClip TickSound;
	public AudioClip EndBell;
	// Use this for initialization
	[SerializeField] [ReadOnly] int _Count = 15;
	public int Count { set { if (Count >= 1) _Count = value; } get { return _Count; } }
	public IEnumerator AfterAnimation()
	{
		SoundManager.instance.PlaySingle(IconDropSound);
		while (Count >= 1)
		{
			CountDown();
			SoundManager.instance.PlaySingle(TickSound);
			yield return CountDownTimeline.WaitForSeconds(1f);
		}

		//SceneManager.LoadScene(2);
		transform.parent.GetComponent<DeathPanel>().CloseForm();
		GameManager.instance.ResumeAll();
		SoundManager.instance.PlaySingle(EndBell);
		GameManager.instance.GoToScene(2, false, true, 4f);
		//Initiate.Fade(2, Color.white, 1.0f);
	}

	private void CountDown()
	{
		Count--;
		CountDownText.color = Count <= 5 ? Color.red : Color.black;
		CountDownText.text = Count.ToString();

	}

	public void StopAnimation()
	{
		GetComponent<Animator>().enabled = false;
	}

	public void SkipCountDown()
	{
		CountDown();
	}
}
