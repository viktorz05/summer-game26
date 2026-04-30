using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float health = 100f;
    public float speed = 1.5f;
    public float attackRange = 1f;

    [SerializeField] private Transform player;
    private NavMeshAgent agent = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       agent = GetComponent<NavMeshAgent>();
       player = GameObject.Find("playerCapsule").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Chase();
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {

    }
}
