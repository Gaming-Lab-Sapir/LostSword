using UnityEngine;

public class BossHurtState : BossState
{
    float passTime;
    const float smallStunSeconds = 0.25f;

    public override void Enter()
    {
        passTime = 0f;
        boss.SetMoving(false);
        boss.StopMotion();
        boss.Anim.SetTrigger("Hurt");
    }

    public override void Update()
    {
        passTime += Time.deltaTime;

        if (boss.CurrentHealth <= 0)
        {
            boss.Fsm.ChangeState(boss.DeadState);
            return;
        }

        if (passTime >= smallStunSeconds)
        {
            boss.Fsm.ChangeState(boss.RunState);
        }
    }
}
