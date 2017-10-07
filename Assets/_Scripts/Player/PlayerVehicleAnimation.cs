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

	private float targetAnimationSpeed;
	public float animationSpeed;


	void Awake()
	{
		SetAnimationSpeed(0);
	}

	// Update is called once per frame
	void Update () 
	{
		animationSpeed = Mathf.Lerp(animationSpeed, targetAnimationSpeed, Time.deltaTime);

		//Rotate
		transform.Rotate(rotationSpeed * animationSpeed * Time.deltaTime, Space.Self);
	}

	public void SetAnimationSpeed(float speed, bool instant = true)
	{
		targetAnimationSpeed = speed;
		if(instant)
			animationSpeed = targetAnimationSpeed;
	}
}
