using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobotDog : Enemy3D
{
    [SerializeField] private float attackInitDuration;
    [SerializeField] private float attackDelayDuration;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private int bodyHitDamage;
    [SerializeField] private int attackHitDamage;

    private bool killedAnimation = false;
    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    private float attackDistance;

    private EnemyAttackCollider3D attackCollider;
    // Start is called before the first frame update
    private void Awake() {
        SetBaseDamage(attackHitDamage);
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
        EnemiesUtils.ToggleAttackCollider(attackCollider,isAttacking);

        //Collider do ataque desativado
        yield return new WaitForSeconds(attackInitDuration);
        EnemiesUtils.ToggleAttackCollider(attackCollider, !isAttacking);

        yield return new WaitForSeconds(attackDelayDuration);
        //Fim animação ataque
        isAttacking = false;

        yield return new WaitForSeconds(3.0f);

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
    }
}
