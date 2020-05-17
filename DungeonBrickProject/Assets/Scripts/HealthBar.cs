using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    public int mInitialHealth = 3;
    private int mCurrentHealth;
    public Transform mHealthBarCenterPosition;

    private Sprite mFirstHealthSegmentFull;
    private Sprite mHealthSegmentFull;
    private Sprite mLastHealthSegmentFull;

    private Sprite mFirstHealthSegmentEmpty;
    private Sprite mHealthSegmentEmpty;
    private Sprite mLastHealthSegmentEmpty;

    private GameObject[] mHealthSegmentObjects;

    private float mSpriteSize = 0.17f;

    // Use this for initialization
    void Start () {
        mCurrentHealth = mInitialHealth;
        mHealthSegmentObjects = new GameObject[mInitialHealth];
        Sprite[] sprites = Resources.LoadAll<Sprite>("HealthBar");
        mFirstHealthSegmentFull = sprites[0];
        mHealthSegmentFull = sprites[1];
        mLastHealthSegmentFull = sprites[2];
        mFirstHealthSegmentEmpty= sprites[3];
        mHealthSegmentEmpty= sprites[4];
        mLastHealthSegmentEmpty= sprites[5];

        InitializeHealthBar();
        
    }

    private void InitializeHealthBar()
    {
        int coef = (int)-(mInitialHealth / 2);
        float xCoord = mHealthBarCenterPosition.localPosition.x + mSpriteSize * coef;
        if (mInitialHealth % 2 == 0)
            xCoord += mSpriteSize / 2;
        for (int i = 0;i < mInitialHealth; i++)
        {
            GameObject NewObj = new GameObject();
            mHealthSegmentObjects[i] = NewObj;
            SpriteRenderer NewSprite = NewObj.AddComponent<SpriteRenderer>();
            NewSprite.sortingLayerName = "UI";
            UpdateHealthSegment(i, true);
            NewObj.transform.SetParent(mHealthBarCenterPosition);
            NewObj.transform.localScale = new Vector3(1, 1, 1);
            NewObj.transform.localPosition = new Vector3(xCoord, 0, 0);
            xCoord += mSpriteSize;
            NewObj.SetActive(true);
        }
    }

    public bool OnDamaged( int damage)
    {
        int newHealth = mCurrentHealth - damage;
        if (newHealth < 0)
            newHealth = 0;
        for(int i = mCurrentHealth-1; i >= newHealth; i--)
        {
            UpdateHealthSegment(i, false);
        }
        mCurrentHealth = newHealth;
        if(mCurrentHealth == 0)
        {
            mHealthSegmentObjects[0].gameObject.transform.parent.gameObject.SetActive(false);
            return true;
        }
        return false;
    }

    private void ResetHealth()
    {
        mCurrentHealth = mInitialHealth;
        for(int i = 0; i<mInitialHealth; i++)
        {
            UpdateHealthSegment(i,true);
        }
    }

    private void UpdateHealthSegment(int index, bool isFull)
    {
        SpriteRenderer NewSprite = mHealthSegmentObjects[index].GetComponent<SpriteRenderer>();
        if (index == 0)
        {
            if (isFull)
            {
                NewSprite.sprite = mFirstHealthSegmentFull;
            }
            else
            {
                NewSprite.sprite = mFirstHealthSegmentEmpty;
            }
        }
        else if (index == mInitialHealth - 1)
        {
            if (isFull)
            {
                NewSprite.sprite = mLastHealthSegmentFull;
            }
            else
            {
                NewSprite.sprite = mLastHealthSegmentEmpty;
            }
        }
        else
        {
            if (isFull)
            {
                NewSprite.sprite = mHealthSegmentFull;
            }
            else
            {
                NewSprite.sprite = mHealthSegmentEmpty;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
