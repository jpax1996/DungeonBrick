using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellTimer : MonoBehaviour {

    private Image mSpellTimeImage;
    private float mSpeed = 0.005f;
    private float mCurrentAmount;
    public delegate void SpellTimeOver();
    public static event SpellTimeOver OnSpellTimeOver;

    private void Start()
    {
        GameEvents.current.onThrowStart += SetUpSpellTimer;
        mSpellTimeImage = this.transform.GetChild(0).GetComponent<Image>();
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
        if (this.gameObject.activeSelf)
        {
            if (mCurrentAmount > 0)
            {
                mCurrentAmount -= mSpeed;
            }
            else
            {
                TimerOver();
            }
            mSpellTimeImage.fillAmount = mCurrentAmount;
        }
	}

    void TimerOver()
    {
        this.gameObject.SetActive(false);
        OnSpellTimeOver();
    }

    void SetUpSpellTimer()
    {
        this.gameObject.SetActive(true);
        mCurrentAmount = 1;
        mSpellTimeImage.fillAmount = 1;
    }
}
