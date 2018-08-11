using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class RTS_Checks : RTS_Cam
{
    public Player_Controller unit;






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

            selected.a = 1;
            panel.portrait.color = selected;

            #region Selected Unit
            unit = selectedUnit.GetComponent<Player_Controller>();
            ChangeStats();

            #endregion

            switch (unit.unitName)
            {

                default:
                    panel.unitName.text = unit.portrait.name;
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
            panel.unitName.text = "";
            panel.portrait.sprite = null;
            selected.a = 0;
            panel.portrait.color = selected;

            panel.HP_Text.text = "";
            panel.MP_Text.text = "";

            panel.HP_Bar.fillAmount = 0;
            panel.MP_Bar.fillAmount = 0;

            panel.EXP_Bar.fillAmount = 0;
        }
    }



    protected void OnHealthChanged(int MaxHP, int HP)
    {
        if (HP <= 0)
        {
            panel.portrait.sprite = null;
            selected.a = 0;
            panel.portrait.color = selected;
            Destroy(selectedUnit);
        }
    }



    protected void ChangeStats()
    {
        OnHealthChanged(unit.maxHP, unit.HP);
        unit.Stats();
       
    }





}
