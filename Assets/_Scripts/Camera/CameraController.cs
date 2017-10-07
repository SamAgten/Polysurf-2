using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public partial class CameraController : MonoBehaviour 
{
	[Header("Animation")]
	public float cameraTrackingSmoothness = 2f;
	public float elasticity = 2f;
	public float snapBackDistance = 20f;
	// [SerializeField] private float m_SwaySpeed = .5f;
    //     [SerializeField] private float m_BaseSwayAmount = .5f;
    //     [SerializeField] private float m_TrackingSwayAmount = .5f;
    //     [Range(-1, 1)] [SerializeField] private float m_TrackingBias = 0;

	[Header("Positions")]
	public Transform mainMenuPosition;
	public Transform flyPosition;

	private bool trackingPlayer = false;
	private float targetRelativePlayerDistance = 1f;
	private float relativePlayerDistance = 1f;

	private CameraControllerState currentState;
	private CameraControllerTrackingState trackingState;
	private CameraControllerLerpState lerpState;
	
	[Inject]
	private void Construct(Player player, ColorManager colorManager)
	{
		trackingState = new CameraControllerTrackingState(this, colorManager, player);
		lerpState = new CameraControllerLerpState(this);
	}
	
	void Update()
	{
		if(currentState != null)
			currentState.Tick();
		// else
		// {
		// 	 float bx = (Mathf.PerlinNoise(0, Time.time*m_SwaySpeed) - 0.5f);
        //     float by = (Mathf.PerlinNoise(0, (Time.time*m_SwaySpeed) + 100)) - 0.5f;

        //     bx *= m_BaseSwayAmount;
        //     by *= m_BaseSwayAmount;

        //     float tx = (Mathf.PerlinNoise(0, Time.time*m_SwaySpeed) - 0.5f) + m_TrackingBias;
        //     float ty = ((Mathf.PerlinNoise(0, (Time.time*m_SwaySpeed) + 100)) - 0.5f) + m_TrackingBias;

        //     tx *= -m_TrackingSwayAmount;
        //     ty *= m_TrackingSwayAmount;

        //     transform.Rotate(bx + tx, by + ty, 0);
		// }
	}

	public void StartTrackingPlayer()
	{
		SwitchState(trackingState);
	}

	public void StopTracking()
	{
		SwitchState(null);
	}

	public void LerpTo(Transform target)
	{
		lerpState.SetTarget(target);
		SwitchState(lerpState);
	}

	private void SwitchState(CameraControllerState state)
	{
		if(currentState != null)
			currentState.OnStateExit();

		currentState = state;

		if(currentState != null)
			currentState.OnStateEnter();
	}

	
}
