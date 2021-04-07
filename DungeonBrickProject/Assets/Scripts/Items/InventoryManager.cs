using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Assets/Inventory")]
public class InventoryManager : ScriptableObject
{
    public List<InventorySlot> mItemList;

    public void ItemPickup(Item itemPickedUp, int amount)
    {
        for (int i = 0; i< mItemList.Count; i++)
        {
            if(itemPickedUp == mItemList[i].mItemObject)
            {
                mItemList[i].AddAmount(amount);
                return;
            }
        }
        itemPickedUp.OnPickup();
        mItemList.Add(new InventorySlot(itemPickedUp, amount));
    }
}

[System.Serializable]
public class InventorySlot
{
    public Item mItemObject;
    public int mAmount;
    public InventorySlot(Item _item, int _amount)
    {
        mItemObject = _item;
        mAmount = _amount;
    }
    public void AddAmount(int _amount)
    {
        mAmount += _amount;
        mItemObject.OnStacked();
    }
}
