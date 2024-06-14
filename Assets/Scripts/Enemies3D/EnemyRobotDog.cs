using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobotDog : Enemy3D
{
    [SerializeField] private float attackInitDuration;
    [SerializeField] private float attackDuration;
    [SerializeField] private float attackDelayDuration;
    [SerializeField] private float alertRadius;
    private Transform patrolRadiusCenter;
    [SerializeField] private float patrolRadius;
    [SerializeField] private int bodyHitDamage;
    [SerializeField] private int attackHitDamage;

    private bool isAttacking = false;

    private bool killedAnimation = false;
    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    private float attackDistance;

    private EnemyAttackCollider3D attackCollider;
    // Start is called before the first frame update
    private void Awake() {
        SetBaseDamage(attackHitDamage);
        patrolRadiusCenter = transform;
    }

    void Start()
    {
        InitRobotDogObjects();
        EnemiesUtils.ToggleAttackCollider(attackCollider, false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInRange();
        GetAnimations();
    }

    public float GetAlertRadius() {
        return alertRadius;
    }

    public Vector3 GetPatrolRadiusPosition() {
        return patrolRadiusCenter.position;
    }

    public float GetPatrolRadius() {
        return patrolRadius;
    }

    private void RobotDogAttack() {
        if(!isAttacking && !killed) {
            StartCoroutine(RobotDogAttackCoroutine());
        }
    }

    private void GetAnimations() {
        animator.SetBool("IsAttacking", isAttacking);
        animator.SetBool("IsKilled", killed);
    }

    private IEnumerator RobotDogAttackCoroutine() {
        //Ataque do inimigo
        isAttacking = true;
        yield return new WaitForSeconds(attackDelayDuration);

        //Collider do ataque desativado
        EnemiesUtils.ToggleAttackCollider(attackCollider,isAttacking);
        yield return new WaitForSeconds(attackDuration);

        //Fim animação ataque
        EnemiesUtils.ToggleAttackCollider(attackCollider, !isAttacking);
        yield return new WaitForSeconds(attackDelayDuration);
        
        isAttacking = false;
    }

    private void InitRobotDogObjects() {
        attackCollider = transform.Find("AttackCollider").gameObject.GetComponent<EnemyAttackCollider3D>();
        player = Player3D.Instance.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attackDistance = agent.stoppingDistance;
    }

    private void CheckPlayerInRange() {
        if(!isAttacking && Vector3.Distance(transform.position, player.position) <= attackDistance) {
            RobotDogAttack();
        }
    }

    public override void SetBaseDamage(int damage) {
        bodyDamage = bodyHitDamage;
        attackDamage = attackHitDamage;
    }

    public override void Kill() {
        isAttacking = false;
        killed = true;
        KilledAnimation();
    }

    private void KilledAnimation() {
        //StartCoroutine(KilledAnimationCoroutine());
        gameObject.SetActive(false);
    }

    private IEnumerator KilledAnimationCoroutine() {
        for(int i = 0; i < 64; i++) {
            yield return new WaitForSeconds(0.8f);
        }

        
    }
}
