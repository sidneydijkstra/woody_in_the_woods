using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour{
    private GameObject[] _enemys;

    void Start(){
    }

    void Update(){
        // get enemys
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");

        // check for exploasion
        for (int i = 0; i < _enemys.Length; i++){
            if (Vector3.Distance(this.transform.position, _enemys[i].transform.position) < 2f){
                for (int y = 0; y < _enemys.Length; y++){
                    if (Vector3.Distance(this.transform.position, _enemys[y].transform.position) < 6f){
                        _enemys[y].GetComponent<EnemyController>().expload();
                    }
                }
                Destroy(this.gameObject);
                break;
            }
        }
    }

}
