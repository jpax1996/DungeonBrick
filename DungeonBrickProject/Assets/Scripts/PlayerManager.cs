using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : EntityStats
{
    private PlayerHealthBar mPlayerHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetPlayerHealthBar(GameObject HealthBarObj)
    {
        mPlayerHealthBar = HealthBarObj.GetComponent<PlayerHealthBar>();
        mPlayerHealthBar.SetSlider(HealthBarObj.GetComponent<Slider>());
        mPlayerHealthBar.SetMaxHealth(mMaxHealth);
        mPlayerHealthBar.SetCurrentHealth(mMaxHealth);
    }

    public void OnHitEnemy(GameObject EnemyObj)
    {
        EnemyObj.GetComponent<Enemy>().OnHit(this.GetComponent<EntityStats>());
        int damage = CalculateDamageReceived(EnemyObj.GetComponent<EntityStats>());
        mPlayerHealthBar.OnDamaged(damage);

    }
}
