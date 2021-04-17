using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mPlayerPrefab;
    public GameObject mPlayerObject;
    public PlayerManager mPlayerManager;
    public ExperienceManager mExperienceManager;
    public ThrowCounterManager mThrowCounterManager;
    public LevelManager mLevelManager;
    public InventoryManager mInventoryManager;
    public DisplayInventory mDisplayInventory;
    private static GameManager instance;
    public static GameManager mInstance { get { return instance; } }
    public bool mGameStarted = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        mGameStarted = false;
        if (mPlayerObject == null)
        {
            InstantiatePlayer();
        }
    }

    public void StartGame()
    {
        mPlayerManager.Initialize();
        mLevelManager.Initialize();
        mInventoryManager.Initialize(mPlayerManager);
        mExperienceManager.Initialize();
        mDisplayInventory.Initialize();
        mThrowCounterManager.Initialize();
        GameEvents.current.GameStart();
        mGameStarted = false;
    }

    public void RestartGame()
    {
        GameEvents.current.GameRestart();
        StartGame();
    }

    private void InstantiatePlayer()
    {
        mInstance.mPlayerObject = Instantiate(mInstance.mPlayerPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), this.transform);
        mInstance.mPlayerManager = mInstance.mPlayerObject.GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if(mGameStarted == false)
        {
            StartGame();
            mGameStarted = true;
        }
    }
}
