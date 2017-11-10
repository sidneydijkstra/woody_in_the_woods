using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeController : MonoBehaviour {

    public GameObject axeDisplay;

    private GameObject[] _resourceObjects;

    private float currentResources;

	void Start () {
        _resourceObjects = GameObject.FindGameObjectsWithTag("Resource");
	}
	
	void Update () {
        axeDisplay.transform.localEulerAngles = Vector3.Slerp(axeDisplay.transform.position, new Vector3(0, axeDisplay.transform.position.x, axeDisplay.transform.position.z), 2 * Time.deltaTime);//43

        RaycastHit hit;
        Physics.Raycast(this.transform.position, this.transform.forward, out hit, 1);
        for (int i = 0; i < _resourceObjects.Length; i++){
            if (_resourceObjects[i] == null) {
                _resourceObjects = GameObject.FindGameObjectsWithTag("Resource");
                break;
            }

            if (hit.collider != null && hit.collider.CompareTag("Resource")){
                float dis = Vector3.Distance(this.transform.position, _resourceObjects[i].GetComponent<ResourceController>().getCollectPoint().transform.position);
                if (dis < 2 && this.GetComponent<AccelerometerController>().isForwardAndLeftAcceleration() || dis < 2 && Input.GetKeyDown(KeyCode.Space)){
                    _resourceObjects[i].GetComponent<ResourceController>().hitResource();
                    currentResources += _resourceObjects[i].GetComponent<ResourceController>().resourcesPerHit;
                    axeDisplay.transform.localEulerAngles = new Vector3(43, this.transform.position.x, this.transform.position.z);
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

    // get resources
    public float getCurrentResources() {
        return currentResources;
    }

    // remove resources
    public void removeResources(float amount) {
        currentResources -= amount;
    }

    // can remove resources
    public bool canRemoveResources(float amount) {
        if ((currentResources - amount) >= 0) {
            return true;
        }
        return false;
    }

}
