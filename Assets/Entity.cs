using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;

[ExecuteInEditMode]
[RequireComponent(typeof(EntityStart))]
public class Entity : MonoBehaviour
{
    /// <summary>
    /// Chosen character
    /// </summary>
    public _Character[] playerInfo = new _Character[5];

    void Start() => game = FindObjectOfType<GameManager>();

    void Update()
    {
        game = FindObjectOfType<GameManager>();
        switch (entityType)
        {
            default:
                GetComponent<MeshFilter>().mesh = game.meshes[0];
                GetComponent<Renderer>().sharedMaterial = game.materials[0];
                break;

            case 1:
                GetComponent<MeshFilter>().mesh = game.meshes[1];
                if(GetComponent<Renderer>())
                {
                    GetComponent<Renderer>().sharedMaterial = game.materials[1];
                }
              
                break;

            case 2:
                GetComponent<MeshFilter>().mesh = game.meshes[1];
                GetComponent<Renderer>().sharedMaterial = game.materials[2];
                break;

            case 3:
                switch(character)
                {
                    default:
                        break;

                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:
                        break;
                }
                break;

            case 4:

                break;

        }
    }
    public int[] context = new int[2];

    #region variables
    GameManager game;


    /// <summary>
    /// Chosen Character Index.
    /// </summary>
    public int character;

    /// <summary>
    /// Index for entity type chosen.
    /// 0 = Nothing,
    /// 1 = Player,
    /// 2 = NPC,
    /// 3 = Interactable,
    /// </summary>
    public int entityType;
    #endregion
}
