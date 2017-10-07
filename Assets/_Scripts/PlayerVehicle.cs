using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Vehicle", menuName="Polysurf/Vehicle", order=0)]
public class PlayerVehicle : ScriptableObject 
{
	public GameObject vehicle;
	public float moveSpeed = 1f;
}
