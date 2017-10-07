using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerStateShowColors : GameManagerState 
{
	private ColorManager colorManager;

	public GameManagerStateShowColors(GameManager gameManager, ColorManager colorManager) : base(gameManager)
	{
		this.colorManager = colorManager;
	}

	public override void OnStateEnter()
	{
		colorManager.EnableInput(false);
		colorManager.onFinishShowingColors += FinishedShowingColors;
		colorManager.ShowColors(2);
	}

	private void FinishedShowingColors()
	{
		manager.SwitchState(GameManager.GAME_STATE.FLYING);
	}
}
