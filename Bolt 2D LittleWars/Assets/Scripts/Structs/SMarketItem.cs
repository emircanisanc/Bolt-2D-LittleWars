using UnityEngine;

[System.Serializable]
public class SMarketItem
{
    public Soldier[] soldierPrefabs;
    public Sprite[] soldierSprites;
    public SSoldierData[] soldierDatas;
    private int currentLevel;
    public int[] soldierGolds;

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

    private bool CanUpgradeMore()
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

}
