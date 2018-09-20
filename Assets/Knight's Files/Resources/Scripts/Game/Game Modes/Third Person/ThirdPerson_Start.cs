using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPerson_Start : ThirdPerson_Interact {

    GameObject playerModel;

	protected void StartingVariables ()
    {
        playerModel = GameObject.FindGameObjectWithTag("Player");

        switch(SceneManager.GetActiveScene().buildIndex)
        {




            case 4:
                playerModel.transform.localPosition = new Vector3(51, 0, 34);
                break;

            case 5:
                playerModel.transform.localPosition = new Vector3(47, 0, 39);
                break;
        }

    }
	
	
}
