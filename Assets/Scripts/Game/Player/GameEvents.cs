using System;

public static class GameEvents
{
    public static event Action EnemyKilledByArrow;
    public static event Action<int> PlayerDamaged;
    public static event Action<int> CoinCollected;
    public static event Action<int> ArrowPickedUp;
    public static event Action ArrowShot;
    public static event Action GameLost;
    public static void RaiseArrowShot() => ArrowShot?.Invoke();
    public static void RaiseEnemyKilledByArrow() => EnemyKilledByArrow?.Invoke();
    public static void RaisePlayerDamaged(int amount) => PlayerDamaged?.Invoke(amount);
    public static void RaiseCoinCollected(int amount) => CoinCollected?.Invoke(amount);
    public static void RaiseArrowPickedUp(int amount) => ArrowPickedUp?.Invoke(amount);
    public static void RaiseGameLost() => GameLost?.Invoke();
}
