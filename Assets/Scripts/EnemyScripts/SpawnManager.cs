using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Enemy zombiePrefab;
    [SerializeField] private float spawnRadius = 9f;
    [SerializeField] private int enemiesPerWave = 3;
    [SerializeField] private float timeBetweenSpawn = 0.5f;
    private float timeSinceLastSpawn;

    private IObjectPool<Enemy> zombiePool;
    private int wave = 0;

    private void Awake()
    {
        zombiePool = new ObjectPool<Enemy>(CreateEnemy);
    }
    void Start() => StartCoroutine(waveLoop());

    // Update is called once per frame
    private IEnumerator waveLoop()
    {
        while (true)
        {
            wave++;
            yield return StartCoroutine(SpawnWave(enemiesPerWave + wave * 2));
            yield return new WaitForSeconds(timeBetweenSpawn);

        }
    }
    private IEnumerator SpawnWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(zombiePrefab, GenerateRandomPos(), zombiePrefab.transform.rotation);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    private Vector3 GenerateRandomPos()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float radius = spawnRadius;

        return new Vector3(
            Mathf.Cos(angle) * radius,
            1f,
            Mathf.Sin(angle) * radius
        );
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(zombiePrefab);
        return enemy;

    }
}
