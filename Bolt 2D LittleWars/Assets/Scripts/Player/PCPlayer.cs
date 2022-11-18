using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCPlayer : Player
{
    [SerializeField] private float criticHealth;
    [SerializeField] private CHealth CHealthCastle;
    private int lastSpawned = 0;

    void Update()
    {
        if(Castle.blueSoldiersCount-1 > Castle.redSoldiersCount)
        {
            if(CHealthCastle.GetCurrentHealth() <= criticHealth)
            {
                // critic state
                if(_Market.TryBuyAt(2, _GoldData))
                {
                    lastSpawned = 2;
                }
                else
                {
                    TrySpawnRandom();
                }
            }
            else
            {
                // not critic state
                TrySpawnByLastSpawned();
            }
        }
        else
        {
            // has equal or more soldier
            if(_Market.CanUpgradeMore())
            {
                if(!_Market.TryUpgrade(_GoldData))
                {
                    if(_GoldData.GetGold() > 10)
                    {
                        if(Random.Range(0, 100) < 2)
                        {
                            TrySpawnByLastSpawned();
                        }
                    }
                    
                }
            }
            else
            {
                TrySpawnByLastSpawned();
            }
        }
    }

    private void TrySpawnByLastSpawned()
    {
        
        switch(lastSpawned)
        {
            case 0:
                TrySpawnRandom();
                break;
            case 1:
                TrySpawnRandom();
                break;
            case 2:
                if(_Market.TryBuyAt(1, _GoldData))
                {
                    lastSpawned = 1;
                }
                break;
        }
    }

    private bool TrySpawnRandom()
    {
        var random = 0;
        random = Random.Range(0, 3);
        if(_Market.TryBuyAt(random, _GoldData))
        {
            lastSpawned = random;
            return true;
        }
        return false;
    }
}
