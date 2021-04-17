using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public EntityStats mStats;
    public BallMovement mBallMovement;
    private Transform mSpawnTransform;
    private ExperienceManager mExperienceManager;
    private InventoryManager mInventoryManager;

    public delegate void KilledEnemy();
    public static event KilledEnemy OnKilledEnemy;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onLevelStart += EnableThrow;
        GameEvents.current.onGameRestart += ResetState;
        mBallMovement = this.GetComponent<BallMovement>();
    }

    public void Initialize()
    {
        mExperienceManager = GameManager.mInstance.mExperienceManager;
        mInventoryManager = GameManager.mInstance.mInventoryManager;
        mStats.Initialize();
    }

    private void ResetState()
    {
        mInventoryManager.ResetInventory();
        //mStats.ResetStats();
    }

    public void SetSpawnTransform(Transform spawnTransform)
    {
        mSpawnTransform = spawnTransform;
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

    public void ResetPosition()
    {
        this.transform.position = mSpawnTransform.position;
        this.transform.rotation= mSpawnTransform.rotation;
    }
    
    private void OnApplicationQuit()
    {
        mInventoryManager.ResetInventory();
    }
}
