using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrowController : MonoBehaviour {

    public GameObject trowObject;
    public Transform adjectPoint;

	void Start () {
		
	}
	
	void Update () {
        if (this.GetComponent<AccelerometerController>().isForwardAcceleration() || Input.GetKey(KeyCode.Mouse0)) {
            GameObject obj = Instantiate(trowObject, adjectPoint.position, this.transform.rotation) as GameObject;
            obj.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z);
            obj.GetComponent<Rigidbody>().AddForce(obj.transform.forward * 25, ForceMode.Impulse);
        }
	}
}
