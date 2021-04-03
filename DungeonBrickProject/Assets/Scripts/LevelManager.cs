using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject mPlayerPrefab;
    public GameObject[] mEpisode1Levels;
    public Transform mLevelStartingTransform;
    public GameObject mPlayerHealthBarObj;
    public GameObject mExperienceManager;
    public GameObject mThrowCounterManager;

    private GameObject mCurrentLevel;
    private GameObject mPreviousLevel;
    private int mLevelCpt = 0;
    private List<Enemy> mAllEnemyList;
    private Transform mPlayerSpawnTrans;
    private GameObject mPlayerGameObject;
    
    public delegate void LevelStart();
    public static event LevelStart OnLevelStart;

    public delegate void LevelOver();
    public static event LevelOver OnLevelOver;

    const string PLAYER_SPAWN_NAME = "PlayerSpawn";

    // Use this for initialization
    void Start () {
        mAllEnemyList = new List<Enemy>();
        System.Random rnd = new System.Random();
        GameObject[] arr = mEpisode1Levels.OrderBy(x => rnd.Next()).ToArray();
        PlayerManager.OnKilledEnemy += IsLevelOver;
        LevelTransitionController.OnLoadingInOver += StartLevel;
        LevelTransitionController.OnLoadingOutOver += LoadingOutOver;
        InstantiatePlayer();
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
        mPlayerGameObject.GetComponent<PlayerManager>().ResetState();
    }
    private void InstantiatePlayer()
    {
        if (mPlayerGameObject == null)
        {
            mPlayerGameObject = Instantiate(mPlayerPrefab, new Vector3(0,0,0), new Quaternion(0,0,0,0), this.transform);
            PlayerManager currentPlayerManager = mPlayerGameObject.GetComponent<PlayerManager>();
            currentPlayerManager.SetPlayerHealthBar(mPlayerHealthBarObj);
            currentPlayerManager.SetExperienceManager(mExperienceManager);
            currentPlayerManager.SetThrowCounterManager(mThrowCounterManager);
        }
    }

    private void SpawnNextLevel()
    {
        if (mCurrentLevel != null)
        {
            Destroy(mCurrentLevel);
        }
        mCurrentLevel = Instantiate(mEpisode1Levels[mLevelCpt], mLevelStartingTransform.position, mLevelStartingTransform.rotation);
        
        mPlayerSpawnTrans = mCurrentLevel.transform.Find(PLAYER_SPAWN_NAME);
        PlayerManager currentPlayerManager = mPlayerGameObject.GetComponent<PlayerManager>();
        currentPlayerManager.SetSpawnTransform(mPlayerSpawnTrans);
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
