using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickerManager : MonoBehaviour
{
    public ItemDatabase mItems;
    private List<GameObject> mItemRotation;
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
        foreach(GameObject gameObjectItem in instance.mItems.mAllItems)
        {
            Item item = gameObjectItem.GetComponent<Item>();
            if (item.mItemID == ID)
            {
                return item;
            }
        }
        return null;
    }

    public static Item GetRandomItem()
    {
        GameObject gameObjectItem = instance.mItems.mAllItems[Random.Range(0, instance.mItems.mAllItems.Count)];
        return gameObjectItem.GetComponent<Item>();
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
            randomItems[i] = mItemRotation[cpt].GetComponent<Item>();
            cpt++;
        }
        return randomItems;
    }


    private void ShuffleItemRotation()
    {
        for (int i = 0; i < mItemRotation.Count; i++)
        {
            GameObject temp = mItemRotation[i];
            int randomIndex = Random.Range(i, mItemRotation.Count);
            mItemRotation[i] = mItemRotation[randomIndex];
            mItemRotation[randomIndex] = temp;
        }
    }

}
