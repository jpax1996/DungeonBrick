using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject[] mEpisode1Levels;
    public Transform mLevelStartingTransform;

    private GameObject mCurrentLevel;
    private GameObject mPreviousLevel;
    private int mLevelCpt = 0;
    private List<Enemy> mAllEnemyList;
    private bool mIsLevelOver = false;

    private Transform mPlayerSpawnTrans;
    private PlayerManager mPlayerManager;
    private ThrowCounterManager mThrowManager;

    public bool mIsUpdatingUi = false;
    const string PLAYER_SPAWN_NAME = "PlayerSpawn";

    private void Start()
    {
        GameEvents.current.onThrowOver += EndThrow;
        GameEvents.current.onXPUpdateOver += UIUpdatesOver;
        GameEvents.current.onGameStart += StartLevel;
        PlayerManager.OnKilledEnemy += IsLevelOver;
        LevelTransitionController.OnLoadingInOver += StartLevel;
        LevelTransitionController.OnLoadingOutOver += LoadingOutOver;
    }
    public void Initialize()
    {
        mPlayerManager = GameManager.mInstance.mPlayerManager;
        mThrowManager = GameManager.mInstance.mThrowCounterManager;
        InitializeFirstLevel();
    }
    // Use this for initialization
    public void InitializeFirstLevel () {
        mAllEnemyList = new List<Enemy>();
        System.Random rnd = new System.Random();
        GameObject[] arr = mEpisode1Levels.OrderBy(x => rnd.Next()).ToArray();
        SpawnNextLevel();
        ResetPlayer();
    }

    private void LoadingOutOver()
    {
        SpawnNextLevel();
        ResetPlayer();
    }

    private void ResetPlayer()
    {
        mPlayerManager.ResetPosition();
    }

    private void SpawnNextLevel()
    {
        if (mCurrentLevel != null)
        {
            Destroy(mCurrentLevel);
        }
        mCurrentLevel = Instantiate(mEpisode1Levels[mLevelCpt], mLevelStartingTransform.position, mLevelStartingTransform.rotation);
       
        mPlayerSpawnTrans = mCurrentLevel.transform.Find(PLAYER_SPAWN_NAME);
        mPlayerManager.SetSpawnTransform(mPlayerSpawnTrans);
        mCurrentLevel.GetComponent<LevelSpawner>().SpawnEntities();
        
        InitializeEnemyList();
        
        mLevelCpt++;
        if(mLevelCpt >= mEpisode1Levels.Length)
        {
            mLevelCpt = 0;
        }
    }

    private void InitializeEnemyList()
    {
        Transform spawnEnemiesParent = mCurrentLevel.gameObject.transform.Find("SpawnedEnemies");
        foreach(Transform child in spawnEnemiesParent)
        {
            mAllEnemyList.Add(child.gameObject.GetComponent<Enemy>());
        }
    }

    private void IsLevelOver()
    {
        foreach (Enemy currEnemy in mAllEnemyList) 
        {
            if (currEnemy.IsAlive())
            {
                return;
            }
        }
        GameEvents.current.ThrowOver();
        mIsLevelOver = true;
    }

    private void StartLevel()
    {
        mIsLevelOver = false;
        GameEvents.current.LevelStart();
    }

    void EndThrow()
    {
        mPlayerManager.StopMovement();
        GameEvents.current.XpUpdateStart();
    }

    void UIUpdatesOver()
    {
        mIsUpdatingUi = false;
        if (mIsLevelOver)
        {
            GameEvents.current.LevelOver();
        }
        else
        {
            if (mThrowManager.IsOutOfThrows())
            {
                GameEvents.current.GameOver();
            }
            else
            {
                mPlayerManager.EnableThrow();
            }
        }
    }
}
