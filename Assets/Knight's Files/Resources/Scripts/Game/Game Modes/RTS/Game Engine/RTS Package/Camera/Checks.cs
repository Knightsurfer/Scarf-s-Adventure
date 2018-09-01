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
    protected void Command_Panel()
    {
        

            panel = FindObjectOfType<Command_Panel>();
            panel.GetComponent<Canvas>().enabled = true;
            if (selectedUnit != null)
            {
                unit = selectedUnit.GetComponent<Unit_Controller>();
                PanelRemote();
            }
            if (selectedUnit == null)
            {
                panel.Panel_Defaults();
            }
    }
    protected void PanelRemote()
    {
        #region Defaults
        if (unit.portrait != null)
        {
            selected.a = 1;
            panel.portrait.color = selected;
            panel.portrait.sprite = unit.portrait;
        }
        #endregion
        #region Setting The Panel
        panel.unitName.text = unit.unitName;

        panel.HP_Text.text = "HP " + unit.HP + "/" + unit.maxHP;
        panel.MP_Text.text = "MP " + unit.MP + "/" + unit.maxMP;

        panel.HP_Bar.fillAmount = (float)unit.HP / unit.maxHP;
        panel.MP_Bar.fillAmount = (float)unit.MP / unit.maxMP;

        panel.EXP_Bar.fillAmount = (float)unit.EXP / unit.maxEXP;
        #endregion
    }
}
