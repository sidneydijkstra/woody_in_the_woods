using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour {

    public GameObject destroyObject;
    public int maxResources;
    public int resourcesPerHit;

    private int _currentResources;

    // tree branch
    private GameObject _collectPoint;

	void Start () {
        _collectPoint = this.transform.FindChild("collect_point").gameObject;
        _currentResources = maxResources;
	}
	
	void Update () {
	}

    // farm resource
    public void hitResource() {
        _currentResources -= resourcesPerHit;
        if (_currentResources <= 0) {
            GameObject newTree = Instantiate(destroyObject, this.transform.position, this.transform.rotation) as GameObject;

            int childCount = this.transform.childCount;
            Transform[] children = new Transform[childCount];
            for (int i = 0; i < childCount; i++){
                children[i] = this.transform.GetChild(i);
            }
            for (int i = 0; i < childCount; i++){
                children[i].parent = newTree.transform;
            }


            // play sound
            GameObject.FindGameObjectWithTag("AUDIOCONTROLLER").GetComponent<AudioController>().playAudio("hakken");

            Destroy(this.gameObject);
        }
    }

    // get resource collect point
    public GameObject getCollectPoint() {
        return _collectPoint;
    }
}
