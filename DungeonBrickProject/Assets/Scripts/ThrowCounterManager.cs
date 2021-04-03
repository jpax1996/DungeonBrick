using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowCounterManager : MonoBehaviour
{
    public Text mCounterText;

    private int mMaxThrows;
    private int mThrowCounter;

    private void Start()
    {
        BallMovement.OnStartThrow += DecreaseCounter;
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
