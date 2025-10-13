using System;
using System.Collections;
using UnityEngine;

public class BossVampire : MonoBehaviour
{
    [Header("General")]
    [SerializeField] float movementSpeed = 2.5f;
    [SerializeField] float stopDistance = 0.8f;
    [SerializeField] int health = 120;
    [SerializeField] float deathDelay = 0.8f;

    [Header("Combat")]
    [SerializeField] LayerMask playerLayers;
    [SerializeField] int attackDamage = 15;
    [SerializeField] float attackRadius = 1.5f;
    [SerializeField] float attackDistance = 0.5f;
    [SerializeField] float nextAttackAllowedTime = 0f;
    [SerializeField] float attackDelay = 0.5f;
    [SerializeField] float attackDuration = 0.5f;
    [SerializeField] float attackCooldown = 1.5f;
    public int CurrentHealth { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public Transform Target { get; private set; }
    public BossStateMachine Fsm { get; private set; }
    public BossRunState RunState { get; private set; }
    public BossAttackState AttackState { get; private set; }
    public BossHurtState HurtState { get; private set; }
    public BossDeadState DeadState { get; private set; }

    Vector2 lookDirection = Vector2.right;

    public float MovementSpeed => movementSpeed;
    public float StopDistance => stopDistance;
    public float AttackRadius => attackRadius;
    public float AttackDistance => attackDistance;
    public LayerMask PlayerLayers => playerLayers;
    public float AttackDelay => attackDelay;
    public float AttackDuration => attackDuration;
    public float AttackCooldown => attackCooldown;
    public float NextAttackAllowedTime
    {
        get => nextAttackAllowedTime;
        set => nextAttackAllowedTime = value;
    }

    public event Action OnBossDestroyed;

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        CurrentHealth = health;

        Fsm = new BossStateMachine();
        RunState = new BossRunState(); RunState.Initialize(this);
        AttackState = new BossAttackState(); AttackState.Initialize(this);
        HurtState = new BossHurtState(); HurtState.Initialize(this);
        DeadState = new BossDeadState(); DeadState.Initialize(this);
    }

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player) Target = player.transform;
        Fsm.ChangeState(RunState);
    }

    void Update() { Fsm.Current?.Update(); }
    void FixedUpdate() { Fsm.Current?.FixedUpdate(); }

    public void OnArrowHit(int damage)
    {
        if (Fsm.Current == DeadState) return;
        CurrentHealth = Mathf.Max(0, CurrentHealth - Mathf.Max(1, damage));
        if (CurrentHealth <= 0) Fsm.ChangeState(DeadState);
        else Fsm.ChangeState(HurtState);
    }

    public void DealDamageToPlayer()
    {
        GameEvents.RaisePlayerDamaged(attackDamage);
    }

    public void SetMoving(bool moving) { Anim.SetBool("Moving", moving); }

    public void FaceTarget(Vector2 worldPosition)
    {
        Vector2 diraction = worldPosition - (Vector2)transform.position;
        if (diraction.sqrMagnitude < 0.0001f) return;
        diraction.Normalize();
        lookDirection = diraction;
        Anim.SetFloat("MoveX", diraction.x);
        Anim.SetFloat("MoveY", diraction.y);
    }

    public Vector2 AttackCenter()
    {
        return (Vector2)transform.position + lookDirection * attackDistance;
    }

    public void StopMotion() { Rb.linearVelocity = Vector2.zero; }

    public IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(deathDelay);
        OnBossDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
