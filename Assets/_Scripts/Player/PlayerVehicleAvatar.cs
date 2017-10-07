using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVehicleAvatar : MonoBehaviour 
{
	public float colorChangeSmoothness = 2f;
	public float colorChangeScaleSmoothness = 10f;
	public float colorChangeScale = 1.5f;
	public Transform landingPosition;

	private Color targetColor;
	private ParticleSystem.MainModule mainPs;
	private new MeshRenderer renderer;
	private bool upAndRunning = true;

	void Awake()
	{
		renderer = GetComponent<MeshRenderer>();
		ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
		mainPs = ps.main;
	}

	void Update()
	{
		renderer.material.color = Color.Lerp(renderer.material.color, targetColor, Time.deltaTime * colorChangeSmoothness);
		mainPs.startColor = Color.Lerp(mainPs.startColor.color, targetColor, Time.deltaTime * colorChangeSmoothness);
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * colorChangeScaleSmoothness);

		if(!upAndRunning)
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime);
			transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime);

			if(Vector3.Distance(transform.localPosition, Vector3.zero) < 0.01f)
			{
				transform.localPosition = Vector3.zero;
				transform.localRotation = Quaternion.identity;
			}
		}
	}

	public void Land()
	{
		transform.localPosition = landingPosition.localPosition;
		transform.localRotation = landingPosition.localRotation;
		ForceColor(Color.black);
		upAndRunning = false;
	}

	public virtual void SetColor(Color color)
	{
		renderer.material.color = Color.white;
		mainPs.startColor = Color.white;
		targetColor = color;
		transform.localScale = new Vector3(colorChangeScale, colorChangeScale, colorChangeScale);
	}

	private void ForceColor(Color color)
	{
		renderer.material.color = color;
		mainPs.startColor = color;
		transform.localScale = Vector3.one;
		targetColor = color;
	}

	
}
