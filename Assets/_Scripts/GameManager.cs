using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour 
{
	private GameManagerState currentState;
	private Dictionary<GAME_STATE, GameManagerState> states = new Dictionary<GAME_STATE, GameManagerState>();

	public enum GAME_STATE
	{
		MAIN_MENU,
		FLYING,
		CHOOSE_COLORS
	}

	[Inject]
	private void Construct(
		GameManagerStateMainMenu mainMenuState,
		GameManagerStateFlying flyingState,
		GameManagerStateShowColors showColorsState
	)
	{
		//Main menu state
		states[GAME_STATE.MAIN_MENU] = mainMenuState;
		states[GAME_STATE.FLYING] = flyingState;
		states[GAME_STATE.CHOOSE_COLORS] = showColorsState;
	}

	void Start()
	{
		SwitchState(GAME_STATE.MAIN_MENU);
	}

	void Update()
	{
		if(currentState != null)
		{
			currentState.Tick();
		}
	}

	public void SwitchState(GAME_STATE gameState)
	{
		
		if(currentState != null && currentState != states[gameState])
		{
			currentState.OnStateExit();
		}

		currentState = states[gameState];
		currentState.OnStateEnter();
	}

	
}
