using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] private SMarketItem[] soldiers;
    [SerializeField] private Castle _Castle;
    private int level;
    [SerializeField] private int maxLevel;
    [SerializeField] private int[] upgradeGolds;

    public bool TryBuyAt(int index, CGoldData goldData)
    {
        var soldierData = soldiers[index];
        if(goldData.GetGold() >= soldierData.CurrentSoldierGold() && _Castle.CanSpawn())
        {
            goldData.ReduceGold(soldierData.CurrentSoldierGold());
            _Castle.SpawnSoldier(soldierData.CurrentSoldier());
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryBuyAt(SMarketItem marketItem, CGoldData goldData)
    {
        if(goldData.GetGold() >= marketItem.CurrentSoldierGold() && _Castle.CanSpawn())
        {
            goldData.ReduceGold(marketItem.CurrentSoldierGold());
            _Castle.SpawnSoldier(marketItem.CurrentSoldier());
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanUpgradeMore()
    {
        return level < maxLevel;
    }

    public int CurrentUpgradeGold()
    {
        return upgradeGolds[level];
    }

    public bool TryUpgrade(CGoldData goldData)
    {
        if(CanUpgradeMore())
        {
            if(goldData.GetGold() >= CurrentUpgradeGold())
            {
                goldData.ReduceGold(CurrentUpgradeGold());
                foreach(var item in soldiers)
                {
                    item.Upgrade();
                }
                level++;
                _Castle.Upgrade();
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
        
    }

    public SMarketItem[] GetMarketItems()
    {
        return soldiers;
    }
}
