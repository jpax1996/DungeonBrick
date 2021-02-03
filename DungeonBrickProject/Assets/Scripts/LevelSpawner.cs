using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelSpawner : MonoBehaviour
{
    public GameObject mBatPrefab;
    public Transform mEnemiesParentTrans;
    public Transform mSpawnedEnemiesTrans;
    
    const string BAT_PREFAB_NAME = "BatPrefab";
    // Start is called before the first frame update
    public void SpawnEntities()
    {
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
