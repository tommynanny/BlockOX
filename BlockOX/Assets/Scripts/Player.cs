using Chronos;
using Ez.Pooly;
using UnityEngine;
//using UnityEngine.Rendering.PostProcessing;
public class Player : BaseBehaviour
{
	public static GameObject instance;
	public static GameObject Car;

	public static Player Code;
	public TextMesh MileTextMesh;
	//public PostProcessProfile currentEffect;

	[ReadOnly] public bool ReadyToDestroy = true;


	public GameObject TheCanvas;
	public GameObject DeathPanelPrefab;
	public GameObject WastedPrefab;

	public GameObject BloodBar;
	public void Awake()
	{
		instance = gameObject;
		Car = transform.Find("Car").gameObject;
		Code = this;
	}

	private void Update()
	{

		BloodBar.GetComponent<ProgressBarPro>().Value = GameManager.instance.CarHealth / 100f;

		MileTextMesh.text = Mathf.FloorToInt(gameObject.transform.position.z).ToString("F0");
		GameManager.instance.Distance = Mathf.FloorToInt(gameObject.transform.position.z);

		//if (gameObject.transform.position.y < -10f && ReadyToDestroy)
		//{
		//	ReadyToDestroy = false;
		//	DestroyCar();
		//}
		if (gameObject.transform.position.y < -10f && ReadyToDestroy)
		{
			GameManager.instance.CarHealth = 0;
			ReadyToDestroy = false;
			DestroyCar();
		}

		if (GameManager.instance.CarHealth == 0 && ReadyToDestroy)
		{
			ReadyToDestroy = false;
			DestroyCar();
		}


	}

	public void DestroyCar()
	{
		//Timekeeper.instance.Clock("Player").localTimeScale = 0.2f;
		LevelManager.instance.StopRecord();
		OpenWastedEffect();
		SoundManager.instance.FadeOut(1f);
		GameManager.instance.ColorOut(1f);

		//first explode the car into pieces;
		time.Plan(
			1f,
			false,
			delegate
			{
				GameObject container = Player.Car.GetComponent<MeshExploder>().Explode();
				foreach (Transform child in container.transform)
				{
					Timeline tl = child.gameObject.AddComponent<Timeline>();
					tl.mode = TimelineMode.Global;
					tl.globalClockKey = "Fragments";
					child.GetComponent<Collider>().isTrigger = true;
					child.GetComponent<Timeline>().rigidbody.useGravity = false;
				}
				time.rigidbody.isKinematic = true;
				Car.SetActive(false);
				return container;
			},
			delegate (GameObject container)
			{
				Destroy(container);
				Car.SetActive(true);
				Timekeeper.instance.Clock("Fragments").localTimeScale = 1f;
				SoundManager.instance.FadeIn(1f);
				GameManager.instance.ColorIn(1f);
			}
		);

		time.Plan(
			4f,
			false,
			delegate
			{
				Timekeeper.instance.Clock("Fragments").localTimeScale = 0f;
				Timekeeper.instance.Clock("Player").localTimeScale = 0f;

				Pooly.DespawnAllClonesInCategory("Treasure");
				Pooly.DespawnAllClonesInCategory("Building");
				//OpenDeathPanel();
			},
			delegate
			{
				Timekeeper.instance.Clock("Fragments").localTimeScale = -5f;
				GameManager.instance.CarHealth = 100;
			}
		);

	}



	void OnExhaustRewind()
	{
		if (!ReadyToDestroy)
		{
			time.rigidbody.isKinematic = false;
			ReadyToDestroy = true;
			Timekeeper.instance.Clock("Player").localTimeScale = 1f;
		}
	}

	// Use this for initialization
	public void OpenWastedEffect()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag("WastedEffect");
		foreach (GameObject go in gos)
			Destroy(go);

		GameObject temp = Instantiate(WastedPrefab);
		temp.transform.SetParent(TheCanvas.transform, false);
	}


	public void OpenDeathPanel()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag("DeathPanel");
		foreach (GameObject go in gos)
			Destroy(go);

		GameObject temp = Instantiate(DeathPanelPrefab);
		temp.transform.SetParent(TheCanvas.transform, false);
	}

}