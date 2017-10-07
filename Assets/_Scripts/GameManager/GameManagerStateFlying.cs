using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerStateFlying : GameManagerState 
{
	private ColorManager colorManager;

	public GameManagerStateFlying(GameManager gameManager, ColorManager colorManager) : base(gameManager)
	{
		this.colorManager = colorManager;
	}

	public override void OnStateEnter()
	{
		colorManager.EnableInput(true);
	}
}
