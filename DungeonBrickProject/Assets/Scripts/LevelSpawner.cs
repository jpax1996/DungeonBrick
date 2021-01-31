using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelSpawner : MonoBehaviour
{

    const string BAT_PREFAB_NAME = "BatPrefab";

    public GameObject mPlayerPrefab;
    public GameObject mBatPrefab;
    public Transform mPlayerSpawnTrans;
    public Transform mEnemiesParentTrans;
    public Transform mSpawnedEnemiesTrans;
    
    // Start is called before the first frame update
    public void SpawnEntities()
    {
        Instantiate(mPlayerPrefab, mPlayerSpawnTrans.position, mPlayerSpawnTrans.rotation, this.transform);
        foreach (Transform child in mEnemiesParentTrans)
        {
            if (child.name.Contains(BAT_PREFAB_NAME))
            {
                Instantiate(mBatPrefab,child.position, child.rotation, mSpawnedEnemiesTrans);
                Destroy(child.gameObject);
            }
        }
        Destroy(mEnemiesParentTrans.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
