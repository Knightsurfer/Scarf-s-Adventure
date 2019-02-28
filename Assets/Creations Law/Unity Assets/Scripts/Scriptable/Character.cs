using UnityEngine;

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
public class Character : ScriptableObject
{
    public Sprite portrait;
    public Mesh mesh;
    public Material material;
}