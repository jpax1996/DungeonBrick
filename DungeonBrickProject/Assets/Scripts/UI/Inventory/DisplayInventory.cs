using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryManager mInventory;
    public GameObject mSlotPrefab;

    private PlayerManager mPlayerManager;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMN;
    Dictionary<InventorySlot, GameObject> mItemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        mPlayerManager = GameManager.GetPlayerManager();
        gameObject.SetActive(false);
        UpdateDisplay();
    }

    public void OnClosePressed()
    {
        mPlayerManager.EnableThrow();
        gameObject.SetActive(false);
    }

    public void OnOpenPressed()
    {
        if (!mPlayerManager.IsThrowRunning())
        {
            mPlayerManager.DisableThrow();
            UpdateDisplay();
            gameObject.SetActive(true);
        }
    }

    private void UpdateDisplay()
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
                mItemsDisplayed.Add(slot, obj);
            }
            else
            {
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.mAmount.ToString("n0");
            }
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMN)),Y_START + ((-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMN))), 0f);
    }
}
