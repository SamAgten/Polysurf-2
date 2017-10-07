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
	private ParticleSystem particleSystem;
	private new MeshRenderer renderer;
	private bool upAndRunning = true;
	private bool booting = false;
	private Vector3 defaultPosition;
	private Quaternion defaultRotation;

	void Awake()
	{
		renderer = GetComponent<MeshRenderer>();
		particleSystem = GetComponentInChildren<ParticleSystem>();
	}

	void Start()
	{
	}

	void Update()
	{
		renderer.material.color = Color.Lerp(renderer.material.color, targetColor, Time.deltaTime * colorChangeSmoothness);
		ParticleSystem.MainModule mainPS = particleSystem.main;
		mainPS.startColor = Color.Lerp(mainPS.startColor.color, targetColor, Time.deltaTime * colorChangeSmoothness);
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * colorChangeScaleSmoothness);

		if(booting)
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPosition, Time.deltaTime);
			//transform.localRotation = Quaternion.Lerp(transform.rotation, defaultRotation, Time.deltaTime);

			if(Vector3.Distance(transform.localPosition, Vector3.zero) < 0.01f)
			{
				transform.localPosition = defaultPosition;
				transform.localRotation = defaultRotation;
				booting = false;
				upAndRunning = true;
			}
		}
	}

	public void Land()
	{
		if(!upAndRunning) return;

		defaultPosition = transform.localPosition;
		defaultRotation = transform.localRotation;
		transform.localPosition = landingPosition.localPosition;
		transform.localRotation = landingPosition.localRotation;
		ForceColor(Color.black);
		particleSystem.Stop();
		upAndRunning = false;
	}

	public void Boot()
	{
		if(upAndRunning) return;
		booting = true;
		particleSystem.Play();
	}

	public virtual void SetColor(Color color)
	{
		renderer.material.color = Color.white;
		ParticleSystem.MainModule mainPS = particleSystem.main;
		mainPS.startColor = Color.white;
		targetColor = color;
		transform.localScale = new Vector3(colorChangeScale, colorChangeScale, colorChangeScale);
	}

	private void ForceColor(Color color)
	{
		renderer.material.color = color;
		ParticleSystem.MainModule mainPS = particleSystem.main;
		mainPS.startColor = color;
		transform.localScale = Vector3.one;
		targetColor = color;
	}
}
