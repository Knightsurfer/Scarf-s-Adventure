using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer_Camera : MonoBehaviour
{
    GameObject cam;
    Transform player;
    public Vector3 offset;
    protected float rotateSpeed;


	void Start ()
    {
       cam = GameObject.FindGameObjectWithTag("MainCamera");
       player = GameObject.FindGameObjectWithTag("Player").transform;
       offset = player.position - cam.transform.position;
	}
	

	void Update ()
    {




        cam.transform.position = player.position - offset;
        cam.transform.LookAt(player);
	}
}
