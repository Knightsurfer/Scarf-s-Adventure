using System.Collections.Generic;
using UnityEngine;
public class PlayerInventory : MonoBehaviour
{
    #region singleton
    public static PlayerInventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    readonly int space = 2;

    public List<Item> items = new List<Item>();

    
    public bool Add(Item item)
    {
        
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                print("HOARDER");
                return false;
            }
            items.Add(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }


}