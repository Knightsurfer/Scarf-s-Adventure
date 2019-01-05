using UnityEngine;
using UnityEngine.UI;

public class Command_Panel : MonoBehaviour
{
    #region Variables
    public Text unitName;
    public Image portrait;

    public Text HP_Text;
    public Image HP_Bar;

    public Text MP_Text;
    public Image MP_Bar;

    public Image EXP_Bar;
    #endregion

    void Start()
    {
        portrait = GameObject.Find("Unit Portrait").GetComponent<Image>();
        unitName = GameObject.Find("Unit Name").GetComponent<Text>();

        HP_Text = GameObject.Find("HP Text").GetComponent<Text>();
        HP_Bar = GameObject.Find("HP Bar").GetComponent<Image>();

        MP_Text = GameObject.Find("MP Text").GetComponent<Text>();
        MP_Bar = GameObject.Find("MP Bar").GetComponent<Image>();

        EXP_Bar = GameObject.Find("EXP Bar").GetComponent<Image>();

        portrait.type = Image.Type.Simple;

        Panel_Defaults();
    }
    public void Panel_Defaults()
    {
        unitName.text = "";
        portrait.sprite = null;
        portrait.color = new Color(1, 1, 1, 0);

        HP_Text.text = "";
        MP_Text.text = "";

        HP_Bar.fillAmount = 0;
        MP_Bar.fillAmount = 0;

        EXP_Bar.fillAmount = 0;
    }





}
