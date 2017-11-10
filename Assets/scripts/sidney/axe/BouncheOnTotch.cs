using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncheOnTotch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    private void OnCollisionEnter(Collision col){
        if (col.gameObject.CompareTag("TROWOBJECT")) {
            col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(4, this.transform.position, 4, 5);
        }
    }
}
