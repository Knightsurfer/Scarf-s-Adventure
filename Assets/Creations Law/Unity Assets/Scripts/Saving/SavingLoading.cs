using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;



public class SavingLoading : MonoBehaviour
{
    readonly string fileName = "Saving System.json";
    private string filePath;
    public PlayerController player;

    public static SavingLoading _instance;
    public static SavingLoading Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public GameData gameData;









    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
           
        if(gameData == null)
        {
            gameData = new GameData();
        }
        filePath = Path.Combine(Application.dataPath + "/Game/Saves/", fileName);
    }


    protected void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void SaveGame()
	{
        gameData.playerPos = player.transform.localPosition;
        gameData.playerRot = player.transform.localRotation;
        gameData.playerHealth = player.health;
        

        string json = JsonUtility.ToJson(gameData);

        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        
        File.WriteAllText(filePath, json);
        //
    }


    public void LoadGame()
	{
        player.transform.localPosition = gameData.playerPos;
        player.transform.localRotation = gameData.playerRot;
        player.health = gameData.playerHealth;

        string json;
        if (File.Exists(filePath))
        {
            json = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.Log("No Save File Exists.");
        }
    }
}

public class GameData
{
    public string saveName;
    public int playerHealth;
    public Vector3 playerPos;
    public Quaternion playerRot;
}