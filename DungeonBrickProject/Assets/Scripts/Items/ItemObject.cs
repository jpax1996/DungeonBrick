using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Assets/Item")]
public class ItemObject : ScriptableObject
{
    public string ItemID;
    public string mItemName;
    [TextArea]
    public string mItemDescription;
    public Sprite mItemSprite;
}
