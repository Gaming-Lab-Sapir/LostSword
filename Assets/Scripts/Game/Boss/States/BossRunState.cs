using UnityEngine;

public class BossRunState : BossState
{
    public override void Enter()
    {
        boss.SetMoving(true);
    }

    public override void FixedUpdate()
    {
        if (!boss.Target)
        {
            boss.StopMotion();
            boss.SetMoving(false);
            return;
        }

        Vector2 bossPosition = (Vector2)boss.transform.position;
        Vector2 targetPosition = (Vector2)boss.Target.position;
        Vector2 direction = (targetPosition - bossPosition).normalized;

        boss.FaceTarget(targetPosition);

        float stopSqr = boss.StopDistance * boss.StopDistance;
        if ((targetPosition - bossPosition).sqrMagnitude <= stopSqr)
        {
            boss.StopMotion();
            boss.SetMoving(false);
            boss.Fsm.ChangeState(boss.AttackState);
            return;
        }

        boss.Rb.linearVelocity = direction * boss.MovementSpeed;
    }

    public override void Exit()
    {
        boss.SetMoving(false);
        boss.StopMotion();
    }
}
