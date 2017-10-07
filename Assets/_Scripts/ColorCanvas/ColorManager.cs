using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour 
{
	public const int MAX_COLORS = 4;
	public ColorButton[] colorButtons = new ColorButton[MAX_COLORS];
	public event System.Action<Color> onColorSelected;

	private Coroutine setColorsRoutine = null;
	private CanvasGroup canvasGroup;

	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	void Start()
	{
		foreach(ColorButton button in colorButtons)
		{
			button.onColorPress += ColorSelect;
		}

		SetColors(new Color[]{
			Color.red, Color.red, Color.blue, Color.blue
		});
	}

	public void SetColors(Color[] colors)
	{
		canvasGroup.alpha = 1;
		
		if(colors.Length > MAX_COLORS)
		{
			Debug.LogError("[" + this + "] Cannot set more than 4 colors");
			return;
		}

		//Set the colors
		int j = 0;
		for(int i = 0; i < MAX_COLORS; i++)
		{
			colorButtons[i].Color = colors[j];
			j++;
			if(j > colors.Length)
			{
				j = 0;
			}
		}

		if(setColorsRoutine == null)
			setColorsRoutine = StartCoroutine(ShowButtonsRoutine());
	}

	public void HideColors()
	{
		if(setColorsRoutine != null)
			StopCoroutine(setColorsRoutine);

		setColorsRoutine = null;

		foreach(ColorButton button in colorButtons)
			button.Visible = false;

		canvasGroup.alpha = 0;
	}

	private IEnumerator ShowButtonsRoutine()
	{
		float delay = 0.5f;

		foreach(ColorButton button in colorButtons)
		{
			button.Visible = true;
			yield return new WaitForSeconds(delay);
		}

		setColorsRoutine = null;
	}

	private void ColorSelect(Color color)
	{
		if(onColorSelected != null)
		{
			onColorSelected(color);
		}
	}
}
