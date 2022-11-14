using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : GenericSingleton<UIManager>
{
    [SerializeField] private GameObject MarketPanel;
    [SerializeField] private SSoldierButton sSoldierButtonPrefab;
    private Market PlayerMarket;

    private List<SSoldierButton> soldierButtons;


    public void CreateMarketUI(Market PlayerMarket, CGoldData PlayerGoldData)
    {
        this.PlayerMarket = PlayerMarket;
        soldierButtons = new List<SSoldierButton>();
        var index = 0;
        foreach(var marketItem in PlayerMarket.GetMarketItems())
        {
            var btn = Instantiate(sSoldierButtonPrefab.gameObject, MarketPanel.transform).GetComponent<SSoldierButton>();
            soldierButtons.Add(btn);
            btn.soldierGoldText.text = marketItem.CurrentSoldierGold().ToString();
            btn.upgradeText.text = "UPGRADE("+marketItem.CurrentUpgradeGold().ToString()+")";
            btn.soldierImage.sprite = marketItem.CurrentSoldierSprite();
            btn.AttackDamageText.text = ((int)marketItem.CurrentSoldierData().attackDamage).ToString();
            btn.HealthText.text = ((int)marketItem.CurrentSoldierData().health).ToString();
            btn.soldierButton.onClick.AddListener(delegate {PlayerMarket.TryBuyAt(marketItem, PlayerGoldData);});
            index++;
        }
    }

    public void UpdateMarketUI(int playerGold)
    {
        var index = 0;
        foreach(var marketItem in PlayerMarket.GetMarketItems())
        {
            if(marketItem.CurrentSoldierGold() <= playerGold)
            {
                soldierButtons[index].soldierButton.enabled = true;
                var color = soldierButtons[index].BackgroundImage.color;
                soldierButtons[index].BackgroundImage.color = new Color(color.r, color.g, color.b, 1f);
            }
            else
            {
                soldierButtons[index].soldierButton.enabled = false;
                var color = soldierButtons[index].BackgroundImage.color;
                soldierButtons[index].BackgroundImage.color = new Color(color.r, color.g, color.b, 0.2f);
            }
            if(marketItem.CanUpgradeMore())
            {
                soldierButtons[index].upgradeButton.enabled = marketItem.CurrentUpgradeGold() <= playerGold;
            }
            else
            {
                soldierButtons[index].upgradeButton.gameObject.SetActive(false);
            }
            index++;
        }
    }
}
