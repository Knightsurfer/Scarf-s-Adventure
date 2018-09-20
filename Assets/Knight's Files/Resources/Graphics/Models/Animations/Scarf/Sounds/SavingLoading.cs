using UnityEngine;
using System.IO;


public class SavingLoading : MonoBehaviour
{

	private void Start()
	{
        Debug.Log("Test");
		PlayerData playerData = new PlayerData();
		playerData.position = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
        playerData.rotation = GameObject.FindGameObjectWithTag("Player").transform.localRotation;
        playerData.health = 80;
		
		
		
		string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);



        //
        File.WriteAllText(Application.dataPath + "/Saves/Saving System.json", json);
        //




        PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);
		Debug.Log("Position: " + loadedPlayerData.position);
		Debug.Log("Health: " + loadedPlayerData.health);
	}
	
	private class PlayerData
	{
		public Vector3 position;
		public Quaternion rotation;
		
		public int health;	
	}
	
	
	private void SaveGame()
	{
        //
        //File.WriteAllText(Application.dataPath + "/Saves/Saving System.json", json);
        //
    }



    private void LoadGame()
	{
        //
        //string json = File.ReadAllText(Application.dataPath + "/Saves/Saving System.json");
		//
	}
	
}