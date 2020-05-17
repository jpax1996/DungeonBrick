using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Material mFlashMaterial;
    private Material mDefaultMaterial;
    private SpriteRenderer mSpriteRenderer;
    private HealthBar mHealthBar;
    private Animator mEnemyAnimator;
    private void Start()
    {
        mSpriteRenderer = this.GetComponent<SpriteRenderer>();
        mFlashMaterial = Resources.Load("Flash_Material", typeof(Material)) as Material;
        mDefaultMaterial = mSpriteRenderer.material;
        mHealthBar = this.GetComponent<HealthBar>();
        mEnemyAnimator = this.GetComponent<Animator>();
    }

    public void OnHit(int damage) {
        mSpriteRenderer.material = mFlashMaterial;
        Invoke("ResetMaterial", .1f);

        if (mHealthBar.OnDamaged(damage))
        {
            OnDead();
        }
    }
    
    void OnDead()
    {
        mEnemyAnimator.SetBool("isDead",true);
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    void ResetMaterial()
    {
        mSpriteRenderer.material = mDefaultMaterial;
    }

}
