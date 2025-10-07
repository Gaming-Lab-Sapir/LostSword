using UnityEngine;

public class CollectCoinsKillEnemies : QuestStep
{
    [Header("Requirements")]
    [SerializeField] private int coinsToComplete = 10;
    [SerializeField] private int enemiesToKill = 10;

    private int coins;
    private int kills;

    public bool IsComplete => coins >= coinsToComplete && kills >= enemiesToKill;

    private void OnEnable()
    {
        GameEvents.CoinCollected += OnCoinCollected;           
        GameEvents.EnemyKilledByArrow += OnEnemyKilled;        
        UpdateProgressUI();
    }

    private void OnDisable()
    {
        GameEvents.CoinCollected -= OnCoinCollected;
        GameEvents.EnemyKilledByArrow -= OnEnemyKilled;
    }

    private void OnCoinCollected(int amount)
    {
        coins += amount;
        if (coins > coinsToComplete) coins = coinsToComplete;
        UpdateProgressUI();
        TryComplete();
    }

    private void OnEnemyKilled()
    {
        kills += 1;
        if (kills > enemiesToKill) kills = enemiesToKill;
        UpdateProgressUI();
        TryComplete();
    }

    private void TryComplete()
    {
        if (!IsComplete) return;
        FinishQuest(); 
    }

    private void UpdateProgressUI()
    {
        if (questInfo != null)
            GameEvents.RaiseQuestProgress(questInfo.id, coins + kills, coinsToComplete + enemiesToKill);
        
    }

}

