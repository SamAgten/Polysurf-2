using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour 
{
	public float idleAnimationSpeed = 1f;
	public float maxAnimationSpeed = 10f;

	public Transform vehicleHandle;

	private PlayerVehicle playerVehicle;
	private PlayerVehicleAvatar avatar;
	private PlayerVehicleAnimation animation;
	private Color currentColor;
	private float speed = 0f;
	private float targetSpeed = 0f;
	private bool surfing = false;

	[Inject]
	private void Construct(SaveData saveData, ColorManager colorManager)
	{
		ChangeVehicle(saveData.PlayerVehicle);
		colorManager.onColorSelected += ChangeColor;
	}

	void Awake()
	{
		animation = vehicleHandle.GetComponent<PlayerVehicleAnimation>();
	}

	void Start()
	{
		Land();
	}

	void Update()
	{
		if(surfing)
		{
			speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * playerVehicle.acceleration);
			transform.position = transform.position + transform.forward * speed * Time.deltaTime;
			if(avatar != null)
			{
				float t = speed / playerVehicle.maxSpeed;
				animation.SetAnimationSpeed(Mathf.Lerp(idleAnimationSpeed, maxAnimationSpeed, t));
			}
		}
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

	public void Surf()
	{
		surfing = true;
	}

	public void SetSpeed(float target, bool instant = false)
	{
		targetSpeed = Mathf.Clamp(target, 0, playerVehicle.maxSpeed);
		if(instant)
		{
			speed = targetSpeed;
		}
	}

	public void ChangeColor(Color color)
	{
		if(avatar != null)
			avatar.SetColor(color);
	}

	public void Land()
	{
		if(avatar != null)
			avatar.Land();
	}

	public void Boot()
	{
		if(avatar != null)
			avatar.Boot();
		
		animation.SetAnimationSpeed(1.5f, false);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gate"))
        {

        }
    }
}
