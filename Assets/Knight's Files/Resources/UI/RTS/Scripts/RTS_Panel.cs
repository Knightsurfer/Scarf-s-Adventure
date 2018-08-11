using UnityEngine;
using UnityEngine.UI;

public class RTS_Panel : MonoBehaviour
{
    public Image portrait;
    public Text unitName;

    public Text HP_Text;
    public Image HP_Bar;

    public Text MP_Text;
    public Image MP_Bar;

    public Image EXP_Bar;

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

    }
}
