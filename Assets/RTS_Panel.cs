using UnityEngine;
using UnityEngine.UI;

public class RTS_Panel : MonoBehaviour
{
    public Image portrait;
    public InputField chatpanel;

    protected Color selected;
    protected float alpha;

    void Start()
    {
        selected = new Color(1, 1, 1);
        portrait = GameObject.Find("Unit").GetComponent<Image>();
       
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
