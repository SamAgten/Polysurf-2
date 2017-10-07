using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CameraController
{
	
	public class CameraControllerLerpState : CameraControllerState 
	{
		private Transform target = null;

		public CameraControllerLerpState(CameraController cameraController) : base(cameraController)
		{
		}

		public override void Tick()
		{
			if(target == null) return;

			cameraController.transform.position = Vector3.Lerp(cameraController.transform.position, target.position, Time.deltaTime * cameraController.cameraTrackingSmoothness);
			cameraController.transform.rotation = Quaternion.Slerp(cameraController.transform.rotation, target.rotation, Time.deltaTime * cameraController.cameraTrackingSmoothness);
		}

		public void SetTarget(Transform target)
		{
			this.target = target;
		}
	}

}
