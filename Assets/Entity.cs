using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;

[ExecuteInEditMode]
public class Entity : MonoBehaviour
{
    public Mesh[] meshes = new Mesh[5];
    public Material[] materials = new Material[5];

    public _Character[] playerInfo = new _Character[5];
    public int entityType;
    public int character;

    void Start()
    {
        
    }
   
    void Update()
    {
        switch(entityType)
        {
            default:
                GetComponent<MeshFilter>().mesh = meshes[0];
                GetComponent<Renderer>().sharedMaterial = materials[0];
                break;

            case 1:
                GetComponent<MeshFilter>().mesh = meshes[1];
                if(GetComponent<Renderer>())
                {
                    GetComponent<Renderer>().sharedMaterial = materials[1];
                }
              
                break;

            case 2:
                GetComponent<MeshFilter>().mesh = meshes[1];
                GetComponent<Renderer>().sharedMaterial = materials[2];
                break;

            case 3:
                GetComponent<MeshFilter>().mesh = meshes[2];
                GetComponent<Renderer>().sharedMaterial = materials[3];
                break;

        }
    }
}
