using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour, IDetoneable, IThrowable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Rigidbody rb;
    public float launchAngle;
    [SerializeField] public float launchVelocity;
    [SerializeField] public float damage;

    void Start()
    {
       rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    public IEnumerator Detonate()
    {
        yield return null;
    }
    public void Throw()
    {

    }
}
