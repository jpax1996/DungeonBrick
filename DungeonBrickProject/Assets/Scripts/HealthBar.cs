using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public Slider mHealthSlider;
    private float mCurrHealth;

    public bool OnDamaged( int damage)
    {
        float currHealth = mHealthSlider.value;
        currHealth = currHealth - damage;
        if (currHealth < 0)
        {
            currHealth = 0;
        }
        mHealthSlider.value = currHealth;
        return currHealth <= 0;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
