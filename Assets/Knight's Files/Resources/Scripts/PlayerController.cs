using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float leftx;
    float lefty;
    Rigidbody rigid;

	// Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ControllerInput();
    }




    void ControllerInput()
    {
        ControllerConvert();

        rigid.AddForce(new Vector3(leftx,0,lefty));
    }

    void ControllerConvert()
    {
        leftx = Input.GetAxis("LeftStickX")  *25;
        lefty = Input.GetAxis("LeftStickY")  *25;
    }






}
