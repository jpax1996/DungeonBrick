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
        BallMovement.OnStartThrow += DecreaseCounter;
        LevelTransitionController.OnLoadingOutOver += ResetCounter;
        Initialize();
    }

    private void Initialize()
    {
        mPlayerManager = GameManager.GetPlayerManager();
        SetMaxThrows(mPlayerManager.mMaxThrows);
        ResetCounter();
    }

    public void SetMaxThrows(int maxThrows){
        mMaxThrows = maxThrows;
    }

    public void ResetCounter()
    {
        mThrowCounter = mMaxThrows;
        UpdateThrowCounter();
    }

    public void DecreaseCounter()
    {
        mThrowCounter--;
        UpdateThrowCounter();
    }

    private void UpdateThrowCounter()
    {
        mCounterText.text = mThrowCounter + "/" + mMaxThrows + " Throws";
    }
}
