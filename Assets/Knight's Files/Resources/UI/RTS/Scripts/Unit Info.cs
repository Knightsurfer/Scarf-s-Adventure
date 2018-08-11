using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Unit")]
public class UnitInfo : ScriptableObject
{
    public string unitName = "Test";
    public Sprite unitPortrait = null;

    public int  default_HP = 0;
    public int default_MP = 0;
    public bool selected = false;
}
