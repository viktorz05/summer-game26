using System;
using UnityEngine;

public class AmmoModule : MonoBehaviour, IWeaponModule
{
    public event Action OnAmmoChanged;
    public event Action OnEmptyClip;

    private uint _currentAmmo;
    private uint _reserveAmmo;
    private WeaponData _data;
    public bool HasAmmo => _currentAmmo > 0;
    public bool HasReserve => _reserveAmmo > 0;
    public bool IsClipFull => _currentAmmo >= _data.magazineSize;
    public uint CurrentAmmo => _currentAmmo;
    public uint ReserveAmmo => _reserveAmmo;

    public void Initialize(WeaponData data)
    {
        _data = data;
        _currentAmmo = data.magazineSize;
        _reserveAmmo = data.reserveSize;
        UIManager.Instance?.setAmmo((int)_currentAmmo, (int)_data.magazineSize);
    }

    public void ConsumeRound()
    {
        if (_currentAmmo > 0)
        {
            _currentAmmo--;
            UIManager.Instance?.setAmmo((int)_currentAmmo, (int)_data.magazineSize);
            OnAmmoChanged?.Invoke();
        }
        OnEmptyClip?.Invoke();
    }

    public void Reload()
    {
        uint needed = _data.magazineSize - _currentAmmo;
        uint toLoad = (uint)Mathf.Min(needed, _reserveAmmo);
        _currentAmmo += toLoad;
        _reserveAmmo -= toLoad;

        UIManager.Instance?.setAmmo((int)_currentAmmo, (int)_data.magazineSize);
        OnAmmoChanged?.Invoke();
    }
}
