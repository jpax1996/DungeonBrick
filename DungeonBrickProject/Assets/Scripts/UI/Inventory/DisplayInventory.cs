using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public GameObject mSlotPrefab;

    private PlayerManager mPlayerManager;
    private InventoryManager mInventory;
    private LevelManager mLevelManager;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMN;
    Dictionary<InventorySlot, GameObject> mItemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        mPlayerManager = GameManager.mInstance.mPlayerManager;
        mInventory = GameManager.mInstance.mInventoryManager;
        mLevelManager = GameManager.mInstance.mLevelManager;
    }
    public void Initialize()
    {
        gameObject.SetActive(false);
        UpdateDisplay();
    }

    public void OnClosePressed()
    {
        if (!mLevelManager.mIsUpdatingUi)
        {
            mPlayerManager.EnableThrow();
        }
        gameObject.SetActive(false);
    }

    public void OnOpenPressed()
    {
        if (!mPlayerManager.IsThrowRunning())
        {
            mPlayerManager.DisableThrow();
            gameObject.SetActive(true);
        }
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < mInventory.mItemList.Count; i++)
        {
            InventorySlot slot = mInventory.mItemList[i];
            GameObject obj = null;
            mItemsDisplayed.TryGetValue(slot,out obj);
            if (obj==null)
            {
                obj = Instantiate(mSlotPrefab,Vector3.zero,Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.mAmount.ToString("n0");
                Image itemImg = obj.transform.Find("ItemSprite_img").GetComponent<Image>();
                itemImg.sprite = slot.mItem.mItemSprite;
                mItemsDisplayed.Add(slot, obj);
            }
            else
            {
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.mAmount.ToString("n0");
            }
        }
    }
    public void ResetDisplay()
    {
        foreach (KeyValuePair<InventorySlot, GameObject> entry in mItemsDisplayed)
        {
            Destroy(entry.Value);
        }
        mItemsDisplayed.Clear();
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMN)),Y_START + ((-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMN))), 0f);
    }
}
