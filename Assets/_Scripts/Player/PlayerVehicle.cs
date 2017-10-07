using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Vehicle", menuName="Polysurf/Vehicle", order=0)]
public class PlayerVehicle : ScriptableObject 
{
	public PlayerVehicleAvatar vehicle;
	
	public float maxSpeed = 30f;
	public float acceleration = 4f;
	
}
