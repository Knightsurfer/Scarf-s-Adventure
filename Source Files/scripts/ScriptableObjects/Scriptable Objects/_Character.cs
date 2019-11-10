using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace ScriptableObjects
{
    //||======================================||
    //||                                                                                     ||
    //||                            Scriptable Character                            ||
    //||                                                                                     ||
    //||                                                                                     ||
    //||======================================||
    //||                                                                                     ||
    //||    Allows you to create the starting state for a character     ||           
    //||    To create a character simply right plick on the project    ||
    //||    window and go to "Create" then "New" then "Character" ||
    //||                                                                                     ||
    //||======================================||


   



    [CreateAssetMenu(fileName = "New Character", menuName = "New/Character")]
    public class _Character : ScriptableObject
    {
        public GameObject prefab;
        public Sprite portrait;
    }

}
