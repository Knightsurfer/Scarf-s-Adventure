using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : PlayerStats
{
    public string mode;
    public int modeSelector;
    bool thirdPerson;
    bool rts;


    public void Start()
    {

    }

    void Update()
    {
        Test_Functions();
        Stats_Update();
        Shortcuts();
    }

    void Shortcuts()
    {
        if (Input.GetKey(KeyCode.F3))
        {
            Recovery(0, "Heal", 1);

        }

        if (Input.GetKey(KeyCode.F4))
        {
            Recovery(0, "Damage", 1);
        }
    }

    void Test_Functions()
    {
        switch (mode)
        {

            case "ThirdPerson":
                if (!thirdPerson)
                {
                    thirdPerson = true;
                    rts = false;

                    foreach (ThirdPerson tp in FindObjectsOfType<ThirdPerson>())
                    {
                        tp.enabled = true;
                        tp.SendMessage("Start");
                    }

                    FindObjectOfType<RTS_Controls>().enabled = false;

                    GameObject.Find("KH UI").GetComponent<Canvas>().enabled = true;
                }
                break;

            case "RTS":
                if (!rts)
                {
                    thirdPerson = false;
                    rts = true;

                    foreach (ThirdPerson tp in player)
                    {
                        tp.enabled = false;
                    }
                    FindObjectOfType<RTS_Controls>().enabled = true;
                    FindObjectOfType<RTS_Controls>().SendMessage("Start");
                    GameObject.Find("KH UI").GetComponent<Canvas>().enabled = false;
                }
                break;

        }
    }








}
