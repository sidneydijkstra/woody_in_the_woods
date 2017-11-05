using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentController : MonoBehaviour {
	public GameObject bullet;
	private float timer;
	public int difficulty;
	// Use this for initialization
	void Start () {
		//timer = 0.5f;
		timer = 0.75f / difficulty;
	}
	
	// Update is called once per frame
	void Update () {
		 timer -= Time.deltaTime;
		 GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Enemy");
		 float closest = Mathf.Infinity;
		 GameObject closestObject = this.gameObject;
		    foreach (GameObject child in allObjects) {
		        float dist = (transform.position - child.transform.position).magnitude;
		        if (dist < 7.5f * difficulty) {
		        	if (dist < closest) {
		        		closestObject = child;
		        		closest = dist;
		        	}
		        	if (timer < 0) {
		       		GameObject bulletIns = Instantiate(bullet, this.transform.position, Quaternion.identity);
		       		bulletIns.GetComponent<Bullet>().direction = child.transform.position;
		       		timer = 0.5f;
		       		return;
		       	}
		        }
		}
		Vector3 targetDir = closestObject.transform.position - transform.position;
	 	float step = 1.0f * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f) + new Vector3(0,90,0);
		        transform.rotation = Quaternion.LookRotation(newDir);
	}
	
}
