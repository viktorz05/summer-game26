using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Zombie;
    private float spawnRadius = 9f;
    private float speed = 3f;

    void Start()
    {
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {

        //Instantiate(Zombie, GenerateRandomPos(), Zombie.transform.rotation);
        Vector3 target = GenerateRandomPos();
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = GenerateRandomPos();
        }

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

        Vector3 spawnPos = new Vector3(posX, 0, posZ);

        return spawnPos;
    }
}
