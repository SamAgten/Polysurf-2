using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour 
{
	public float cameraTrackingSmoothness = 2f;
	public float elasticity = 2f;
	public float snapBackDistance = 20f;

	private bool trackingPlayer = false;
	private Vector3 relativeCameraPosition;
	private float targetRelativePlayerDistance = 1f;
	private float relativePlayerDistance = 1f;
	[Inject] private Player player;
	
	[Inject]
	private void Construct(ColorManager colorManager)
	{
		colorManager.onColorSelected += SnapBack;
	}


	void Start()
	{
		StartTrackingPlayer();
	}

	void Update()
	{
		if(trackingPlayer)
		{
			relativePlayerDistance = Mathf.Lerp(relativePlayerDistance, targetRelativePlayerDistance, Time.deltaTime * elasticity);
			
			Vector3 desiredPosition = player.transform.position + relativeCameraPosition * relativePlayerDistance;

			transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * cameraTrackingSmoothness);
		}
	}

	public void StartTrackingPlayer()
	{
		if(player == null) return;

		relativeCameraPosition = transform.position - player.transform.position;		
		trackingPlayer = true;
	}

	public void StopTracking()
	{
		trackingPlayer = false;
	}

	public void SnapBack(Color c)
	{
		if(trackingPlayer)
		{
			relativePlayerDistance = snapBackDistance;
		}
	}
}
