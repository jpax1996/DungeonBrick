using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public HealthBar mHealthBar;
    public bool mIsAlive;
    private Material mFlashMaterial;
    private Material mDefaultMaterial;
    private SpriteRenderer mSpriteRenderer;
    private Animator mEnemyAnimator;

    public delegate void EnemyDead();
    public static event EnemyDead OnEnemyDead;
    private void Start()
    {
        mSpriteRenderer = this.GetComponent<SpriteRenderer>();
        mFlashMaterial = Resources.Load("Flash_Material", typeof(Material)) as Material;
        mDefaultMaterial = mSpriteRenderer.material;
        mEnemyAnimator = this.GetComponent<Animator>();
        mIsAlive = true;
    }

    public void OnHit(int damage) {
        mSpriteRenderer.material = mFlashMaterial;
        Invoke("ResetMaterial", .1f);

        if (mHealthBar.OnDamaged(damage))
        {
            OnDead();
            OnEnemyDead();
        }
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

}
