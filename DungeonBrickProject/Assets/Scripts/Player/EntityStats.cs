using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public int mLevel;
    public Attribute[] mAttributes;
    public Dictionary<AttributeType, Attribute> mAttributesDict = new Dictionary<AttributeType, Attribute>();

    public delegate void MaxThrowsUpdated();
    public static event MaxThrowsUpdated OnMaxThrowsUpdated;

    public void Initialize()
    {
        foreach(Attribute attribute in mAttributes)
        {
            if (!mAttributesDict.ContainsKey(attribute.mType))
            {
                mAttributesDict.Add(attribute.mType,attribute);
                UpdateTotalAttribute(attribute.mType);
            }
        }
    }

    public int CalculateDamageReceived(EntityStats attackingEntity)
    {
        float attackDamage = 0;
        if (attackingEntity.mAttributesDict.ContainsKey(AttributeType.DAMAGE))
        {
            Attribute mEntityDamage = attackingEntity.mAttributesDict[AttributeType.DAMAGE];
            attackDamage = mEntityDamage.mBaseValue + mEntityDamage.mAdditionalValue;
        }

        return Mathf.RoundToInt(attackDamage);
    }
    public int GetTotalAttributeValue(AttributeType type)
    {
        int total = 0;
        if (mAttributesDict.ContainsKey(type))
        {
            total = mAttributesDict[type].mBaseValue + mAttributesDict[type].mAdditionalValue;
        }
        return total;
    }

    public void UpdateTotalAttribute(AttributeType type)
    {
        if (type == AttributeType.THROWS)
        {
            OnMaxThrowsUpdated();
        }
    }

    public void ApplyBuffs(Dictionary<AttributeType, Attribute> _buffs)
    {
        foreach(KeyValuePair<AttributeType, Attribute> buff in _buffs)
        {
            AddBuff(buff.Value);
        }
    }

    public void AddBuff(Attribute _buff)
    {
        Attribute currStat = mAttributesDict[_buff.mType];
        currStat.mBaseValue += _buff.mBaseValue;
        currStat.mAdditionalValue += _buff.mAdditionalValue;
        UpdateTotalAttribute(currStat.mType);
    }

}

public enum AttributeType
{
    DAMAGE=0,
    HEALTH=1,
    THROWS=2
}

[System.Serializable]
public class Attribute
{
    public AttributeType mType;
    public int mBaseValue;
    public int mAdditionalValue;
    public Attribute(AttributeType _type, int _base, int _add)
    {
        mType = _type;
        mBaseValue = _base;
        mAdditionalValue = _add;
    }
}