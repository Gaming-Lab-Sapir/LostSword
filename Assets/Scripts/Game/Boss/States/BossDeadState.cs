using UnityEngine;
public class BossDeadState : BossState
{
    public override void Enter()
    {
        boss.SetMoving(false);
        boss.StopMotion();
        boss.Anim.SetTrigger("Death");
        var prefab = boss.SwordPrefab;
        if (prefab != null)
            Object.Instantiate(prefab, boss.transform.position, Quaternion.identity);
        boss.StartCoroutine(boss.DeathRoutine());
    }
}
