using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : EntityStats
{
    public BallMovement mBallMovement;
    private Transform mSpawnTransform;
    private ExperienceManager mExperienceManager;
    private InventoryManager mInventoryManager;

    public delegate void KilledEnemy();
    public static event KilledEnemy OnKilledEnemy;

    private void Awake()
    {
        Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.OnLevelOver += StopMovement;
        LevelManager.OnLevelStart += EnableThrow;
        EnableThrow();
        Initialize();
    }
    
    public void SetSpawnTransform(Transform spawnTransform)
    {
        mSpawnTransform = spawnTransform;
    }

    public void Initialize ()
    {
        mBallMovement = this.GetComponent<BallMovement>();
        mExperienceManager = GameManager.GetExperienceManager();
        mInventoryManager = GameManager.GetInventoryManager(); 
    }

    public void OnHitEnemy(GameObject EnemyObj)
    {
        if (EnemyObj.GetComponent<Enemy>().OnHit(this.GetComponent<EntityStats>()))
        {
            mExperienceManager.OnGainedXP(XPGainType.KILL_TYPE);
            OnKilledEnemy();
        }

    }

    public void StopMovement()
    {
        mBallMovement.StopBallMovement();
        DisableThrow();
    }

    public void EnableThrow()
    {
        mBallMovement.SetEnableThrow(true);
    }
    
    public void DisableThrow()
    {
        mBallMovement.SetEnableThrow(false);
    }

    public bool IsThrowRunning()
    {
        return mBallMovement.IsBallRolling();
    }

    public void ResetState()
    {
        this.transform.position = mSpawnTransform.position;
        this.transform.rotation= mSpawnTransform.rotation;
    }
    /*
    private void OnApplicationQuit()
    {
        mInventoryManager.mItemList.Clear();
    }*/
}
