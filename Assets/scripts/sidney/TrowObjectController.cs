using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrowObjectController : MonoBehaviour {
    
	void Start () {
		
	}
	
	void Update () {
        this.transform.eulerAngles += new Vector3(this.transform.eulerAngles.x + 60 * Time.deltaTime, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
	}
}
