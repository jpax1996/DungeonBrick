using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickerManager : MonoBehaviour
{
    public ItemDatabase items;

    private static ItemPickerManager instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Item GetItemByID(string ID)
    {
        foreach(Item item in instance.items.mAllItems)
        {
            if(item.ItemID == ID)
            {
                return item;
            }
        }
        return null;
    }

    public static Item GetRandomItem(string ID)
    {
        return instance.items.mAllItems[Random.Range(0, instance.items.mAllItems.Count)];
    }
}
