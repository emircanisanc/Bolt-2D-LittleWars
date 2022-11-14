using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoldData : MonoBehaviour
{
    [SerializeField] private float goldRate;
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
            _OnGoldChanged(gold);
        }
    }

    public void IncGold()
    {
        gold++;
        _OnGoldChanged(gold);
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
