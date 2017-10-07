using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManagerState : State
{
	protected GameManager manager;

	public GameManagerState(GameManager gameManager)
	{
		manager = gameManager;
	}
}
