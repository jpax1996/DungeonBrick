using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraThrow : Item
{
    public int mNumberExtraThrows;

    public override void OnPickup(EntityStats _entityStats, int _amount)
    {
        base.OnPickup(_entityStats, _amount);
        Attribute buff = new Attribute(AttributeType.THROWS,0, mNumberExtraThrows * _amount);
        ItemBuffs[buff.mType] = buff;
        entityStats.ApplyBuffs(ItemBuffs);
    }

    public override void SetStack(int _amount)
    {
        int diffStack = _amount - ItemStack;
        int additionalThrows = mNumberExtraThrows * diffStack;
        ItemBuffs[AttributeType.THROWS].mAdditionalValue = additionalThrows;
        entityStats.ApplyBuffs(ItemBuffs);
        ItemStack = _amount;
    }
    
    public override void AddStack(int _amount)
    {
        int additionalThrows = mNumberExtraThrows * _amount;
        ItemBuffs[AttributeType.THROWS].mAdditionalValue = additionalThrows;
        entityStats.ApplyBuffs(ItemBuffs);
        ItemStack = _amount;
    }

    public override void RemoveItem(int _amount)
    {
        int additionalThrows = mNumberExtraThrows * -_amount;
        ItemBuffs[AttributeType.THROWS].mAdditionalValue = additionalThrows;
        entityStats.ApplyBuffs(ItemBuffs);
        ItemStack -= _amount;
    }
}
