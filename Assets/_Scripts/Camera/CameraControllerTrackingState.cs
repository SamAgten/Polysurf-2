using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CameraController
{
	public class CameraControllerTrackingState : CameraControllerState
	{
		private ColorManager colorManager;
		private Player player;

		private Vector3 relativeCameraPosition;

		public CameraControllerTrackingState(
			CameraController cameraController,
			ColorManager colorManager,
			Player player
		) : base(cameraController)
		{
			this.colorManager = colorManager;
			this.player = player;
		}

		public override void OnStateEnter()
		{
			colorManager.onColorSelected += SnapBack;
			relativeCameraPosition = cameraController.transform.position - player.transform.position;
		}

		public override void OnStateExit()
		{
			colorManager.onColorSelected -= SnapBack;
		}

		public override void Tick()
		{
			cameraController.relativePlayerDistance = Mathf.Lerp(cameraController.relativePlayerDistance, cameraController.targetRelativePlayerDistance, Time.deltaTime * cameraController.elasticity);
			Vector3 desiredPosition = player.transform.position + relativeCameraPosition * cameraController.relativePlayerDistance;
			cameraController.transform.position = Vector3.Lerp(cameraController.transform.position, desiredPosition, Time.deltaTime * cameraController.cameraTrackingSmoothness);
		}

		private void SnapBack(Color c)
		{
			cameraController.relativePlayerDistance = cameraController.snapBackDistance;
		}
	}
}
