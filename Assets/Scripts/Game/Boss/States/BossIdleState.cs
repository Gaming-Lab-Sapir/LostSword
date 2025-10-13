public class BossIdleState : BossState
{
    public override void Enter()
    {
        boss.SetMoving(false);
        boss.StopMotion();
    }

    public override void Update()
    {
        if (boss.Target) boss.Fsm.ChangeState(boss.RunState);
    }
}

