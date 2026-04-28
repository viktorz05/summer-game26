using System.Collections;
using UnityEngine;

// Attach this to the target prefab. WeaponScript will call OnHit() when the raycast hits it.
public class Target : MonoBehaviour
{
    [Tooltip("Delay before the target respawns if ShootingRange manager is unavailable.")]
    public float localRespawnDelay = 1f;

    [Tooltip("If true, attempt to use ShootingRange.Instance to respawn at a location inside the spawn area.")]
    public bool useShootingRange = true;

    // Called when the target is hit
    public void OnHit()
    {
        // Immediately disappear
        gameObject.SetActive(false);

        // Prefer manager if available
        if (useShootingRange && ShootingRange.Instance != null)
        {
            ShootingRange.Instance.ScheduleRespawn(gameObject, ShootingRange.Instance.targetRespawnDelay);
            return;
        }

        // Fallback: self-respawn after local delay
        StartCoroutine(LocalRespawn(localRespawnDelay));
    }

    IEnumerator LocalRespawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (gameObject != null)
            gameObject.SetActive(true);
    }
}