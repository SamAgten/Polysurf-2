using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour 
{
	public const int MAX_COLORS = 4;
	public ColorButton[] colorButtons = new ColorButton[MAX_COLORS];
	public Text timer;
	public event System.Action<Color> onColorSelected;
	public event System.Action onFinishShowingColors;

	private Coroutine showColorsRoutine = null;
	private CanvasGroup canvasGroup;
	private bool enableInput = true;
	private float showColorsTimer = 0;

	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
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

		HideColors();
		EnableInput(false);
	}

	void Update()
	{
		if(showColorsTimer > 0)
		{
			showColorsTimer -= Time.deltaTime;
			if(showColorsTimer <= 0)
			{
				showColorsTimer = 0;
				HideColors();
				if(onFinishShowingColors != null)
					onFinishShowingColors();
			}
		}
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
	}

	public void EnableInput(bool enabled)
	{
		enableInput = enabled;
	}

	public void ShowColors(float time)
	{
		canvasGroup.alpha = 1f;

		//Show buttons
		if(showColorsTimer <= 0)
		{
			StartCoroutine(ShowButtonsRoutine());
		}
		showColorsTimer = time;
	}

	public void HideColors()
	{
		foreach(ColorButton button in colorButtons)
			button.Visible = false;

		canvasGroup.alpha = 0;
	}

	private IEnumerator ShowButtonsRoutine()
	{
		float delay = 0.2f;
		foreach(ColorButton button in colorButtons)
		{
			button.Visible = true;
			yield return new WaitForSeconds(delay);
		}
	}

	private void ColorSelect(Color color)
	{
		if(!enableInput) return;

		if(onColorSelected != null)
		{
			onColorSelected(color);
		}
	}
}
