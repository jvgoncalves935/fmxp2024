using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobotDog : Enemy3D
{
    [SerializeField] private float attackInitDuration;
    [SerializeField] private float attackDelayDuration;
    [SerializeField] private bool isAttacking = false;

    private EnemyAttackCollider3D attackCollider;
    // Start is called before the first frame update
    void Start()
    {
        InitRobotDogObjects();
        EnemiesUtils.ToggleAttackCollider(attackCollider, false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInRange();
    }

    private void RobotDogAttack() {
        if(!isAttacking) {
            StartCoroutine(RobotDogAttackCoroutine());
        }
    }

    private IEnumerator RobotDogAttackCoroutine() {
        //Ataque do inimigo
        isAttacking = true;
        EnemiesUtils.ToggleAttackCollider(attackCollider,isAttacking);
        ToggleEnemyMovement(!isAttacking);

        yield return new WaitForSeconds(attackInitDuration);

        //Collider do ataque desativado
        EnemiesUtils.ToggleAttackCollider(attackCollider, !isAttacking);
        yield return new WaitForSeconds(attackDelayDuration);

        //Fim anima��o ataque
        isAttacking = false;
        ToggleEnemyMovement(!isAttacking);


        yield return new WaitForSeconds(3.0f);

    }

    private void InitRobotDogObjects() {
        attackCollider = transform.Find("AttackCollider").gameObject.GetComponent<EnemyAttackCollider3D>();
    }

    private void ToggleEnemyMovement(bool toggle) {

    }

    private void CheckPlayerInRange() {
        if(!isAttacking) {
            RobotDogAttack();
        }
    }
}
