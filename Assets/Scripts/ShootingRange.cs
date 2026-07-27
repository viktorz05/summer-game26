using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRange : MonoBehaviour
{
    public static ShootingRange Instance { get; private set; }

    [Tooltip("Prefab used for targets. Preferably tagged 'Target'")]
    public GameObject targetPrefab;

    [Tooltip("Number of targets to spawn in the area")]
    public int initialTargetCount = 5;

    [Tooltip("Size of the rectangular spawn area: X = width, Y = depth (Z). Center is spawnAreaCenter or this transform")]
    public Vector2 spawnAreaSize = new Vector2(8f, 4f);

    [Tooltip("Optional transform used as spawn area center. If null, this GameObject's transform is used.")]
    public Transform spawnAreaCenter;

    [Tooltip("Seconds to wait before reactivating a hit target.")]
    public float targetRespawnDelay = 1f;

    [Tooltip("Rotation speed applied to active targets")]
    public float rotateSpeed = 50f;

    // internal list of spawned/managed targets
    private readonly List<GameObject> spawnedTargets = new List<GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple ShootingRange instances found. Destroying duplicate.");
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        if (spawnAreaCenter == null)
            spawnAreaCenter = transform;

        if (targetPrefab == null)
        {
            // fallback: try to find an existing scene object named "Target"
            GameObject existing = GameObject.Find("Target");
            if (existing != null)
            {
                spawnedTargets.Add(existing);
            }
            else
            {
                Debug.LogWarning("ShootingRange: No targetPrefab assigned and no scene object named 'Target' found. Assign a prefab to spawn targets.");
                return;
            }
        }

        // If we have a prefab, spawn a set of targets
        if (targetPrefab != null)
        {
            for (int i = 0; i < initialTargetCount; i++)
            {
                Vector3 spawnPos = GetRandomSpawnPosition();
                GameObject go = Instantiate(targetPrefab, spawnPos, Quaternion.identity);
                // normalize the name to "Target" so old checks still work
                if (go.name.EndsWith("(Clone)"))
                    go.name = go.name.Replace("(Clone)", "").Trim();
                spawnedTargets.Add(go);
            }
        }
    }

    void Update()
    {
        // Rotate active targets
        for (int i = 0; i < spawnedTargets.Count; i++)
        {
            var t = spawnedTargets[i];
            if (t != null && t.activeInHierarchy)
                t.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.Self);
        }
    }

    // Backwards-compatible public API
    public void HandleTargetHit(GameObject targetGo)
    {
        if (targetGo == null)
            return;

        // Deactivate and schedule respawn at current manager
        targetGo.SetActive(false);
        StartCoroutine(RespawnTarget(targetGo, targetRespawnDelay));
    }

    // Allows other classes (or Target component) to schedule a respawn with a custom delay
    public void ScheduleRespawn(GameObject targetGo, float delay)
    {
        if (targetGo == null)
            return;

        targetGo.SetActive(false);
        StartCoroutine(RespawnTarget(targetGo, delay));
    }

    IEnumerator RespawnTarget(GameObject targetObj, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (targetObj == null)
            yield break;

        // place at a new random position inside the area before re-enabling
        targetObj.transform.position = GetRandomSpawnPosition();
        targetObj.SetActive(true);
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 center = spawnAreaCenter != null ? spawnAreaCenter.position : transform.position;
        float halfX = spawnAreaSize.x * 0.5f;
        float halfZ = spawnAreaSize.y * 0.5f;
        float rx = Random.Range(-halfX, halfX);
        float rz = Random.Range(-halfZ, halfZ);
        // keep Y the same as center to avoid changing vertical placement
        return new Vector3(center.x + rx, center.y, center.z + rz);
    }
}