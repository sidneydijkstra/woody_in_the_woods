using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckAndKillOnTouch : MonoBehaviour {

    public GameObject stuckObject;

    private void OnTriggerEnter(Collider col){
        if (col.gameObject.CompareTag("TROWOBJECT")) {
            Vector3 closestPointOnBounds = this.GetComponent<BoxCollider>().ClosestPointOnBounds(col.transform.position);
            GameObject obj = Instantiate(stuckObject, closestPointOnBounds, col.transform.rotation) as GameObject;
            obj.transform.SetParent(this.transform);
            Destroy(col.gameObject);

            this.transform.parent.parent.GetComponent<EnemyController>().expload();

            // play sound
            GameObject.FindGameObjectWithTag("AUDIOCONTROLLER").GetComponent<AudioController>().playAudio("headshot");
        }
    }
}
