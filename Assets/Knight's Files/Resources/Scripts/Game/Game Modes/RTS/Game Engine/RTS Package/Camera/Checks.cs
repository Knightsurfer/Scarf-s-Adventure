using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class RTS_Checks : RTS_Cam
{
    #region Variables
    protected Command_Panel panel;
    protected Color selected = new Color(1, 1, 1);
    protected Unit_Controller unit;
    protected GameObject selectedUnit;
    protected bool panelWarning = false;
    #endregion


    private void Update()
    {
        Check_Update();
    }
    protected void Check_Update()
    {
        Cam_Update();
        Command_Panel();
    }

    void Command_Panel()
    {
        #region commandPanel
            panel = GameObject.Find("Command Menu").GetComponent<Command_Panel>();
            panel.GetComponent<Canvas>().enabled = true;
            if (selectedUnit != null)
            {
                ChangeStats();
                PanelRemote();
            }
            else if (selectedUnit == null)
            {
                panel.Panel_Defaults();
            }
        #endregion
    }
    protected void PanelRemote()
    {
        selected.a = 1;
        panel.portrait.color = selected;
        unit = selectedUnit.GetComponent<Unit_Controller>();

        panel.unitName.text = unit.unitName;
        panel.portrait.sprite = unit.portrait;

        panel.HP_Text.text = "HP " + unit.HP + "/" + unit.maxHP;
        panel.MP_Text.text = "MP " + unit.MP + "/" + unit.maxMP;

        panel.HP_Bar.fillAmount = (float)unit.HP / unit.maxHP;
        panel.MP_Bar.fillAmount = (float)unit.MP / unit.maxMP;

        panel.EXP_Bar.fillAmount = (float)unit.EXP / unit.maxEXP;
    }




    protected void ChangeStats()
    {
        //OnHealthChanged(unit.maxHP, unit.HP);
    }
    protected void OnHealthChanged(int MaxHP, int HP)
    {

    }







}
