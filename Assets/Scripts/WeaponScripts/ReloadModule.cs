using UnityEngine;
using System.Collections;

public class ReloadModule : MonoBehaviour, IWeaponModule
{
    private WeaponData _data;
    private AmmoModule _ammo;
    public bool isReloading { get; private set; }
    public void Initialize(WeaponData data) => _data = data;
    private void Awake()
    {
        _ammo = GetComponent<AmmoModule>();
    }
    public void startReload()
    {
        if (isReloading || _ammo.IsClipFull || !_ammo.HasReserve) return;
        StartCoroutine(ReloadCoroutine());
    }   
    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        UIManager.Instance?.showReload("Reloading...");
        yield return new WaitForSeconds(_data.reloadSpeed);
        _ammo.Reload();
        isReloading = false;
        UIManager.Instance?.hideReload();
    }
}
