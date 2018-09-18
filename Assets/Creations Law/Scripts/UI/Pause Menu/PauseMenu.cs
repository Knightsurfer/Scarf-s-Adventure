using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : PauseBasic {

    #region Start
    protected void Start()
    {
        BasicStart();
    }
    #endregion
    #region Update
    protected void Update()
    {
        BasicUpdate();
        MenuChoose();
        if (paused)
        {
            if (button_Jump && paused == true)
            {
                CancelMenu();
            }
        }
    }

    protected void MenuChoose()
    {

        if (paused)
        {

            switch (currentMenu)
            {
                case "Main Menu":
                    MainMenu();
                    break;

                case "Items":
                    ItemsMenu();
                    break;


                    case "Equipment":
                    EquipMenu();
                    break;

                    /*

                                    case "Items Menu":
                                        ItemsMenu();
                                        break;

                                    case "Equip Menu":
                                        EquipMenu();
                                        break;

                                    case "Ability Menu":
                                        AbilityMenu();
                                        break;

                                    case "Custom Menu":
                                        CustomMenu();
                                        break;

                                    case "Status Menu":
                                        StatusMenu();
                                        break;

                                    case "Journal Menu":
                                        JournalMenu();
                                        break;

                                    case "Config Menu":
                                        ConfigMenu();
                                        break;


                        */


            }

        }
    }

    #endregion

    //Menus
    void MainMenu()
    {
        UpDownHandler(0, 6);
        if (!mainCanvas.enabled && mainCanvas != null)
        {
            mainCanvas.enabled = !mainCanvas.enabled;
        }
        {
            selectedItem = menuItems[selected].GetComponentInChildren<Text>().text;
            if (button_Attack)
            {

               
                


                SelectedMenu();
                selected = 0;
                HighlightPos();





            }


        }

    }







    void ItemsMenu()
    {
        UpDownHandler(0, 4);
    }
    void EquipMenu()
    {
        UpDownHandler(0, 3);
    }
    void AbilityMenu()
    {
        UpDownHandler(0, 3);
    }
    void CustomMenu()
    {
        UpDownHandler(0, 3);
    }
    void StatusMenu()
    {
        UpDownHandler(0, 3);
    }
    void JournalMenu()
    {
        {
            UpDownHandler(0, 3);
        }
    }
    void ConfigMenu()
    {
        UpDownHandler(0, 3);
    }

}