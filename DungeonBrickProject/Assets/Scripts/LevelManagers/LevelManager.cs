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

    private Transform mPlayerSpawnTrans;
    private PlayerManager mPlayerManager;

    public delegate void LevelStart();
    public static event LevelStart OnLevelStart;

    public delegate void LevelOver();
    public static event LevelOver OnLevelOver;

    const string PLAYER_SPAWN_NAME = "PlayerSpawn";

    public void Initialize(PlayerManager playerManager)
    {
        mPlayerManager = playerManager;
    }

    // Use this for initialization
    public void InitializeFirstLevel () {
        mAllEnemyList = new List<Enemy>();
        System.Random rnd = new System.Random();
        GameObject[] arr = mEpisode1Levels.OrderBy(x => rnd.Next()).ToArray();
        PlayerManager.OnKilledEnemy += IsLevelOver;
        LevelTransitionController.OnLoadingInOver += StartLevel;
        LevelTransitionController.OnLoadingOutOver += LoadingOutOver;
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
        mPlayerManager.ResetState();
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
        OnLevelOver();
    }

    private void StartLevel()
    {
        OnLevelStart();
    }

    void Update()
    {
    }
}
