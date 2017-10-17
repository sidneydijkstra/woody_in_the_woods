using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour {

    public GameObject spawnOnDestroy = null;
    public bool timerOnFunction = false;
    public float delay = 0f;

    private bool _start = false;
    private float _timer = 0f;

	void Start () {
        if (!timerOnFunction) {
            _timer = Time.time + delay;
            _start = true;
        }
	}
	
	void Update () {
        if (_start && Time.time >= _timer) {
            if (spawnOnDestroy != null){
                Instantiate(spawnOnDestroy, this.transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
	}

    public void startTimer() {
        _start = true;
    }
}
