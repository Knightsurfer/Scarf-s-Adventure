
using UnityEngine;

public class Command_Controller : Controller
{
    protected int selected;
    GameObject player;
    protected GameObject[] menuItems = new GameObject[6];

    void AssignMenu()
    {
        ControllerDetect();

        player = GameObject.Find("Characters");
        menuItems[0] = GameObject.Find("Command Item 1");
        menuItems[1] = GameObject.Find("Command Item 2");
        menuItems[2] = GameObject.Find("Command Item 3");
        menuItems[3] = GameObject.Find("Command Item 4");
        menuItems[4] = GameObject.Find("SpikeUp");
        menuItems[5] = GameObject.Find("SpikeDown");
    }




	void Start ()
    {
        AssignMenu();
	}
	

	void Update ()
    {
        
        ControllerCheck();
        Menu();
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
                    menuItems[4].transform.localPosition = new Vector3(210, 133, 0);
                    menuItems[5].transform.localPosition = new Vector3(-180f, 71f, 0);

                    menuItems[0].transform.localPosition = new Vector3(30, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(0, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(0, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(0, -136, 0);

                    
                    break;

                case 1:
                    menuItems[4].transform.localPosition = new Vector3(211, 66, 0);
                    menuItems[5].transform.localPosition = new Vector3(-179f, 1f, 0);

                    menuItems[0].transform.localPosition = new Vector3(0, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(30, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(0, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(0, -136, 0);

                    
                    break;

                case 2:
                    menuItems[4].transform.localPosition = new Vector3(211, -2.7f, 0);
                    menuItems[5].transform.localPosition = new Vector3(-179f, -66.5f, 0);

                    menuItems[0].transform.localPosition = new Vector3(0, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(0, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(30, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(0, -136, 0);
                    break;

                case 3:
                    menuItems[4].transform.localPosition = new Vector3(209, -70.8f, 0);
                    menuItems[5].transform.localPosition = new Vector3(-181, -135f, 0);

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
