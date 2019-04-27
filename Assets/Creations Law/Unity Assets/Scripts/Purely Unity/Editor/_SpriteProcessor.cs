using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteProcessor : AssetPostprocessor
{
    


    void OnPostprocessTexture(Texture2D texture)
    {
        string lowerCaseAssetPath = assetPath.ToLower();
        bool isInSpriteDirectory = lowerCaseAssetPath.IndexOf("/ui/") != -1;
        


        if(isInSpriteDirectory)
        {
            
            if (texture.width != texture.height)
            {
                TextureImporter textureImporter = (TextureImporter)assetImporter;
                textureImporter.textureType = TextureImporterType.Sprite;                
                //Debug.Log("x: " + texture.width + ", y: " + texture.height);
            }
           
        }

    }

}


public class SelectTag : ScriptableWizard
{
    public string searchTag = "Select a tag.";


    [MenuItem("My Tools/Select All of Tag...")]
    static void SelectAllOfTag()
    {
        ScriptableWizard.DisplayWizard<SelectTag>("Select All Of Tag...", "Make Selection", "Select All Players");
    }

    private void OnWizardCreate()
    {
        GameObject[] gameObjects = new GameObject[99];
        int i = 0;
        foreach (PlayerController player in FindObjectsOfType<PlayerController>())
        {
            gameObjects[i] = player.gameObject;
            i++;
        }


        Selection.objects = gameObjects;
    }



}
