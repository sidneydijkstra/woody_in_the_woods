using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    public LineRenderer trail;
    public GameObject head;

    private int rank = 0;

    private GameObject[] _enemys;
    private int _hitsToGive = 0;
    private float _damage = 20;
    private float _range = 10;

    private float _hitTimer = 0;
    private int _givenHits = 0;

    private PlayerAxeController _pAxeController;

    // trail vars
    private float _trailTimer;

	void Start () {
        this.setRank(0);
    }
	
	void Update () {
        // get enemys
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");

        // check for damage
        if (_hitTimer < Time.time) {
            for (int i = 0; i < _enemys.Length; i++){
                if (Vector3.Distance(this.transform.position, _enemys[i].transform.position) < _range) {
                    // attack enemy
                    _enemys[i].GetComponent<EnemyController>().removeHealth(_damage);

                    // set trail
                    trail.SetPosition(0, trail.gameObject.transform.position);
                    trail.SetPosition(1, _enemys[i].transform.position);
                    trail.gameObject.SetActive(true);

                    // rotate head
                    head.transform.LookAt(_enemys[i].transform.position);

                    // set trail timer
                    _trailTimer = Time.time + 0.2f;

                    // set shoot timer
                    _hitTimer = Time.time + 1f;
                }
            }
        }

        // destroy trail
        if (trail.gameObject.activeSelf && _trailTimer < Time.time) {
            trail.gameObject.SetActive(false);
        }

        // destoy on max hits
        if (_givenHits >= _hitsToGive) {
            Destroy(this.gameObject);
        }
	}

    public void setRank(int _rank) {
        rank = _rank;
        if (rank <= 0) {
            _hitsToGive = 50;
            _damage = 20;
            _range = 18;
        }

        if (rank >= 1) {
            _hitsToGive = 120;
            _damage = 35;
            _range = 25;
        }
    }
}
