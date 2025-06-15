using System;
using UnityEngine;

/// <summary>
/// 2‑D melee enemy that patrols, chases, and attacks the player.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public Transform[] patrolPoints;        // Add 2+ points in Inspector
    public Transform player;                // Drag your player here
    public LayerMask playerLayer;           // Layer the player is on
    public Animator animator;               // Optional – for walk / attack anims
    public Transform attackPoint; // Empty child at weapon tip

    [Header("Movement")]
    public float moveSpeed   = 2f;
    public float chaseRange  = 5f;

    [Header("Attack")]
    public float attackRange     = 0.7f;
    public float attackCooldown  = 1f;
    public int   damage          = 1;

    

    private Rigidbody2D rb;
    private int currentPatrolIndex;
    private float nextAttackTime;

    // ─────────────────────────────────────────────────────────────────────────────
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPatrolIndex = 0;
        nextAttackTime = 0f;
    }

    // ─────────────────────────────────────────────────────────────────────────────
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            TryAttack();
        }
        else if (distanceToPlayer <= chaseRange)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    // ─────────────────────────────────────────────────────────────────────────────
    #region  ❱❱  Behaviours

    void Patrol()
    {
        Vector2 target   = patrolPoints[currentPatrolIndex].position;
        Vector2 newPos   = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.deltaTime);
        rb.MovePosition(newPos);

        FlipSprite(target.x - transform.position.x);

        // Reached patrol point?
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

       
    }

    void Chase()
    {
        Vector2 dir      = (player.position - transform.position).normalized;
        Vector2 newPos   = rb.position + dir * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPos);

        FlipSprite(dir.x);
        
    }

    void TryAttack()
    {
        if (Time.time < nextAttackTime) return;

        // Freeze movement during attack
        rb.velocity = Vector2.zero;
        

        // Damage is applied via Animation Event OR immediately here:
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange,
                                                 playerLayer);
        if (hit)
        {
            Debug.Log(hit.name);
            hit.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            print("hit");
        }

        nextAttackTime = Time.time + attackCooldown;
        
    }

    #endregion
    // ─────────────────────────────────────────────────────────────────────────────
    void FlipSprite(float directionX)
    {
        if (Mathf.Abs(directionX) < 0.01f) return;   // No horizontal input

        Vector3 scale = transform.localScale;
        scale.x = directionX > 0 ? -1 : 1;
        transform.localScale = scale;
    }

    // ─────────────────────────────────────────────────────────────────────────────
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
}
