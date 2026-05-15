using UnityEngine;

public class AmmoModule
{
    private uint _currentAmmo;
    private uint _reserveAmmo;
    private WeaponData _data;
    public bool hasAmmo => _currentAmmo > 0;
    public bool hasReserve => _reserveAmmo > 0;
    public bool isClipFull => _currentAmmo >= _data.magazineSize;
    public uint CurrentAmmo => _currentAmmo;
    public uint ReserveAmmo => _reserveAmmo;

    public void Initialize(WeaponData data)
    {
        _data = data;
        _currentAmmo = data.magazineSize;
        _reserveAmmo = data.reserveSize;
    }

    

}
