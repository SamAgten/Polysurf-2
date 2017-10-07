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

		// //Can we spin up the player's avatar?
		// PlayerVehicleAnimation animation = GameObject.FindObjectOfType<PlayerVehicleAnimation>();
		// if(animation != null)
		// {
		// 	float targetSpeed = 1.5f;
		// 	while(Mathf.Abs(animation.AnimationSpeed - targetSpeed) > 0.1f)
		// 	{
		// 		animation.AnimationSpeed = Mathf.Lerp(animation.AnimationSpeed, targetSpeed, Time.deltaTime * 2f);
		// 		yield return null;
		// 	}
		// 	animation.AnimationSpeed = targetSpeed;
		// }

		//1. Switch the camera to race position
		cameraController.LerpTo(cameraController.flyPosition);
		yield return new WaitForSeconds(2);

		manager.SwitchState(GameManager.GAME_STATE.CHOOSE_COLORS);
	}


}
