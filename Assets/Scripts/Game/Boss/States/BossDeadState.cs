public class BossDeadState : BossState
{
    public override void Enter()
    {
        boss.SetMoving(false);
        boss.StopMotion();
        boss.Anim.SetTrigger("Death");
        boss.StartCoroutine(boss.DeathRoutine());
    }
}
