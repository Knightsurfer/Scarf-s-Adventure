using UnityEngine;

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





            }
        }
    }

    #endregion

    void MainMenu()
    {
        if (!mainCanvas.enabled)
        {
            mainCanvas.enabled = true;
        }
        
            UpDownHandler(0,6);
        switch (selected)
        {

            case 0:
                if (button_Attack)
                {
                    mainCanvas.enabled = false;
                    selected = 0;
                    currentMenu = "Items Menu";
                }
                break;



            case 1:
                if (button_Attack)
                {
                    mainCanvas.enabled = false;
                    selected = 0;
                    currentMenu = "Equip Menu";
                }
                break;


            case 2:
                if (button_Attack)
                {
                    mainCanvas.enabled = false;
                    selected = 0;
                    currentMenu = "Ability Menu";
                }
                break;



            case 3:
                if (button_Attack)
                {
                    mainCanvas.enabled = false;
                    selected = 0;
                    currentMenu = "Custom Menu";
                }
                break;



            case 4:
                if (button_Attack)
                {
                    mainCanvas.enabled = false;
                    selected = 0;
                    currentMenu = "Status Menu";
                }
                break;

            case 5:
                if (button_Attack)
                {
                    mainCanvas.enabled = false;
                    selected = 0;
                    currentMenu = "Journal Menu";
                }
                break;


            case 6:
                if (button_Attack)
                {
                    mainCanvas.enabled = false;
                    selected = 0;
                    currentMenu = "Config Menu";
                }
                break;

        }
            if (button_Jump && paused == true)
            {
                pausePanel.enabled = false;
                paused = pausePanel.enabled;
                GamePause();
            }
        
    }

    void ItemsMenu()
    {
        if (!itemCanvas.enabled)
        {
            itemCanvas.enabled = true;
        }
        UpDownHandler(0,4);

        switch (selected)
        {
            case 0:

                break;

            case 1:

                break;

            case 2:

                break;

            case 3:

                break;

            case 4:

                break;

        }
        if (button_Jump && paused == true)
        {
            selected = 0;
            itemCanvas.enabled = false;
            currentMenu = "Main Menu";
        }        
    }
    void EquipMenu()
    {
        if (!equipCanvas.enabled)
        {
            equipCanvas.enabled = true;
        }
            UpDownHandler(0,3);
            switch (selected)
            {
                case 0:
                   
                    break;

                case 1:

                    break;

                case 2:

                    break;

                case 3:

                    break;
            }

        if (button_Jump && paused == true)
        {
            selected = 0;
            equipCanvas.enabled = false;
            currentMenu = "Main Menu";
        }
    }
    void AbilityMenu()
    {
        if (!equipCanvas.enabled)
        {
            equipCanvas.enabled = true;
        }
        UpDownHandler(0, 3);
        switch (selected)
        {
            case 0:

                break;

            case 1:

                break;

            case 2:

                break;

            case 3:

                break;
        }

        if (button_Jump && paused == true)
        {
            selected = 0;
            equipCanvas.enabled = false;
            currentMenu = "Main Menu";
        }
    }
    void CustomMenu()
    {
        if (!equipCanvas.enabled)
        {
            equipCanvas.enabled = true;
        }
        UpDownHandler(0, 3);
        switch (selected)
        {
            case 0:

                break;

            case 1:

                break;

            case 2:

                break;

            case 3:

                break;
        }

        if (button_Jump && paused == true)
        {
            selected = 0;
            equipCanvas.enabled = false;
            currentMenu = "Main Menu";
        }
    }
    void StatusMenu()
    {
        if (!equipCanvas.enabled)
        {
            equipCanvas.enabled = true;
        }
        UpDownHandler(0, 3);
        switch (selected)
        {
            case 0:

                break;

            case 1:

                break;

            case 2:

                break;

            case 3:

                break;
        }

        if (button_Jump && paused == true)
        {
            selected = 0;
            equipCanvas.enabled = false;
            currentMenu = "Main Menu";
        }
    }
    void JournalMenu()
    {
        if (!equipCanvas.enabled)
        {
            equipCanvas.enabled = true;
        }
        UpDownHandler(0, 3);
        switch (selected)
        {
            case 0:

                break;

            case 1:

                break;

            case 2:

                break;

            case 3:

                break;
        }

        if (button_Jump && paused == true)
        {
            selected = 0;
            equipCanvas.enabled = false;
            currentMenu = "Main Menu";
        }
    }
    void ConfigMenu()
    {
        if (!equipCanvas.enabled)
        {
            equipCanvas.enabled = true;
        }
        UpDownHandler(0, 3);
        switch (selected)
        {
            case 0:

                break;

            case 1:

                break;

            case 2:

                break;

            case 3:

                break;
        }

        if (button_Jump && paused == true)
        {
            selected = 0;
            equipCanvas.enabled = false;
            currentMenu = "Main Menu";
        }
    }

}
