using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Assets/Databases/Item Databases")]

public class ItemDatabase : ScriptableObject
{
    public List<Item> mAllItems;
    private InventoryManager mInventoryManager;

    public void Initialize(InventoryManager inventoryManager)
    {
        mInventoryManager = inventoryManager;
    }
}
