using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HitscanShooter : ShootModule
{
    private Camera playerCam;
    private WeaponData _data;
    public event Action<RaycastHit> OnHit;

    private void Awake()
    {
        canShoot = true;
        playerCam = Camera.main;
    } 
    public void Initialize(WeaponData data) => _data = data;
    public override void Shoot()
    {
        Vector3 spread = GetSpread();
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward + spread);
        Debug.DrawRay(ray.origin, 10f * ray.direction, Color.red);
        if (Physics.Raycast(ray, out var hit, _data.range, _data.hitLayer))
        {
            hit.collider.GetComponent<IDamageAble>()?.TakeDamage(_data.damage);
            OnHit?.Invoke(hit);
            Debug.DrawLine(ray.origin, hit.point, Color.green);
        }
    }
    private Vector3 GetSpread()
    {
        float spread = _data.bulletSpread * Mathf.Deg2Rad;
        return new Vector3(
            Random.Range(-spread, spread),
            Random.Range(-spread, spread),
            0f
        );
    }
}
