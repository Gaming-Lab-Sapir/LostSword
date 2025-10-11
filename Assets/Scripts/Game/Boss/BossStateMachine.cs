public class BossStateMachine
{
    public BossState Current { get; private set; }
    public void ChangeState(BossState next)
    {
        if (Current == next) return;
        Current?.Exit();
        Current = next;
        Current?.Enter();
    }
}
