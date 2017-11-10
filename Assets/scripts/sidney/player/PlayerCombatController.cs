using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour {

    [Header("Axe Config")]
    public GameObject axeDisplay;
    public GameObject axe;
    public Transform adjectPoint;

    [Header("Gun Config")]
    public GunConfig gun;

    private bool trowAxe;

    // axe display and delay
    private float axeTimer;

    // gun timer
    private float fireTimer;
    private bool isDisplaying = false;

    void Start () {
        trowAxe = true;
	}
	
	void Update () {
        axeUpdate();
        gunUpdate();
	}

    private void axeUpdate() {
        if (axeTimer > Time.time) {
            return;
        }else { 
}

        if (!isDisplaying) {
            isDisplaying = true;
            axeDisplay.SetActive(true);
        }

        // on trow
        if (this.GetComponent<AccelerometerController>().isForwardAcceleration() && trowAxe || Input.GetKeyDown(KeyCode.Mouse1) && trowAxe) {
            GameObject obj = Instantiate(axe, adjectPoint.position, this.transform.rotation) as GameObject;

            obj.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z);
            obj.GetComponent<Rigidbody>().AddForce(obj.transform.forward * 25, ForceMode.Impulse);
            axeTimer = Time.time + 1.2f;

            isDisplaying = false;
            axeDisplay.SetActive(false);
        }
    }

    private void gunUpdate() {

        // rotate gun when shoot
        Transform gunT = gun.gameObject.transform;
        if (gunT.transform.position.z != 0f) {
            gunT.transform.localEulerAngles = Vector3.Lerp(gunT.transform.localEulerAngles, new Vector3(gunT.transform.localEulerAngles.x, gunT.transform.localEulerAngles.y, 0), 5 * Time.deltaTime);
        }

        // on reload
        if (!gun.reloading() && Input.GetKeyDown(KeyCode.R) || !gun.reloading() && Input.GetKeyDown(KeyCode.Joystick1Button1)) {
            gun.reload();
        }

        // on shoot
        if (Input.GetKey(KeyCode.Mouse0) && gun.canShoot() && fireTimer < Time.time || Input.GetKey(KeyCode.Joystick1Button0) && gun.canShoot() && fireTimer < Time.time) {
            gun.shoot();
            fireTimer = Time.time + (gun.fireRate/1000);

            gunT.transform.localEulerAngles = new Vector3(gunT.transform.localEulerAngles.x, gunT.transform.localEulerAngles.y, gunT.transform.localEulerAngles.z + 10);

            GameObject obj = Instantiate(gun.bullet, gun.barrel.position, gun.barrel.rotation) as GameObject;
            obj.GetComponent<Rigidbody>().AddForce(gun.transform.right * 25, ForceMode.Impulse);

            gun.GetComponent<AudioSource>().Play();
        }
    }

    public void canTrowAxe(bool _bool) {
        trowAxe = _bool;
    }

}
