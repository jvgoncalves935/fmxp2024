using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3D : MonoBehaviour, IDamageable, IEnemyStats
{
    protected bool killed = false;
    protected int damage;
    protected int bodyDamage;
    protected int attackDamage;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }


    public void DamagePlayer(int enemyDamage) {
        if(!killed) {
            Player3D.Instance.PlayerDamage(enemyDamage);
        }
    }

    public void DamagePlayer() {
        if(!killed) {
            Player3D.Instance.PlayerDamage(attackDamage);
        }
    }

    public virtual void Kill() {

    }

    public virtual void Restart() {

    }

    public int GetDamage() {
        return damage;
    }

    public virtual void SetBaseDamage(int baseDamage) {

    }

    public virtual void Attack() {

    }

    private void OnTriggerEnter(Collider collider) {
        if(PlayerUtils.IsPlayerBody(collider) && !Player3D.Instance.IsPlayerHit()) {
            DamagePlayer(bodyDamage);
        }
    }





}