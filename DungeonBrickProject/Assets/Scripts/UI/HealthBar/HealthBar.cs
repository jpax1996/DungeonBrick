using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public Slider mHealthSlider;
    private int mMaxHealth;
    private int mCurrentHealth;

    public void SetCurrentHealth(int health)
    {
        mCurrentHealth = health;
    }

    public void SetMaxHealth(int maxHealth){
        mMaxHealth = maxHealth;
    }

    public bool OnDamaged( int damage)
    {
        mCurrentHealth -= damage;
        if (mCurrentHealth < 0)
        {
            mCurrentHealth = 0;
            mHealthSlider.value = 0;
        }
        else
        { 
            mHealthSlider.value = ((float)mCurrentHealth / (float)mMaxHealth) * 100;
        }
        return mCurrentHealth <= 0;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
