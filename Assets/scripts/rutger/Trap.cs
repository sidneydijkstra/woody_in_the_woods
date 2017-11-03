using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
	public int difficulty;
	private float timer;
	private GameObject enemy;
	// Use this for initialization
	void Start () {
		difficulty = 2;
		timer = 12345.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer != 12345.0f) {
			Damage(enemy);
		}
	}

	void Damage(GameObject en)
	{
		timer -= Time.deltaTime;
		if (difficulty != 0) {
			en.GetComponent<EnemyController>().removeHealth(10 * difficulty);
		}
		if (timer > 100.0f) {
			timer = 0.0f;
		}
		if (difficulty == 2) {
			enemy.GetComponent<EnemyController>().setDestination(this.transform.position);
			difficulty = 0;
			timer = 10.0f;
		} else if (timer <= 0) {
			enemy.GetComponent<EnemyController>().disableDecoy();
			Destroy(this.gameObject);
			Destroy(this);
		} 
		difficulty = 0;
	}

	void OnCollisionEnter(Collision collision)
  	  {
    	     if (collision.gameObject.tag == "Enemy" && enemy == null)
	     {
	     	enemy = collision.gameObject;
	     	Damage(enemy);
	    }
	}
}
