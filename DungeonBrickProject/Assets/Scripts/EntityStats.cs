using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public int mLevel;
    public int mBaseDamage;
    public int mAdditionalDamage;
    public int mBaseDefense;
    public int mMaxHealth;

    protected int CalculateDamageReceived(EntityStats attackingEntity)
    {
        int attackDamage = attackingEntity.mBaseDamage + attackingEntity.mAdditionalDamage;
        float rngFactor = Random.Range(0.80f,1.0f);
        float attackDamageRatio = ((float) attackDamage / (float) this.mBaseDefense);
        float totalDamage = attackDamageRatio * attackingEntity.mBaseDamage;
        totalDamage *= rngFactor;
        return Mathf.RoundToInt(totalDamage);
    }
}
