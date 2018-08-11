using UnityEngine;
using UnityEngine.UI;

public class RTS_Panel : MonoBehaviour
{
    public Image portrait;
    public InputField chatpanel;

    public Text HP_Text;
    public Image HP_Bar;

    public Text MP_Text;
    public Image MP_Bar;

    public Image EXP_Bar;







    protected Color selected;
    protected float alpha;

    void Start()
    {
        selected = new Color(1, 1, 1);
        portrait = GameObject.Find("Image").GetComponent<Image>();
        chatpanel = GameObject.Find("InputField").GetComponent<InputField>();

        HP_Text = GameObject.Find("HP Text").GetComponent<Text>();
        HP_Bar = GameObject.Find("HP Bar").GetComponent<Image>();

        MP_Text = GameObject.Find("MP Text").GetComponent<Text>();
        MP_Bar = GameObject.Find("MP Bar").GetComponent<Image>();

        EXP_Bar = GameObject.Find("EXP Bar").GetComponent<Image>();

    }

    void Update()
    {
        
        if (portrait.sprite == null)
        {
            selected.a = 0;
            portrait.color = selected;
        }
        else
        {
            selected.a = 1;
            portrait.color = selected;
        }
    }






}
