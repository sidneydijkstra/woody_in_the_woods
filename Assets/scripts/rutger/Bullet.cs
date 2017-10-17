using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public Vector3 direction;
	float timer;
	// Use this for initialization
	void Start () {
		timer = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0.0f) {
			Destroy(this.gameObject);
		}
		transform.position = Vector3.MoveTowards(transform.position, direction, 2.0f);
	}
	void OnCollisionEnter(Collision collision)
	{
		Destroy(this.gameObject);
	}
}
