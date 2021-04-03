using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : EntityStats
{
    private PlayerHealthBar mPlayerHealthBar;
    private ExperienceManager mExperienceManager;
    public delegate void KilledEnemy();
    public static event KilledEnemy OnKilledEnemy;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.OnLevelOver += StopMovement;
        LevelManager.OnLevelStart += EnableThrow;
        EnableThrow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetPlayerHealthBar(GameObject HealthBarObj)
    {
        mPlayerHealthBar = HealthBarObj.GetComponent<PlayerHealthBar>();
        mPlayerHealthBar.SetSlider(HealthBarObj.GetComponent<Slider>());
        mPlayerHealthBar.SetMaxHealth(mMaxHealth);
        mPlayerHealthBar.SetCurrentHealth(mMaxHealth);
    }

    public void SetExperienceManager(GameObject ExperienceManagerObj)
    {
        mExperienceManager = ExperienceManagerObj.GetComponent<ExperienceManager>();
    }

    public void OnHitEnemy(GameObject EnemyObj)
    {
        if (EnemyObj.GetComponent<Enemy>().OnHit(this.GetComponent<EntityStats>()))
        {
            mExperienceManager.OnGainedXP(XPGainType.KILL_TYPE);
            OnKilledEnemy();
        }

        int damage = CalculateDamageReceived(EnemyObj.GetComponent<EntityStats>());
        mPlayerHealthBar.OnDamaged(damage);

    }

    public void StopMovement()
    {
        mExperienceManager.ResetCombo();
        this.GetComponent<BallMovement>().StopBallMovement();
        this.GetComponent<BallMovement>().SetEnableThrow(false);
    }
    
    public void ResetThrow()
    {
        mExperienceManager.ResetCombo();
        this.GetComponent<BallMovement>().StopBallMovement();
    }

    public void EnableThrow()
    {
        this.GetComponent<BallMovement>().SetEnableThrow(true);
    }
}
