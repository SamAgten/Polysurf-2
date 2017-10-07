using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerStateFlying : GameManagerState 
{
	private ColorManager colorManager;
	private Player player;
	private CameraController cameraController;

	public GameManagerStateFlying(GameManager gameManager, ColorManager colorManager, Player player, CameraController cameraController) : base(gameManager)
	{
		this.colorManager = colorManager;
		this.player = player;
		this.cameraController = cameraController;
	}

	public override void OnStateEnter()
	{
		colorManager.EnableInput(true);
		player.Surf();

		player.SetSpeed(100);
		cameraController.StartTrackingPlayer();
	}
}
