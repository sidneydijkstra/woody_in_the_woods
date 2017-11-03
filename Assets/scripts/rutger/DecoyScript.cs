using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyScript : MonoBehaviour {
	private bool explosive = true;
	private float timer;
	public float difficulty;
	// Use this for initialization
	void Start () {
		timer = 10.0f;	
	}
	
	// Update is called once per frame
	void Update () {
		if (difficulty == 2) {
			explosive = true;
		} else if (difficulty == 1) {
			explosive = false;
		}
		timer -= Time.deltaTime;
		GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Enemy");
		    foreach (GameObject child in allObjects) {
		        float dist = (transform.position - child.transform.position).magnitude;
		        if (dist < 15.0f) {
		        		print("hello");
			         child.GetComponent<EnemyController>().setDestination(this.transform.position);
		        }
		    }
		if (timer < 0) {
			if (explosive) {
				explode();
			} 
			// allObjects = GameObject.FindGameObjectsWithTag("Enemy");
				    foreach (GameObject child in allObjects) {
				        float dist = (transform.position - child.transform.position).magnitude;
				        if (dist < 15.0f) {
					         child.GetComponent<EnemyController>().disableDecoy();
				        }
				    }
			Destroy(this.gameObject);
		}
	}

	private void explode() {
		 GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Enemy");
		    foreach (GameObject child in allObjects) {
		        float dist = (transform.position - child.transform.position).magnitude;
		        if (dist < 5.0f) {
		        	       float force = 1500.0f;
		                     Vector3 dir = child.transform.position - this.transform.position;
			         dir = dir.normalized;
			         print(dir);
			         child.GetComponent<Rigidbody>().AddForce(dir*force);
			         //kill child
			    //     child.GetComponent<EnemyController>().removeHealth(40);
		        }
		    }
	}
}
