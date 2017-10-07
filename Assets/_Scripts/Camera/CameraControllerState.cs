using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraControllerState : State 
{
	protected CameraController cameraController;

	public CameraControllerState(CameraController cameraController)
	{
		this.cameraController = cameraController;
	}
}
