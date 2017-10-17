using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeController : MonoBehaviour {

    private GameObject[] _resourceObjects;

	void Start () {
        _resourceObjects = GameObject.FindGameObjectsWithTag("Resource");
	}
	
	void Update () {


        RaycastHit hit;
        Physics.Raycast(this.transform.position, this.transform.forward, out hit, 1);
        if (hit.collider != null) {
            print(hit.collider.name);
        }
        for (int i = 0; i < _resourceObjects.Length; i++){
            if (_resourceObjects[i] == null) {
                _resourceObjects = GameObject.FindGameObjectsWithTag("Resource");
                break;
            }

            if (hit.collider != null && hit.collider.CompareTag("Resource")){
                float dis = Vector3.Distance(this.transform.position, _resourceObjects[i].GetComponent<ResourceController>().getCollectPoint().transform.position);
                if (dis < 2 && this.GetComponent<AccelerometerController>().isForwardAndLeftAcceleration() || dis < 2 && Input.GetKeyDown(KeyCode.Space)){
                    _resourceObjects[i].GetComponent<ResourceController>().hitResource();
                }
                this.GetComponent<PlayerCombatController>().canTrowAxe(false);
                //print("false");
            }
            else{
                //print("true");
                this.GetComponent<PlayerCombatController>().canTrowAxe(true);
            }

            
        }
	}
}
