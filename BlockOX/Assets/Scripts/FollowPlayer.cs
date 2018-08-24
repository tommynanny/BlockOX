using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	public Vector3 Offset;
	// Use this for initialization
	private void Start()
	{
		Offset = gameObject.transform.position - Player.instance.transform.position;
	}
	// Update is called once per frame
	void Update () {
		
		//gameObject.transform.rotation = Player.transform.rotation;
		gameObject.transform.position = Player.instance.transform.position + Offset;
		//transform.LookAt(Player.transform);
	}
}
