﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    public EntityStats mStats;
    public HealthBar mHealthBar;
    private bool mIsAlive;
    private Material mFlashMaterial;
    private Material mDefaultMaterial;
    private SpriteRenderer mSpriteRenderer;
    private Animator mEnemyAnimator;

    public delegate void EnemyDead();
    public static event EnemyDead OnEnemyDead;

    const string FLASH_MATERIAL_NAME = "Flash_Material";

    private void Start()
    {
        mSpriteRenderer = this.GetComponent<SpriteRenderer>();
        mFlashMaterial = Resources.Load(FLASH_MATERIAL_NAME, typeof(Material)) as Material;
        mDefaultMaterial = mSpriteRenderer.material;
        mEnemyAnimator = this.GetComponent<Animator>();
        mStats.Initialize();
        mHealthBar.SetMaxHealth(mStats.GetTotalAttributeValue(AttributeType.HEALTH));
        mHealthBar.SetCurrentHealth(mStats.GetTotalAttributeValue(AttributeType.HEALTH));
        mIsAlive = true;
    }

    public bool OnHit(EntityStats PlayerStats) {
        int damage = mStats.CalculateDamageReceived(PlayerStats);
        mSpriteRenderer.material = mFlashMaterial;
        Invoke("ResetMaterial", .1f);

        if (mHealthBar.OnDamaged(damage))
        {
            OnDead();
            OnEnemyDead();
            return true;
        }
        return false;
    }
    
    public void OnDead()
    {
        mIsAlive = false;
        mEnemyAnimator.SetBool("isDead",true);
        this.GetComponent<BoxCollider2D>().enabled = false;
        mHealthBar.gameObject.SetActive(false);
    }

    void ResetMaterial()
    {
        mSpriteRenderer.material = mDefaultMaterial;
    }

    public bool IsAlive()
    {
        return mIsAlive;
    }
}
