using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerStateMainMenu : GameManagerState
{
	public GameManagerStateMainMenu(GameManager gameManager) : base(gameManager)
	{
	}

	public override void Tick()
	{
		if(Input.anyKeyDown)
		{
			StartToFly();
		}
	}

	public void StartToFly()
	{
		manager.SwitchState(GameManager.GAME_STATE.INTRO);
	}
}
