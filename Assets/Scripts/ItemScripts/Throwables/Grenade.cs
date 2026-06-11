using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour, IDetoneable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] public float blastRadius;
    [SerializeField] public float fuseDelay;
    [SerializeField] public float blastForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(FuseCoroutine());
    }

    private IEnumerator FuseCoroutine()
    {
        yield return new WaitForSeconds(fuseDelay);
        Detonate();
    }
    public void Detonate()
    {
        Debug.Log("Detonate called");
        if (explosionEffectPrefab != null)
        {
            GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(explosionEffect, 4f);
            Debug.Log("Effect spawned");
            ApplyForces();
        }
        Debug.Log("Grenade Destroyed");
        Destroy(gameObject, 1f);
    }

    void ApplyForces()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearbyObj in colliders)
        {
            Rigidbody nearbyRb = nearbyObj.GetComponent<Rigidbody>();
            if (nearbyRb != null)
            {
                nearbyRb.GetComponent<IDamageAble>()?.TakeDamage((uint)blastForce);
                nearbyRb.AddExplosionForce(blastForce, transform.position, blastRadius);
            }
        }
    }
}

