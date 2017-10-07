using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour 
{
	public float cameraTrackingSmoothness = 2f;

	private bool trackingPlayer = false;
	private Vector3 relativeCameraPosition;
	[Inject] private Player player;

	void Update()
	{
		if(trackingPlayer)
		{
			Vector3 desiredPosition = player.transform.position + relativeCameraPosition;
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
}
