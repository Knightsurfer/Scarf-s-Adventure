using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Unit")]
public class Unit_Info : ScriptableObject
{
    public Sprite unitPortrait;

    public int default_HP;
    public int default_MP;
    public int default_EXP;

    public int startingLevel;


    public bool isProp;
    public bool canMove;
    public bool canAttack;



}
