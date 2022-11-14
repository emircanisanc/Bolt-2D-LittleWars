using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    void Start()
    {
        UIManager.Instance.CreateMarketUI(_Market, _GoldData);
        _GoldData._OnGoldChanged =UIManager.Instance.UpdateMarketUI;
    }
}
