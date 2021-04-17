using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum XPGainType
{
    HIT_TYPE = 0,
    KILL_TYPE = 1
}

public class ExperienceManager : MonoBehaviour
{
    public int mFirstLevelXPMax;
    public float mXPMaxIncrement;

    public int mHitXP;
    public float mHitXPIncrement;
    public float mKillXP;
    public float mKillXPIncrement;
    public float mXPBarFillSpeed;
    public Slider mExperienceSlider;

    private int mCurrentXP = 0;
    private int mCurrentXPMax;
    private int mCurrentXPCombo = 0;
    private int mCurrentHitCombo = 0;
    private int mCurrentKillCombo = 0;
    private int mTargetXP = 0;
    private bool mIslevelingUp = false;

    private bool mUpdatingXPBar = false;

    public Text mComboText;
    public Text mXPComboText;
    public Text mXPText;

    private void Start()
    {
        GameEvents.current.onXPUpdateStart += ResetCombo;
        GameEvents.current.onLevelUpOver += PerformLevelUp;
    }

    public void Initialize()
    {
        mExperienceSlider.value = 0;
        mCurrentXPMax = mFirstLevelXPMax;
        ResetComboText();
        UpdateXPText();
    }

    public void OnGainedXP(XPGainType xpType)
    {
        int xpGained = 0;
        switch (xpType)
        {
            case XPGainType.HIT_TYPE:
                xpGained += (int)(mHitXP + (mHitXP * (mHitXPIncrement * mCurrentHitCombo)));
                mCurrentHitCombo++;
                break;
            case XPGainType.KILL_TYPE:
                xpGained += (int)(mKillXP + (mKillXP* (mKillXPIncrement* mCurrentKillCombo)));
                mCurrentKillCombo++;
                break;
        }
        mCurrentXPCombo += xpGained;
        UpdateComboText();
    }

    public void ResetCombo()
    {
        StartUpdatingXPBar();
        mCurrentHitCombo = 0;
        mCurrentKillCombo = 0;
        ResetComboText();
    }

    private void ResetComboText()
    {
        mComboText.text = "";
        mXPComboText.text = "";
    }

    private void UpdateComboText()
    {
        string displayText = "";
        if (mCurrentHitCombo != 0)
        {
            displayText += mCurrentHitCombo.ToString() + " HIT";
            if (mCurrentHitCombo > 1)
            {
                displayText += "S";
            }
            if (mCurrentKillCombo != 0)
            {
                displayText += "+";
            }
        }
        if (mCurrentKillCombo != 0)
        {
            displayText += mCurrentKillCombo.ToString() + " KILL";
            if(mCurrentKillCombo > 1)
            {
                displayText += "S";
            }
        }
        mComboText.text = displayText;
        mXPComboText.text = mCurrentXPCombo.ToString();
    }

    private void StartUpdatingXPBar()
    {
        SetXPTarget();
        mUpdatingXPBar = mTargetXP != mCurrentXP ? true : false;
        if (!mUpdatingXPBar)
        {
            GameEvents.current.XpUpdateOver();
        }
    }

    private void UpdateXPBar()
    {
        mCurrentXP = (int)Mathf.MoveTowards ((float)mCurrentXP,(float)mTargetXP, mXPBarFillSpeed);
        if(mCurrentXP != 0)
        {
            mExperienceSlider.value = (float)mCurrentXP / (float)mCurrentXPMax;
        }
        else
        {
            mExperienceSlider.value = 0;
        }

        if(mCurrentXP == mTargetXP) 
        {
            if(mCurrentXP >= mCurrentXPMax && !mIslevelingUp)
            {
                GameEvents.current.LevelUpStart();
                mIslevelingUp = true;
            }
            else
            {
                mUpdatingXPBar = false;
                mCurrentXPCombo = 0;
                GameEvents.current.XpUpdateOver();
            }
        }
    }

    private void UpdateXPText()
    {
        mXPText.text = mCurrentXP + "/" + mCurrentXPMax + " XP";
    }

    private void PerformLevelUp()
    {
        mCurrentXP = 0;
        mCurrentXPMax = mCurrentXPMax + (int)(mCurrentXPMax * mXPMaxIncrement);
        SetXPTarget();
        mExperienceSlider.value = mCurrentXP;
        mUpdatingXPBar = mTargetXP != mCurrentXP ? true : false;
        if (!mUpdatingXPBar)
        {
            GameEvents.current.XpUpdateOver();
        }
        mIslevelingUp = false;
    }

    private void SetXPTarget()
    {
        mTargetXP = mCurrentXP + mCurrentXPCombo;
        if (mTargetXP > mCurrentXPMax)
        {
            mCurrentXPCombo = mTargetXP - mCurrentXPMax;
            mTargetXP = mCurrentXPMax;
        }
        else
        {
            mCurrentXPCombo = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mUpdatingXPBar && !mIslevelingUp)
        {
            UpdateXPBar();
            UpdateXPText();
        }
    }
}
