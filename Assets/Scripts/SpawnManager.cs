using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Zombie;
    private float spawnRadius = 9f;

    void Start()
    {
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnWave()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(Zombie, GenerateRandomPos(), Zombie.transform.rotation);
        }
    }

    private Vector3 GenerateRandomPos()
    {
        float posX = Random.Range(-spawnRadius, spawnRadius);
        float posZ = Random.Range(-spawnRadius, spawnRadius);

        Vector3 spawnPos = new Vector3(posX, 1f, posZ);

        return spawnPos;
    }
}
