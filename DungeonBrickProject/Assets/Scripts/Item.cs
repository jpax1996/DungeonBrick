using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public string mItemID;
    public string mItemName;
    [TextArea]
    public string mItemDescription;
    public Sprite mItemSprite;

    private int itemStack;
    public EntityStats entityStats;
    protected int ItemStack { get { return itemStack; } set { itemStack = value; } }
    public Dictionary<AttributeType, Attribute> ItemBuffs = new Dictionary<AttributeType, Attribute>();

    virtual public void OnPickup(EntityStats _entityStats, int _amount) { itemStack += _amount; entityStats = _entityStats; }
    virtual public Dictionary<AttributeType, Attribute> GetItemBuffs() { return ItemBuffs; }
    virtual public void SetStack(int _amount) { itemStack = _amount; }
    virtual public void AddStack(int _amount) { itemStack += _amount; }
    virtual public void RemoveItem(int _amount) { itemStack -= _amount; }
}
