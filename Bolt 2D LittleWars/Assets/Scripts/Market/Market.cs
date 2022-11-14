using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] private SMarketItem[] soldiers;
    [SerializeField] private Castle _Castle;

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

    public SMarketItem[] GetMarketItems()
    {
        return soldiers;
    }
}
