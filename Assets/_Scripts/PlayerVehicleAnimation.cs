using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVehicleAnimation : MonoBehaviour 
{
	// public AnimationCurve xPlaneAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1,1);
	// public AnimationCurve yPlaneAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1,1);
	// public float movementSpeedX = 1f;
	// public float movementSpeedY = 1f;

	public Vector3 rotationSpeed = new Vector3(0,0,0);

	public float AnimationSpeed
	{
		get; set;
	}

	void Awake()
	{
		this.AnimationSpeed = 1;
	}

	// Update is called once per frame
	void Update () 
	{
		//Rotate
		transform.Rotate(rotationSpeed * AnimationSpeed * Time.deltaTime, Space.Self);
	}
}
