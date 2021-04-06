using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Assets/Item")]
public class Item : ScriptableObject
{
    public string ItemID;
    public string mItemName;
    [TextArea]
    public string mItemDescription;
    public Sprite mItemSprite;
    private int mStack = 0;

    public int GetItemStack() { return mStack; }

    public virtual void OnPickup() 
    {
        mStack++;
    }

    public virtual void OnStacked()
    {
        mStack++;
    }
    public virtual void OnHit() { }

}
