using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour 
{
	public float cameraTrackingSmoothness = 2f;
	public float elasticity = 2f;
	public float snapBackDistance = 20f;
	[SerializeField] private float m_SwaySpeed = .5f;
        [SerializeField] private float m_BaseSwayAmount = .5f;
        [SerializeField] private float m_TrackingSwayAmount = .5f;
        [Range(-1, 1)] [SerializeField] private float m_TrackingBias = 0;

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
	
	void Update()
	{
		if(trackingPlayer)
		{
			relativePlayerDistance = Mathf.Lerp(relativePlayerDistance, targetRelativePlayerDistance, Time.deltaTime * elasticity);
			
			Vector3 desiredPosition = player.transform.position + relativeCameraPosition * relativePlayerDistance;

			transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * cameraTrackingSmoothness);
		}
		else
		{
			 float bx = (Mathf.PerlinNoise(0, Time.time*m_SwaySpeed) - 0.5f);
            float by = (Mathf.PerlinNoise(0, (Time.time*m_SwaySpeed) + 100)) - 0.5f;

            bx *= m_BaseSwayAmount;
            by *= m_BaseSwayAmount;

            float tx = (Mathf.PerlinNoise(0, Time.time*m_SwaySpeed) - 0.5f) + m_TrackingBias;
            float ty = ((Mathf.PerlinNoise(0, (Time.time*m_SwaySpeed) + 100)) - 0.5f) + m_TrackingBias;

            tx *= -m_TrackingSwayAmount;
            ty *= m_TrackingSwayAmount;

            transform.Rotate(bx + tx, by + ty, 0);
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
