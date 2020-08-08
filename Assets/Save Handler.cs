using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveEncrypt
{
    #region Variables
    public List<PlayerController> player = new List<PlayerController>();
    readonly BinaryFormatter bf = new BinaryFormatter();

    Vector3 positions;
    Quaternion rotations;
    #endregion

    public void Save(string category, string saveType, string fileName)
    {
        string fileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + Application.productName;
		if (!File.Exists(fileDirectory + "/Data/" + category + "/" + saveType + "/" + fileName + ".dat"))
        {
			
            Directory.CreateDirectory(fileDirectory + "/Data/" + category + "/" + saveType);
			File.Create(fileDirectory + "/Data/" + category + "/" + saveType + "/" + fileName + ".dat").Dispose();
        }
        FileStream file = File.Create(fileDirectory + "/Data/" + category + "/" + saveType + "/" + fileName + ".dat");
        PlayerData data = new PlayerData();

       
		Debug.Log("Game Saved.");
        //Debug.Log(fileDirectory + "/Data/" + category + "/" + saveType + "/" + fileName + ".dat");

        int i = 0;
        int p = 0;
        int r = 0;
        foreach (PlayerController character in player)
        {
            data.health[i] = character.health;
            data.level[i] = character.level;

            positions = character.transform.localPosition;
            data.positionFloat[p] = positions.x;
            data.positionFloat[p + 1] = positions.y;
            data.positionFloat[p + 2] = positions.z;

            rotations = character.transform.localRotation;
            data.rotationFloat[r] = rotations.x;
            data.rotationFloat[r + 1] = rotations.y;
            data.rotationFloat[r + 2] = rotations.z;
            data.rotationFloat[r + 3] = rotations.w;

            data.currentYaw[i] = character.currentYaw;

            i++;
            p += 3;
            r += 4;
        }
        bf.Serialize(file, data);
        file.Close();
    }
    public void Load(string category, string saveType, string fileName)
    {
        string fileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + Application.productName;

        if (File.Exists(fileDirectory + "/Data/" + category + "/" + saveType + "/" + fileName + ".dat"))
        {
            FileStream file = File.Open(fileDirectory + "/Data/" + category + "/" + saveType + "/" + fileName + ".dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            int i = 0;
            int p = 0;
            int r = 0;
            foreach (PlayerController character in player)
            {
                character.health = data.health[i];
                character.level = data.level[i];

                positions = new Vector3(data.positionFloat[p], data.positionFloat[p + 1], data.positionFloat[p + 2]);
                rotations = new Quaternion(data.rotationFloat[r], data.rotationFloat[r + 1], data.rotationFloat[r + 2], data.rotationFloat[r + 3]);

                character.transform.localPosition = positions;
                character.transform.localRotation = rotations;
                character.currentYaw = data.currentYaw[i];
                i++;
                p += 3;
                r += 4;
            }
        }
    }
}
[Serializable]
class PlayerData
{
    public int[] items = new int[2];

    public int[] health = new int[4];
    public int[] level = new int[4];

    public float[] positionFloat = new float[12];
    public float[] rotationFloat = new float[16];

    public float[] currentYaw = new float[4];
}
