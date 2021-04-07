using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickerDisplay : MonoBehaviour
{
    public GameObject[] mItemButtons;
    public ItemPickerManager mItemPicker;
    public int mNumberItemsDisplayed;
    public delegate void ItemPicked();
    public static event ItemPicked OnItemPicked;

    private PlayerManager mPlayerManager;
    private InventoryManager mInventory;

    string ITEM_TITLE_ENTITY = "ItemTitle_txt";
    string ITEM_DESCRIPTION_ENTITY = "ItemDescription_txt";
    string ITEM_IMAGE_ENTITY = "ItemSprite_img";

    // Start is called before the first frame update
    void Start()
    {
        ExperienceManager.OnLevelUp += OnOpenDisplay;

        mPlayerManager = GameManager.GetPlayerManager();
        mInventory = GameManager.GetInventoryManager();

        gameObject.SetActive(false);
    }

    public void OnOpenDisplay()
    {
        mPlayerManager.DisableThrow();
        SetItemsDisplayed();
        gameObject.SetActive(true);
    }

    public void OnCloseDisplay()
    {
        mPlayerManager.EnableThrow();
        gameObject.SetActive(false);
        OnItemPicked();
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
        button.GetComponent<Button>().onClick.AddListener(delegate { 
            mInventory.ItemPickup(item, 1);
            OnCloseDisplay();
        });
    }
}
