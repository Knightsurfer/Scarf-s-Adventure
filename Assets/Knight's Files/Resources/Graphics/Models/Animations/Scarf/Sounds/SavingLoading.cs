using UnityEngine;
using System.IO;

public class SavingLoading : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    private void Start()
	{
        PlayerData playerData = new PlayerData();
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerData.position = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
            playerData.rotation = GameObject.FindGameObjectWithTag("Player").transform.localRotation;
            playerData.health = 80;
        }
		    string json = JsonUtility.ToJson(playerData);
	}
	private class PlayerData
	{
        public int health;

        public Vector3 position;
		public Quaternion rotation;
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
        //  string json = File.ReadAllText(Application.dataPath + "/Saves/Saving System.json");
        //  PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);
        //  Debug.Log("Position: " + loadedPlayerData.position);
        //  Debug.Log("Health: " + loadedPlayerData.health);
        //

    }
}