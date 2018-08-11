using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class RTS_Checks : RTS_Cam
{
    public Unit_Stats unit;






    protected void Check_Update()
    {
        UnitCard();
    }

   

    void UnitCard()
    {
        

        #region commandPanel
        try
        {
            panel = GameObject.Find("Command Panel").GetComponent<RTS_Panel>();

        }
        catch
        {
            Debug.Log("No Command Panel Present");
        }
        #endregion


        if (selectedUnit != null)
        {
            #region RTS Panel
            

            #endregion

            #region Selected Unit
            unit = selectedUnit.GetComponent<Unit_Stats>();
            ChangeStats();

            #endregion

            switch (unit.unitName)
            {

                default:
                    panel.chatpanel.text = unit.unitName;
                    panel.portrait.sprite = unit.portrait;

                    panel.HP_Text.text = "HP " + unit.HP + "/" + unit.maxHP;
                    panel.MP_Text.text = "MP " + unit.MP + "/" + unit.maxMP;

                    panel.HP_Bar.fillAmount = (float)unit.HP / unit.maxHP;
                    panel.MP_Bar.fillAmount = (float)unit.MP / unit.maxMP;

                    panel.EXP_Bar.fillAmount = (float)unit.EXP / unit.maxEXP;
                    break;
            }
            
        }
        else if (selectedUnit == null)
        {
            panel.chatpanel.text = "";
            panel.portrait.sprite = null;

            panel.HP_Text.text = "";
            panel.MP_Text.text = "";

            panel.HP_Bar.fillAmount = 0;
            panel.MP_Bar.fillAmount = 0;

            panel.EXP_Bar.fillAmount = 0;
        }
    }



    protected void OnHealthChanged(int MaxHP, int HP)
    {


    }



    protected void ChangeStats()
    {
        unit.Stats();
    }





}
