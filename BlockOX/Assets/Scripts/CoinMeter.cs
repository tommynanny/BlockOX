using System.Collections;
using System.Collections.Generic;
using Ez.Pooly;
using UnityEngine;

public class CoinMeter : MonoBehaviour
{

	[HideInInspector] public Vector3 HitPoint;
	public ParticleSystem LevelUpEffect;

	int ShowLevel = 1;
	TextMesh LevelText;
	TextMesh ValueText;
	Transform BarFill;
	bool hasReachedEnd = false;
	[SerializeField] float _Value = 0;
	float Value
	{
		set
		{
			if (value < 0) _Value = 0;
			else if (value > 1) _Value = 1;
			else _Value = value;
		}
		get { return _Value; }
	}

	[SerializeField] float _CurrentValue = 0;
	float CurrentValue
	{
		set
		{
			if (value < 0) _CurrentValue = 0;
			else if (value > 1) _CurrentValue = 1;
			else _CurrentValue = value;
		}
		get { return _CurrentValue; }
	}

	int _TextValue = 0;
	int TextValue
	{
		get { return _TextValue; }
		set
		{
			if (value == _TextValue) return;
			_TextValue = value;
		}
	}

	bool canUpdateValue = true;

	private void Start ()
	{

		LevelText = transform.Find ("LevelText").GetComponent<TextMesh> ();
		BarFill = transform.Find ("Fill");
		ValueText = transform.Find ("ValueText").GetComponent<TextMesh> ();

		ShowLevel = GameManager.instance.level;
		LevelUpEffect.Stop ();
		//Player.instance.transform.position = new Vector3 (0, 1, 102);
		ValueText.text = TextValue.ToString ();
	}

	// Use this for initialization
	IEnumerator OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Coin" && col.GetComponent<Coin> ().hasCollected)
		{
			yield return null;
			CoinManager.instance.AddCoins(1);
			col.gameObject.GetComponent<MeshExploder> ().Explode ();
			if (col.GetComponent<Coin> ().PoolControlled)
			{
				Pooly.Despawn (col.gameObject.transform);
			}
			else
			{
				Destroy (col.gameObject);
			}

		}
	}

	void Update ()
	{
		Value = LevelManager.instance.getXpBarValue ();
		//transform.Find ("Fill").Find ("CoinMeter_Fill").GetComponent<Renderer> ().enabled = Value == 0 ? false : true;
		ValueText.transform.position = GameManager.instance.CoinHitPoint + new Vector3 (0, 3f, 0);
		BarFill.localScale = new Vector3 (CurrentValue, 1, 1);
		GameManager.instance.CoinHitPoint = BarFill.position + new Vector3 (BarFill.Find ("CoinMeter_Fill").GetComponent<Renderer> ().bounds.size.x, 0, 0);
		ValueText.text = TextValue.ToString ();
		if ((CoinManager.instance.Coins != TextValue || ShowLevel != GameManager.instance.level) && canUpdateValue)
		{
			hasReachedEnd = false;
			canUpdateValue = false;
			StartCoroutine (UpdateValue ());
		}
	}


	IEnumerator UpdateValue ()
	{
		//ValueText.gameObject.GetComponent<Animator> ().SetBool ("Jump", true);
		//int direction = Value > CurrentValue ? 1 : -1;
		//int delta = Mathf.RoundToInt((GameManager.instance.CoinCount - TextValue) / (Value - CurrentValue));

		hasReachedEnd = ShowLevel == GameManager.instance.level? true : false;
		ValueText.fontStyle = FontStyle.Italic;
		// when adding up
		while (Mathf.Abs (Value - CurrentValue) >= 0.02f || !hasReachedEnd)
		{
			if (Mathf.Abs (1 - CurrentValue) <= 0.01f)
			{
				hasReachedEnd = true;
				CurrentValue = 0;
				UpdateLevel ();
			}
			CurrentValue += 0.01f;
			//TextValue++; //* delta;
			yield return new WaitForSeconds (.005f);
			//yield return null;
		}
		ValueText.fontStyle = FontStyle.Normal;
		yield return null;
		CurrentValue = Value;
		TextValue = CoinManager.instance.Coins;
		ValueText.gameObject.GetComponent<Animator> ().SetTrigger ("Jump");
		canUpdateValue = true;
	}

	void UpdateLevel ()
	{
		ShowLevel = GameManager.instance.level;
		LevelText.gameObject.GetComponent<Animator> ().SetTrigger ("Jump");
		LevelText.text = "Lv." + ShowLevel.ToString ();
		LevelManager.instance.PlayLevelUpSound ();
		LevelUpEffect.Play ();
	}

}