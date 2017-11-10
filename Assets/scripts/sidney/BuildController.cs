using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour {

    [Header("Build Objects Config")]
    public GameObject opTrap;
    public GameObject opTurret;
    public GameObject opMine;
    public GameObject opDecoy;

    private PlayerAxeController _pAxeController;

    void Start () {
        _pAxeController = this.GetComponent<PlayerAxeController>();
	}
	
	void Update () {

	}

    public void spawnTrap(int _rank) {
        float cost = 0;
        if (_rank == 0){
            cost = 80;
        }else if (_rank == 1){
            cost = 160;
        }else {
            cost = 350;
        }

        if (_pAxeController.canRemoveResources(cost)) {
            _pAxeController.removeResources(cost);
        }else{
            return;
        }

        // play sound
        GameObject.FindGameObjectWithTag("AUDIOCONTROLLER").GetComponent<AudioController>().playAudio("build");

        Vector3 spawnPos = this.transform.position + this.transform.forward * 2;

        RaycastHit hit;
        Physics.Raycast(spawnPos, Vector3.down, out hit);
   
        GameObject i = Instantiate(opTrap, hit.point, this.transform.rotation) as GameObject;
        i.GetComponent<TrapController>().setRank(_rank);
    }

    public void spawnMine() {
        float cost = 120;

        if (_pAxeController.canRemoveResources(cost)) {
            _pAxeController.removeResources(cost);
        }else{
            return;
        }

        // play sound
        GameObject.FindGameObjectWithTag("AUDIOCONTROLLER").GetComponent<AudioController>().playAudio("build");

        Vector3 spawnPos = this.transform.position + this.transform.forward * 2;

        RaycastHit hit;
        Physics.Raycast(spawnPos, Vector3.down, out hit);
   
        GameObject i = Instantiate(opMine, hit.point, this.transform.rotation) as GameObject;
    }

    public void spawnTurrent(int _rank) {
        float cost = 0;
        if (_rank == 0){
            cost = 250;
        }else if (_rank == 1){
            cost = 500;
        }

        if (_pAxeController.canRemoveResources(cost)) {
            _pAxeController.removeResources(cost);
        }else{
            return;
        }

        // play sound
        GameObject.FindGameObjectWithTag("AUDIOCONTROLLER").GetComponent<AudioController>().playAudio("build");

        Vector3 spawnPos = this.transform.position + this.transform.forward * 2;

        RaycastHit hit;
        Physics.Raycast(spawnPos, Vector3.down, out hit);
   
        GameObject i = Instantiate(opTurret, hit.point, this.transform.rotation) as GameObject;
        i.GetComponent<TurretController>().setRank(_rank);
    }

    /*
     
     **** trein ****
     - color traps

     **** school ****
     - make axe and color gun/axe
     - add axe display to playerCombatController and PlayerAxeController
     - add gun audio
     - add lars audio
     
     */

}
