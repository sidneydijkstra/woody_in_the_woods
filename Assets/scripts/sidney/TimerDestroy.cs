using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour {

    public GameObject spawnOnDestroy = null;
    public bool timerOnFunction = false;
    public float delay = 0f;
    public bool makeSmallAfterTimer = false;

    private bool _start = false;
    private bool _done = false;
    private float _timer = 0f;

	void Start () {
        // start timer id timer on function is false
        if (!timerOnFunction) {
            _timer = Time.time + delay;
            _start = true;
        }
	}
	
	void Update () {
        // destroy timer
        if (_start && Time.time >= _timer) {
            // spawn object on destroy or make small
            if (spawnOnDestroy != null){
                Instantiate(spawnOnDestroy, this.transform.position, Quaternion.identity);
            }

            // if not make small destroy this
            if (!makeSmallAfterTimer) {
                Destroy(this.gameObject);
            }

            // set done
            _done = true;
        }

        // if done and make small make the object small
        if (_done && makeSmallAfterTimer) {
            // make objects small
            this.transform.localScale = Vector3.Slerp(this.transform.localScale, Vector3.zero, 5f * Time.deltaTime);

            // if object is 0,0,0 size destroy it
            if (this.transform.localScale == Vector3.zero) {
                Destroy(this.gameObject);
            }
        }
	}

    // function to start the timer
    public void startTimer() {
        _start = true;
    }
}
