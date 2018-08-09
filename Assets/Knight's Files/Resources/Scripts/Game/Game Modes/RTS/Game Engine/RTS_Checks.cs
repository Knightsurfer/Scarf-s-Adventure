using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class RTS_Checks : RTS_Cam
{
    public Unit_Stats info;

    public InputField chat;
    public Image icon;






    protected void Check_Update()
    {
        Selected();
       // UnitCard();
    }

    protected void Selected()
    {
        if (selectedUnit != null)
        {
            remainingDistance = (int)selectedUnit.GetComponent<NavMeshAgent>().remainingDistance;
            if (selectedUnit.GetComponent<NavMeshAgent>().remainingDistance == 0)
            {
                selectedUnit.GetComponent<Animator>().SetBool("Walking", false);
            }
        }
    }

    void UnitCard()
    {
        if (selectedUnit != null)
        {
            switch (selectedUnit.GetComponent<Unit_Stats>().unit.unitName)
            {

                default:
                    selected.GetComponent<RTS_Panel>().chatpanel.text = "Knight";
                   
                    break;

                case "Scarf":

                    info = selectedUnit.GetComponent<Unit_Stats>();
                    chat = selected.GetComponent<RTS_Panel>().chatpanel;
                    icon = GameObject.Find("Command Panel").GetComponent<RTS_Panel>().portrait;


                    chat.text = info.unit.unitName;
                    icon.sprite = info.unit.unitPortrait;
                    break;




            }
        }
    }





    




}
