using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ColorButton : MonoBehaviour 
{
	public Image circle;
	public event System.Action<Color> onColorPress;

	private readonly int visibleAnimationPara = Animator.StringToHash("Visible");

	private Button button;
	private Animator animator;
	private bool visible = false;

	public Color Color
	{
		get
		{
			return circle.color;
		}
		set
		{
			circle.color = value;
		}
	}

	public bool Visible
	{
		get
		{
			return visible;
		}
		set
		{
			visible = value;
			animator.SetBool(visibleAnimationPara, value);
		}
	}

	void Awake()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OnButtonPress);
		animator = GetComponent<Animator>();
	}

	private void OnButtonPress()
	{
		if(onColorPress != null)
			onColorPress(Color);
	}
}
