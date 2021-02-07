using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour {

    public Slider mHealthSlider;
    public Text mHealthText;
    private int mCurrHealth;
    private int mMaxHealth;

    public void SetSlider(Slider newSlider)
    {
        mHealthSlider = newSlider;
    }

    public void SetCurrentHealth(int currHealth)
    {
        mCurrHealth = currHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        mMaxHealth = maxHealth;
    }
    public bool OnDamaged( int damage)
    {
        mCurrHealth -= damage;
        if (mCurrHealth < 0)
        {
            mCurrHealth = 0;
            mHealthSlider.value = 0;
        }
        else
        {
            mHealthSlider.value = ((float) mCurrHealth/ (float) mMaxHealth) * 100;
        }
        UpdateHealthText();
        return mCurrHealth <= 0;
    }

    private void UpdateHealthText()
    {
        mHealthText.GetComponent<Text>().text = "HP: " + mCurrHealth + "/" + mMaxHealth;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
