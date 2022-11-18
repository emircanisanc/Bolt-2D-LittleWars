using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    [SerializeField] private Player[] players;

    public void OnGameEnd(ETeam loser)
    {
        foreach(var player in players)
        {
            player.enabled = false;
        }
        if(loser == ETeam.BlueTeam)
        {
            OnEnemyWins();
        }
        else
        {
            OnPlayerWins();
        }
    }

    private void OnPlayerWins()
    {
        AudioManager.Instance.PlayVictory();
        UIManager.Instance.ShowVictoryScreen();
    }
    private void OnEnemyWins()
    {
        AudioManager.Instance.PlayDefeat();
        UIManager.Instance.ShowDefeatScreen();
    }
}
