using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Assets/Inventory")]
public class InventoryManager : ScriptableObject
{
    public List<InventorySlot> mStartingItemList;
    public List<InventorySlot> mItemList;

    private PlayerManager mPlayerManager;
    private DisplayInventory mDisplayInventory;

    public void Initialize(PlayerManager _PlayerManager)
    {
        mPlayerManager = _PlayerManager;
        mDisplayInventory = GameManager.mInstance.mDisplayInventory;
        foreach (InventorySlot slot in mStartingItemList)
        {
            InventorySlot newSlot = new InventorySlot(mPlayerManager.mStats,slot.mItem,slot.mAmount);
            mItemList.Add(newSlot);
        }
    }

    public void ResetInventory()
    {
        foreach(InventorySlot slot in mItemList)
        {
            slot.ItemRemove(slot.mAmount);
        }
        mItemList.Clear();
        mDisplayInventory.ResetDisplay();
    }

    public void ItemPickup(Item itemPickedUp, int amount)
    {
        for (int i = 0; i< mItemList.Count; i++)
        {
            if(itemPickedUp.mItemID == mItemList[i].mItem.mItemID)
            {
                mItemList[i].AddAmount(amount);
                mDisplayInventory.UpdateDisplay();
                return;
            }
        }
        mItemList.Add(new InventorySlot(mPlayerManager.mStats,itemPickedUp, amount));
        mDisplayInventory.UpdateDisplay();
    }
}

[System.Serializable]
public class InventorySlot
{
    public GameObject mItemObject;
    public Item mItem;
    public int mAmount;
    public InventorySlot(EntityStats _entityStats, Item _item, int _amount)
    {
        mItemObject = _item.gameObject;
        mItem = _item;
        ItemPickedUp(_entityStats,_amount);
    }
    public void AddAmount(int _amount)
    {
        mItem.AddStack(_amount);
        mAmount += _amount;
    }
    public void ItemPickedUp(EntityStats _entityStats, int _amount)
    {
        mItem.OnPickup(_entityStats, _amount);
        mAmount += _amount;
    }
    public void ItemRemove(int _amount)
    {
        mItem.RemoveItem(_amount);
        mAmount -= _amount;
    }
}
