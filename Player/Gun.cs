using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool IsReloading => _isReloading;
    public int CurrentAmmo => _currentAmmo;
    public GunData GunData => _gunData;

    [SerializeField] GunData _gunData;
    [SerializeField] Transform _shootingLocation;

    private int _currentAmmo;
    private bool _isReloading;

    public void DecrementCurrentAmmo()
    {
        _currentAmmo--;
    }

    public void ReloadWeapon()
    {
        _currentAmmo = _gunData.ClipSize;
    }

    public void SetIsReloading(bool val) => _isReloading = val;
}
