using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour 
{
	public Transform vehicleHandle;

	private PlayerVehicle playerVehicle;
	private PlayerVehicleAvatar avatar;
	private Color currentColor;

	[Inject]
	private void Construct(SaveData saveData, ColorManager colorManager)
	{
		ChangeVehicle(saveData.PlayerVehicle);
		colorManager.onColorSelected += ChangeColor;
	}

	public void ChangeVehicle(PlayerVehicle vehicle)
	{
		if(playerVehicle != null)
		{
			//Clear the existing one
			if(avatar != null)
				Destroy(avatar.gameObject);
		}

		//Add a new one
		playerVehicle = vehicle;
		avatar = GameObject.Instantiate<PlayerVehicleAvatar>(vehicle.vehicle);
		avatar.transform.SetParent(vehicleHandle, false);
	}

	public void ChangeColor(Color color)
	{
		if(avatar != null)
			avatar.SetColor(color);
	}
}
