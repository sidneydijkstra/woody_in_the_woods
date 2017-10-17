using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour {

    [Header("Attack Config")]
    public float attackRange = 0.7F;
    public float damage = 10F;

    private GameObject _player;
    
    void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
        enemyAttack();
	}

    private void enemyAttack() {
        float dis = Vector3.Distance(this.transform.position, _player.transform.position);
        if (dis <= attackRange) {
            _player.GetComponent<PlayerController>().removeHealth(damage);
        }
    }
}
