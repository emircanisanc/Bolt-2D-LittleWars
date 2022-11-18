using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoldData : MonoBehaviour
{
    [SerializeField] private float goldRate;
    [SerializeField] private int goldLimit;
    private float nextIncTime;
    private int gold;

    public delegate void OnGoldChanged(int gold);
    public OnGoldChanged _OnGoldChanged;

    public int GetGold()
    {
        return gold;
    }

    public void ReduceGold(int delta)
    {
        if(gold >= delta)
        {
            gold -= delta;
            _OnGoldChanged?.Invoke(gold);
        }
    }

    public void IncGold()
    {
        if(gold < goldLimit)
        {
            gold++;
            _OnGoldChanged?.Invoke(gold);
        }
        
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad >= nextIncTime)
        {
            nextIncTime = Time.timeSinceLevelLoad + goldRate;
            IncGold();
        }
    }
}
