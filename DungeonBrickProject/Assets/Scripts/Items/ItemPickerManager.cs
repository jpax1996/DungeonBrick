using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickerManager : MonoBehaviour
{
    public ItemDatabase mItems;
    private List<Item> mItemRotation;
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
        foreach(Item item in instance.mItems.mAllItems)
        {
            if(item.ItemID == ID)
            {
                return item;
            }
        }
        return null;
    }

    public static Item GetRandomItem()
    {
        return instance.mItems.mAllItems[Random.Range(0, instance.mItems.mAllItems.Count)];
    }

    public Item[] GetRandomItems(int numberItems)
    {
        mItemRotation = mItems.mAllItems;
        ShuffleItemRotation();
        Item[] randomItems = new Item[numberItems];
        int cpt = 0;
        for (int i = 0; i<numberItems; i++)
        {
            if (cpt >= mItemRotation.Count)
            {
                cpt = 0;
            }
            randomItems[i] = mItemRotation[cpt];
            cpt++;
        }
        return randomItems;
    }


    private void ShuffleItemRotation()
    {
        for (int i = 0; i < mItemRotation.Count; i++)
        {
            Item temp = mItemRotation[i];
            int randomIndex = Random.Range(i, mItemRotation.Count);
            mItemRotation[i] = mItemRotation[randomIndex];
            mItemRotation[randomIndex] = temp;
        }
    }

}
