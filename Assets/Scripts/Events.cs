using System;

public static class Events
{
    public static Action<int> OnScoreUpdate;

    public static Action<GameState> OnStateEnter;
    public static Action<GameState> OnStateExit;
    public static Func<GameState> OnGetCurrentState;
}
