using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunConfig : MonoBehaviour {

    [Header("Gun Config")]
    public float maxClipAmmo = 0;
    public float reloadTime = 0;
    public float damage = 0;
    public float fireRate = 0;

    public Transform barrel = null;
    public GameObject bullet = null;

    private float _currentClipAmmo = 0;
    private float _reloadTimer = 0;



    void Start () {
        _currentClipAmmo = maxClipAmmo;
	}

    public bool canShoot() {
        if (_currentClipAmmo <= 0) {
            if (!reloading()) {
                reload();
            }
        }
        if (_reloadTimer <= Time.time && _currentClipAmmo > 0) {
            return true;
        }
        return false;
    }

    public void shoot() {
        if (canShoot()) {
            _currentClipAmmo--;
        }
    }

    public bool reloading() {
        if (_reloadTimer <= Time.time){
            return false;
        }
        return true;
    }

    public void reload() {
        print("reloading");
        _currentClipAmmo = maxClipAmmo;
        _reloadTimer = Time.time + reloadTime;
    }

}
