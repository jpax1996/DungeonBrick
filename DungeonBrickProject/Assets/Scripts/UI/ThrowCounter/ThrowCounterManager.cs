using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowCounterManager : MonoBehaviour
{
    public Text mCounterText;

    private int mMaxThrows;
    private int mThrowCounter;
    private PlayerManager mPlayerManager;

    private void Start()
    {
        EntityStats.OnMaxThrowsUpdated += UpdateMaxThrows;
        GameEvents.current.onThrowStart += DecreaseCounter;
        LevelTransitionController.OnLoadingOutOver += ResetCounter;
    }

    public void Initialize()
    {
        mPlayerManager = GameManager.mInstance.mPlayerManager;
        ResetCounter();
    }

    public void SetMaxThrows(int maxThrows){
        mMaxThrows = maxThrows;
    }

    public void ResetCounter()
    {
        SetMaxThrows(mPlayerManager.mStats.GetTotalAttributeValue(AttributeType.THROWS));
        mThrowCounter = mMaxThrows;
        UpdateThrowCounter();
    }

    public void DecreaseCounter()
    {
        mThrowCounter--;
        UpdateThrowCounter();
    }

    private void UpdateMaxThrows()
    {
        if(mPlayerManager != null)
        {
            SetMaxThrows(mPlayerManager.mStats.GetTotalAttributeValue(AttributeType.THROWS));
            UpdateThrowCounter();
        }
    }

    private void UpdateThrowCounter()
    {
        mCounterText.text = mThrowCounter + "/" + mMaxThrows + " Throws";
    }

    public bool IsOutOfThrows()
    {
        return mThrowCounter == 0? true:false;
    }
}
