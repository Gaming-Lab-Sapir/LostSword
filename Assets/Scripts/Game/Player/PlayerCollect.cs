using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public int totalCoins { get; private set; }

    void OnEnable() { GameEvents.CoinCollected += OnCoinCollected; }
    void OnDisable() { GameEvents.CoinCollected -= OnCoinCollected; }

    void OnCoinCollected(int amount)
    {
        AddCoins(amount);
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log("Total Coins: " + totalCoins);
    }
}
