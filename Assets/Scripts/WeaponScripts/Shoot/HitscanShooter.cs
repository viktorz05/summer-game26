using System;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class HitscanShooter : ShootModule, IWeaponModule
{
    private Camera playerCam;
    private WeaponData _data;
    public event Action<RaycastHit> OnHit;
    [SerializeField] private VisualEffect muzzleFlashVFX;

    private void Awake()
    {
        canShoot = true;
        playerCam = Camera.main;
    } 
    public void Initialize(WeaponData data) => _data = data;
    public override void Shoot()
    {
        Vector3 direction = GetSpreadDirection();
        Ray ray = new Ray(playerCam.transform.position, direction);
        Debug.DrawRay(ray.origin, 10f * ray.direction, Color.red, 2f);
        muzzleFlashVFX.Play();
        if (Physics.Raycast(ray, out var hit, _data.range, _data.hitLayer))
        {
            hit.collider.GetComponent<IDamageAble>()?.TakeDamage(_data.damage);
            OnHit?.Invoke(hit);
            Debug.DrawLine(ray.origin, 10f * hit.point, Color.green, 2f);
        }
    }
    private Vector3 GetSpreadDirection()
    {
        Vector3 baseDirection = playerCam.transform.forward;

        if (_data.bulletSpread <= 0f)
            return baseDirection;

        float spread = _data.bulletSpread * Mathf.Deg2Rad;
        Vector3 randomSpread = new Vector3(
            Random.Range(-spread, spread),
            Random.Range(-spread, spread),
            0f
        );

        // Apply spread by rotating the base direction
        Quaternion spreadRotation = Quaternion.Euler(randomSpread.x * Mathf.Rad2Deg, randomSpread.y * Mathf.Rad2Deg, 0f);
        return (spreadRotation * baseDirection).normalized;
    }
}
