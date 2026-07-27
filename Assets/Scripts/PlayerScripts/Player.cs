using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageAble
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private CharacterStats baseStats;
    private RuntimeStats playerStats;
    void Start()
    {
        if (baseStats == null)
        {
            baseStats = GetComponent<CharacterStats>();
        }
        playerStats = new RuntimeStats(baseStats);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(uint damage)
    {
        return;
    }
}
