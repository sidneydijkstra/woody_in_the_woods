using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckOnTotch : MonoBehaviour {

    public GameObject stuckObject;

	void Start () {
	}
	
	void Update () {
	}

    private void OnCollisionEnter(Collision col){
        if (col.gameObject.CompareTag("TROWOBJECT")) {
            Vector3 closestPointOnBounds = this.GetComponent<Collider>().ClosestPointOnBounds(col.transform.position);
            GameObject obj = Instantiate(stuckObject, closestPointOnBounds, col.transform.rotation) as GameObject;
            obj.transform.parent = this.transform;
            Destroy(col.gameObject);
        }
    }
}
