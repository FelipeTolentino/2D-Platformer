using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] bool showAttackRange;   
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayer;
    
    [SerializeField] float attackRate = 2f;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] int attackDamage = 40;
    
    Animator animator;
    
    float nextAttackTimer = 0f;
    

    // Update is called once per frame
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(Time.time >= nextAttackTimer)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
                nextAttackTimer = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack() {
        animator.SetTrigger("Attack");
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
         
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.tag == "Enemies")
                enemy.GetComponent<EnemyHealth>().takeDamage(attackDamage);

            if (enemy.gameObject.tag == "Breakable")
                enemy.GetComponent<Breakable>().Break();
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
