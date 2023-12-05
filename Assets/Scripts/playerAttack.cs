using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    [SerializeField]
    float attackRate = 0f;
    float timeBetweenAttacks = 2f;

    [SerializeField]
    LayerMask enemies;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Transform attackLocation;

    [SerializeField]
    float attackRange = 2;

    [SerializeField]
    int attackDamage = 20;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        attackRate += Time.deltaTime;

        if (Input.GetKeyDown("v") && attackRate > timeBetweenAttacks)
        {
            Attack();
            animator.Play("Attack");
            attackRate = 0f;
        }

    }

    private void Attack()
    {
        Collider2D[] hitsEnemies = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, enemies);

        foreach (Collider2D enemy in hitsEnemies)
        {
            enemy.GetComponent<enemyController>().Hit(attackDamage);
            Debug.Log("Hit");
        }
     }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackLocation.position, attackRange);
    }
}
