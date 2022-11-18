using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : GenericSingleton<UIManager>
{
    [SerializeField] private GameObject MarketPanel;
    [SerializeField] private SSoldierButton sSoldierButtonPrefab;

    [SerializeField] private Text TextBlueHealth;
    [SerializeField] private Text TextRedHealth;
    [SerializeField] private Button BtnUpgrade;
    [SerializeField] private Text TextUpgrade;
    [SerializeField] private Text TextPlayerGold;

    [SerializeField] private GameObject VictoryPanel;
    [SerializeField] private GameObject DefeatPanel;

    private Market PlayerMarket;

    private List<SSoldierButton> soldierButtons;


    public void SetCastleHealth(ETeam team, float health)
    {
        if(team == ETeam.BlueTeam)
        {
            TextBlueHealth.text = health.ToString();
        }
        else
        {
            TextRedHealth.text = health.ToString();
        }
        
    }

    public void ShowDefeatScreen()
    {
        DefeatPanel.SetActive(true);
    }
    public void ShowVictoryScreen()
    {
        VictoryPanel.SetActive(true);
    }


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
            btn.soldierImage.sprite = marketItem.CurrentSoldierSprite();
            btn.AttackDamageText.text = ((int)marketItem.CurrentSoldierData().attackDamage).ToString();
            btn.HealthText.text = ((int)marketItem.CurrentSoldierData().health).ToString();
            btn.soldierButton.onClick.RemoveAllListeners();
            btn.soldierButton.onClick.AddListener(delegate {AudioManager.Instance.PlayBuy(); PlayerMarket.TryBuyAt(marketItem, PlayerGoldData);});
            index++;
        }
        TextUpgrade.text = "UPGRADE(" + PlayerMarket.CurrentUpgradeGold().ToString() + ")";
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
            index++;
        }

        if(PlayerMarket.CanUpgradeMore())
        {
            if(PlayerMarket.CurrentUpgradeGold() <= playerGold)
            {
                var btnColor = BtnUpgrade.image.color;
                BtnUpgrade.image.color = new Color(btnColor.r, btnColor.g, btnColor.b, 1);
                BtnUpgrade.enabled = true;
                TextUpgrade.text = "UPGRADE(" + PlayerMarket.CurrentUpgradeGold().ToString() + ")";
            }
            else
            {
                var btnColor = BtnUpgrade.image.color;
                BtnUpgrade.image.color = new Color(btnColor.r, btnColor.g, btnColor.b, 0.2f);
                BtnUpgrade.enabled = false;
            }
        }
        TextPlayerGold.text = playerGold.ToString();
    }

    public void UpgradeAge(CGoldData playerGoldData)
    {
        if(PlayerMarket.CanUpgradeMore())
        {
            if(PlayerMarket.TryUpgrade(playerGoldData))
            {
                AudioManager.Instance.PlayUpgrade();
                UpdateMarket(playerGoldData);
            }
        }
    }

    private void UpdateMarket(CGoldData playerGoldData)
    {
        var index = 0;
        foreach(var item in PlayerMarket.GetMarketItems())
        {
            soldierButtons[index].soldierGoldText.text = item.CurrentSoldierGold().ToString();
            soldierButtons[index].soldierImage.sprite = item.CurrentSoldierSprite();
            soldierButtons[index].AttackDamageText.text = ((int)item.CurrentSoldierData().attackDamage).ToString();
            soldierButtons[index].HealthText.text = ((int)item.CurrentSoldierData().health).ToString();
            soldierButtons[index].soldierButton.onClick.RemoveAllListeners();
            soldierButtons[index].soldierButton.onClick.AddListener(delegate {AudioManager.Instance.PlayBuy(); PlayerMarket.TryBuyAt(item, playerGoldData);});
            index++;
        }
        if(!PlayerMarket.CanUpgradeMore())
        {
            var image = BtnUpgrade.GetComponent<Image>();
            if(image.enabled)
            {
                image.enabled = false;
                BtnUpgrade.enabled =false;
                TextUpgrade.text = "";
            }
        }
    }
}