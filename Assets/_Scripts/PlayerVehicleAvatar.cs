using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVehicleAvatar : MonoBehaviour 
{
	public float colorChangeSmoothness = 2f;
	public float colorChangeScale = 1.5f;

	private Color targetColor;
	private ParticleSystem.MainModule mainPs;
	private new MeshRenderer renderer;

	void Awake()
	{
		renderer = GetComponent<MeshRenderer>();
		ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
		mainPs = ps.main;
	}

	void Start()
	{
		SetColor(Color.red);
	}

	void Update()
	{
		renderer.material.color = Color.Lerp(renderer.material.color, targetColor, Time.deltaTime * colorChangeSmoothness);
		mainPs.startColor = Color.Lerp(mainPs.startColor.color, targetColor, Time.deltaTime * colorChangeSmoothness);
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * colorChangeSmoothness);
	}

	public virtual void SetColor(Color color)
	{
		renderer.material.color = Color.white;
		mainPs.startColor = Color.white;
		targetColor = color;
		transform.localScale = new Vector3(colorChangeScale, colorChangeScale, colorChangeScale);
	}

	
}
