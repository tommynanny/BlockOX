using System.Collections;
using Chronos;
using UnityEngine;
public class PlayerMovement : BaseBehaviour
{

	public float ForwardForce = 200f;
	public float RotateDegree = 3f;
	public GameObject FrontLeft;
	public GameObject FrontRight;
	public GameObject LeftLight;
	public GameObject RightLight;
	Rigidbody rb;
	// Use this for initialization
	void Start()
	{
		rb = time.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		//timeControl ();
		rb.AddForce(transform.forward * ForwardForce * Time.deltaTime);
		if (time.timeScale > 0) Side_Movement_Input();
		GameManager.instance.CarSpeed = time.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
		float angle = transform.localEulerAngles.y;
		GameManager.instance.CarHeadAngle = (angle > 180) ? angle - 360 : angle;
	}

	#region MoveMent
	void Side_Movement_Input()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.position.x > (Screen.width / 2))
			{
				SideMove(Direction.Right);
			}
			if (touch.position.x < (Screen.width / 2))
			{
				SideMove(Direction.Left);
			}
		}

		if (Input.GetAxis("Horizontal") < 0)
		{
			SideMove(Direction.Left);
		}

		if (Input.GetAxis("Horizontal") > 0)
		{
			SideMove(Direction.Right);
		}

		if (Input.GetAxis("Horizontal") == 0 && Input.touchCount == 0)
		{
			SideMove(Direction.None);
		}

	}

	enum Direction { Left, Right, None };
	void SideMove(Direction input)
	{
		switch (input)
		{
			case Direction.Left:
				//rb.AddForce(transform.right * (-SidewayForce * Time.deltaTime),ForceMode.VelocityChange);
				//rb.AddTorque(transform.up * -TurningTorque, ForceMode.VelocityChange);
				transform.Rotate(0, -.5f, 0);
				FrontLeft.transform.localEulerAngles = new Vector3(0, -20, 90);
				FrontRight.transform.localEulerAngles = new Vector3(0, -200, 90);
				LeftLight.GetComponent<MeshRenderer>().material.color = Color.yellow;
				RightLight.GetComponent<MeshRenderer>().material.color = Color.grey;
				break;
			case Direction.Right:
				//rb.AddForce(transform.right * (SidewayForce * Time.deltaTime), ForceMode.VelocityChange);
				//rb.AddTorque(transform.up * TurningTorque, ForceMode.VelocityChange);
				transform.Rotate(0, .5f, 0);
				FrontLeft.transform.localEulerAngles = new Vector3(0, 20, 90);
				FrontRight.transform.localEulerAngles = new Vector3(0, -160, 90);
				LeftLight.GetComponent<MeshRenderer>().material.color = Color.grey;
				RightLight.GetComponent<MeshRenderer>().material.color = Color.yellow;
				break;
			default:
				FrontLeft.transform.localEulerAngles = new Vector3(0, 0, 90);
				FrontRight.transform.localEulerAngles = new Vector3(0, -180, 90);
				LeftLight.GetComponent<MeshRenderer>().material.color = Color.grey;
				RightLight.GetComponent<MeshRenderer>().material.color = Color.grey;
				break;
		}
	}
	#endregion
	// void timeControl ()
	// {
	// 	// Change its time scale on key press
	// 	if (Input.GetKeyDown (KeyCode.Q))
	// 	{

	// 		Timekeeper.instance.Clock ("Player").localTimeScale = -5f;
	// 	}
	// 	else if (Input.GetKeyDown (KeyCode.Alpha2))
	// 	{
	// 		Timekeeper.instance.Clock ("Player").localTimeScale = 0; // Pause
	// 	}
	// 	else if (Input.GetKeyDown (KeyCode.E))
	// 	{
	// 		Timekeeper.instance.Clock ("Player").localTimeScale = 1; // Normal
	// 	}
	// }

}