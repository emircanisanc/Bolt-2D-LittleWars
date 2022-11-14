using UnityEngine;

[System.Serializable]
public struct SMarketItem
{
    public Soldier[] soldierPrefabs;
    public Sprite[] soldierSprites;
    public SSoldierData[] soldierDatas;
    private int currentLevel;
    public int[] soldierGolds;
    public int[] upgradeGolds;

    public Soldier CurrentSoldier()
    {
        return soldierPrefabs[currentLevel];
    }

    public void Upgrade()
    {
        if(CanUpgradeMore())
        {
            currentLevel++;
        }
    }

    public bool CanUpgradeMore()
    {
        return soldierPrefabs.Length-1 > currentLevel;
    }

    public SSoldierData CurrentSoldierData()
    {
        return soldierDatas[currentLevel];
    }

    public Sprite CurrentSoldierSprite()
    {
        return soldierSprites[currentLevel];
    }
    public int CurrentSoldierGold()
    {
        return soldierGolds[currentLevel];
    }
    public int CurrentUpgradeGold()
    {
        if(upgradeGolds.Length-1 >= currentLevel)
        {
            return upgradeGolds[currentLevel];
        }
        else
        {
            return 0;
        }
    }
}
