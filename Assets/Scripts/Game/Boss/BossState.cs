using UnityEngine;

public abstract class BossState
{
    public BossVampire boss;

    public void Initialize(BossVampire boss) { this.boss = boss; }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
}
