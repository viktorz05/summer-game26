using UnityEngine;

public abstract class ShootModule : MonoBehaviour
{
    public bool canShoot;
    public virtual void Shoot() { }
}
