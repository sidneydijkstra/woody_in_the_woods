using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour {

    [Header("Axe Config")]
    public GameObject axe;
    public Transform adjectPoint;

    [Header("Gun Config")]
    public GunConfig gun;

    private bool trowAxe;

    void Start () {
        trowAxe = true;
	}
	
	void Update () {
        axeUpdate();
        gunUpdate();
	}

    private void axeUpdate() {
        if (this.GetComponent<AccelerometerController>().isForwardAcceleration() && trowAxe || Input.GetKey(KeyCode.Mouse1) && trowAxe) {
            GameObject obj = Instantiate(axe, adjectPoint.position, this.transform.rotation) as GameObject;

            obj.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z);
            obj.GetComponent<Rigidbody>().AddForce(obj.transform.forward * 25, ForceMode.Impulse);
        }
    }

    private void gunUpdate() {
        if (!gun.reloading() && Input.GetKeyDown(KeyCode.R) || !gun.reloading() && Input.GetKeyDown(KeyCode.Joystick1Button1)) {
            gun.reload();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && gun.canShoot() || Input.GetKeyDown(KeyCode.Joystick1Button0) && gun.canShoot()) {
            gun.shoot();

            GameObject obj = Instantiate(gun.bullet, gun.barrel.position, gun.barrel.rotation) as GameObject;
            obj.GetComponent<Rigidbody>().AddForce(gun.transform.right * 25, ForceMode.Impulse);
        }
    }

    public void canTrowAxe(bool _bool) {
        trowAxe = _bool;
    }

}
