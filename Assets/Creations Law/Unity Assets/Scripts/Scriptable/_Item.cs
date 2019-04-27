using UnityEngine;

//||=====================================||
//||                                                                                   ||
//||                            Scriptable Item                                 ||
//||                                                                                   ||
//||                                                                                   ||
//||=====================================||
//||                                                                                   ||
//||    Allows you to create the starting state for an item        ||           
//||    To create a item simply right plick on the project         ||
//||    window and go to "Create" then "New" then "Item"       ||
//||                                                                                   ||
//||=====================================||


[CreateAssetMenu(fileName = "New Item", menuName = "New/Item")]
public class _Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public Mesh mesh;
    public Material material;

}