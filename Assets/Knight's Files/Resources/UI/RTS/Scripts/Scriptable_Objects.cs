﻿using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

#region RTS
public class Scriptable_Objects : ScriptableObject
{

}


[CreateAssetMenu(fileName = "NewUnit", menuName = "Unit")]
public class Unit_Info : ScriptableObject
{
    #region Base Stats
    public Sprite unitPortrait;

    public int default_HP;
    public int default_MP;
    public int default_EXP;

    public int startingLevel;

    public bool isProp;
    #endregion
}

#endregion

#region ThirdPerson





#endregion