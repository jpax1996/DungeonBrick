using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickerDisplay : MonoBehaviour
{
    public GameObject[] mItemButtons;
    public ItemPickerManager mItemPicker;
    public int mNumberItemsDisplayed;

    private InventoryManager mInventory;

    string ITEM_TITLE_ENTITY = "ItemTitle_txt";
    string ITEM_DESCRIPTION_ENTITY = "ItemDescription_txt";
    string ITEM_IMAGE_ENTITY = "ItemSprite_img";

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onLevelUpStart += OnOpenDisplay;
        mInventory = GameManager.mInstance.mInventoryManager;
        gameObject.SetActive(false);
    }

    public void OnOpenDisplay()
    {
        SetItemsDisplayed();
        gameObject.SetActive(true);
    }

    public void OnCloseDisplay()
    {
        gameObject.SetActive(false);
        GameEvents.current.LevelUpOver();
    }

    private void SetItemsDisplayed()
    {
        Item[] mItemsDisplayed = new Item[mNumberItemsDisplayed];
        mItemsDisplayed = mItemPicker.GetRandomItems(mNumberItemsDisplayed);
        for (int i = 0; i < mNumberItemsDisplayed; i++)
        {
            GameObject currButton = mItemButtons[i];
            Item currItem = mItemsDisplayed[i];
            UpdateButton(currButton, currItem);
        }
    }

    private void UpdateButton(GameObject button, Item item)
    {
        Text buttonTitle = button.transform.Find(ITEM_TITLE_ENTITY).GetComponent<Text>();
        Text buttonDescription = button.transform.Find(ITEM_DESCRIPTION_ENTITY).GetComponent<Text>();
        Image buttonImage = button.transform.Find(ITEM_IMAGE_ENTITY).GetComponent<Image>();
        buttonTitle.text = item.mItemName;
        buttonDescription.text = item.mItemDescription;
        buttonImage.sprite = item.mItemSprite;
        Button itemButton = button.GetComponent<Button>();
        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(delegate { 
            mInventory.ItemPickup(item, 1);
            OnCloseDisplay();
        });
    }
}
