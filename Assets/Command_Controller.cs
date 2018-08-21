
using UnityEngine;

public class Command_Controller : Controller
{
    protected int selected;
    GameObject player;
    protected GameObject[] menuItems = new GameObject[4];

    void AssignMenu()
    {
        player = GameObject.Find("Characters");
        menuItems[0] = GameObject.Find("Command Item 1");
        menuItems[1] = GameObject.Find("Command Item 2");
        menuItems[2] = GameObject.Find("Command Item 3");
        menuItems[3] = GameObject.Find("Command Item 4");
    }




	void Start ()
    {
        AssignMenu();
	}
	

	void Update ()
    {
        ControlerTypeCheck();
        ControllerCheck();
        Menu();
	}


    void ControlerTypeCheck()
    {
        psController = player.GetComponentInChildren<ThirdPersonController>().psController;
        xboxController = player.GetComponentInChildren<ThirdPersonController>().xboxController;
    }


    protected void Menu()
    {

        if (!GameObject.Find("Pause Menu").GetComponent<Canvas>().enabled)
        {
            UpDownHandler();

            switch (selected)
            {

                case -1:
                    selected = 3;
                    break;

                case 0:
                    menuItems[0].transform.localPosition = new Vector3(30, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(0, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(0, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(0, -136, 0);
                    break;

                case 1:
                    menuItems[0].transform.localPosition = new Vector3(0, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(30, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(0, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(0, -136, 0);
                    break;

                case 2:
                    menuItems[0].transform.localPosition = new Vector3(0, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(0, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(30, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(0, -136, 0);
                    break;

                case 3:
                    menuItems[0].transform.localPosition = new Vector3(0, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(0, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(0, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(30, -136, 0);
                    break;

                case 4:
                    selected = 0;
                    break;
            }
        }
       
    }








    void UpDownHandler()
    {
        if (direction == "up")
        {
            if (d_Up == false)
            {
                d_Up = true;

                selected--;
            }
        }

        if (direction == "down")
        {
            if (d_Down == false)
            {
                d_Down = true;

                selected++;
            }
        }
    }






}
