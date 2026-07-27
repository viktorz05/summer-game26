using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageAble
{
    public float health = 10f;
    public float speed = 1.5f;
    public float attackRange = 1f;

    [SerializeField] private Transform player;
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       agent = GetComponent<NavMeshAgent>();
       agent.speed = speed;
       player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange)
            Attack();
        else
            Chase();
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        agent.ResetPath();
    }

    public void TakeDamage(uint damage)
    {
        health -= damage;
        if (health <= 0f) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
