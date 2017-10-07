using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerStateIntro : GameManagerState 
{
	private CameraController cameraController;
	private Player player;

	public GameManagerStateIntro(GameManager gameManager, CameraController cameraController, Player player) : base(gameManager)
	{
		this.cameraController = cameraController;
		this.player = player;
	}	

	public override void OnStateEnter()
	{
		manager.StartCoroutine(PlayIntro());
	}

	private IEnumerator PlayIntro()
	{
		//Show color!
		player.Boot();
		player.ChangeColor(Color.red);

		yield return new WaitForSeconds(1.5f);

		//1. Switch the camera to race position
		cameraController.LerpTo(cameraController.flyPosition);
		yield return new WaitForSeconds(2);

		manager.SwitchState(GameManager.GAME_STATE.CHOOSE_COLORS);
	}


}
