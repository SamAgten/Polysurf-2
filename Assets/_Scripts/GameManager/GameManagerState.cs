using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManagerState
{
	protected GameManager manager;

	public GameManagerState(GameManager gameManager)
	{
		manager = gameManager;
	}

	public virtual void OnStateEnter()
	{

	}

	public virtual void OnStateExit()
	{

	}

	public virtual void Tick()
	{

	}

}
