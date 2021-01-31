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

    public delegate void LevelOver();
    public static event LevelOver OnLevelOver;

    // Use this for initialization
    void Start () {
        mAllEnemyList = new List<Enemy>();
        System.Random rnd = new System.Random();
        GameObject[] arr = mEpisode1Levels.OrderBy(x => rnd.Next()).ToArray();
        mCurrentLevel = Instantiate(mEpisode1Levels[mLevelCpt], mLevelStartingTransform.position, mLevelStartingTransform.rotation);
        mCurrentLevel.GetComponent<LevelSpawner>().SpawnEntities();
        InitializeEnemyList();
        Enemy.OnEnemyDead += IsLevelOver;
        mLevelCpt++;
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
            if (currEnemy.mIsAlive)
            {
                return;
            }
        }
        OnLevelOver();
    }

    void Update()
    {
    }
}
