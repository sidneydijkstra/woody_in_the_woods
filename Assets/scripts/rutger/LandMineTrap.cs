using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineTrap : MonoBehaviour {
	Transform[] array;
	public bool canExplode;
	public int difficulty;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void explode() {
		 GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Enemy");
		    foreach (GameObject child in allObjects) {
		        float dist = (transform.position - child.transform.position).magnitude;
		        if (dist < 5.0f * difficulty) {
		        	       float force = 750.0f;
		                     Vector3 dir = child.transform.position - transform.position;
			         dir = dir.normalized;
			         print(dir);
			         child.GetComponent<Rigidbody>().AddForce(dir*force);
			         //kill child
		        }
		    }
	}

void OnCollisionEnter(Collision collision)
    {
    	     if (collision.gameObject.name != "Terrain")
	     {
	     	explode();
	     	Destroy(this.gameObject);
	    }
}

}
