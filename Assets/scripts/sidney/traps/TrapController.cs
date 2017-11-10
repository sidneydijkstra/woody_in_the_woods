using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour {

    private int rank = 0;

    private GameObject[] _enemys;
    private int _hitsToGive = 0;

    private float _hitTimer = 0;
    private int _givenHits = 0;

	void Start () {
        this.setRank(2);
    }
	
	void Update () {
        // get enemys
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");

        // check for damage
        if (_hitTimer < Time.time) {
            for (int i = 0; i < _enemys.Length; i++){
                if (Vector3.Distance(this.transform.position, _enemys[i].transform.position) < 2f) {
                    _enemys[i].GetComponent<EnemyController>().removeHealth(20);
                    _hitTimer = Time.time + 0.7f;
                    _givenHits++;
                    break;
                }
            }
        }

        // destoy on max hits
        if (_givenHits >= _hitsToGive) {
            Destroy(this.gameObject);
        }
	}

    public void setRank(int _rank) {
        rank = _rank;
        if (rank <= 0) {
            _hitsToGive = 20;
        }

        if (rank == 1) {
            _hitsToGive = 50;
        }


        if (rank >= 2) {
            _hitsToGive = 80;
        }
    }
}
