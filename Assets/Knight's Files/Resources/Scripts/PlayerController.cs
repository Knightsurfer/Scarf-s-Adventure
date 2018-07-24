using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Camera cam;
    public LayerMask movementLayer;
    Rigidbody rigid;


	void Start ()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() ;
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ControllerInput();
    }




    void ControllerInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit,100,movementLayer))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);


            }

        }

    }

    






}
