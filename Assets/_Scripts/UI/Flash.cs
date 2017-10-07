using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class Flash : MonoBehaviour 
{
	public float flashSmoothness = 10f;

	private CanvasGroup canvasGroup;

	[Inject]
	private void Construct(ColorManager colorManager)
	{
		colorManager.onColorSelected += OnColorSelect;
	}

	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	void Update()
	{
		canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, Time.deltaTime * flashSmoothness);
	}

	private void OnColorSelect(Color color)
	{
		Do();
	}

	public void Do()
	{
		canvasGroup.alpha = 1;
	}
}
