using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
	public const string SAVE_DATA = "SaveData";

	private DefaultData defaultData;

	[SerializeField]
	private PlayerVehicle playerVehicle;
	public PlayerVehicle PlayerVehicle
	{
		get
		{
			return playerVehicle;
		}
		private set
		{
			playerVehicle = value;
		}
	}

	public SaveData(DefaultData defaultData)
	{
		this.defaultData = defaultData;
		Load();
	}

	public void Load()
	{
		string json = PlayerPrefs.GetString(SAVE_DATA, string.Empty);
		if(json == string.Empty)
		{
			//Load the default data
			playerVehicle = defaultData.defaultVehicle;
		}
		else
		{
			SaveData saveData = JsonUtility.FromJson<SaveData>(json);
			this.playerVehicle = saveData.playerVehicle;
		}
	}

	public void Save()
	{
		string json = JsonUtility.ToJson(this);
		PlayerPrefs.SetString(SAVE_DATA, json);
	}

	[System.Serializable]
	public class DefaultData
	{
		public PlayerVehicle defaultVehicle;
	}

}
