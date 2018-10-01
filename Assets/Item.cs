using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : Scriptable_Objects
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public Mesh mesh;
    public Material material;







}
