using UnityEngine;

public class BossAttackState : BossState
{
    float passTime;
    bool didHit;

    public override void Enter()
    {
        passTime = 0f;
        didHit = false;
        boss.StopMotion();
        if (boss.Target) boss.FaceTarget((Vector2)boss.Target.position);
        boss.Anim.SetTrigger("Attack");
    }

    public override void Update()
    {
        passTime += Time.deltaTime;

        if (!didHit && passTime >= boss.AttackDelay)
        {
            didHit = true;
            Vector2 center = boss.AttackCenter();
            var hits = Physics2D.OverlapCircleAll(center, boss.AttackRadius, boss.PlayerLayers);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].CompareTag("Player"))
                {
                    boss.DealDamageToPlayer();
                    break;
                }
            }
        }

        if (passTime >= boss.AttackDuration)
        {
            boss.NextAttackAllowedTime = Time.time + boss.AttackCooldown;
            boss.Fsm.ChangeState(boss.RunState);
        }
    }

    public override void Exit()
    {
        boss.StopMotion();
    }
}
