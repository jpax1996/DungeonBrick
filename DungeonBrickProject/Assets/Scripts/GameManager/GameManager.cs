using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mPlayerPrefab;
    //public GameObject mPlayerHealthBarObj;
    private PlayerManager mPlayerManager;
    public ExperienceManager mExperienceManager;
    public ThrowCounterManager mThrowCounterManager;
    public LevelManager mLevelManager;
    public InventoryManager mInventoryManager;
    private static GameManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        OnStartGame();
    }


    /*private void Start()
    {
        OnStartGame();
    }*/

    private void OnStartGame()
    {
        InstantiatePlayer();
        mLevelManager.Initialize(mPlayerManager);
        mLevelManager.InitializeFirstLevel();
    }

    private void InstantiatePlayer()
    {
        GameObject PlayerGameObject = Instantiate(mPlayerPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), this.transform);
        mPlayerManager = PlayerGameObject.GetComponent<PlayerManager>();
    }

    public static PlayerManager GetPlayerManager()
    {
        return instance.mPlayerManager;
    }

    public static ExperienceManager GetExperienceManager()
    {
        return instance.mExperienceManager;
    }
    
    public static ThrowCounterManager GetThrowCounterManager()
    {
        return instance.mThrowCounterManager;
    }
    
    public static LevelManager GetLevelManager()
    {
        return instance.mLevelManager;
    }
    
    public static InventoryManager GetInventoryManager()
    {
        return instance.mInventoryManager;
    }
}
