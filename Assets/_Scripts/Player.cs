using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour 
{
	public Transform vehicleHandle;

	private PlayerVehicle playerVehicle;

	[Inject]
	private void Construct(SaveData saveData)
	{
		ChangeVehicle(saveData.PlayerVehicle);
	}

	public void ChangeVehicle(PlayerVehicle vehicle)
	{
		if(playerVehicle != null)
		{
			//Clear the existing one
			foreach(Transform child in vehicleHandle)
			{
				Destroy(child.gameObject);
			}
		}

		//Add a new one
		playerVehicle = vehicle;
		GameObject avatar = GameObject.Instantiate<GameObject>(vehicle.vehicle);
		avatar.transform.SetParent(vehicleHandle, false);
	}
}
