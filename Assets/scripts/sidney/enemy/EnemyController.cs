using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    
    [Header("Movement Config")]
    public float jumpHeight = 3F;
    public GameObject animal = null;

    [Header("Health Config")]
    public float maxHealth = 40F;
    public float hitDelay = 0.7F;

    [Header("Spawn Config")]
    public float spawnChange = 100;



    // movement vars
    private GameObject animalObject = null;
    private NavMeshAgent agent;
    private Rigidbody rig;
    private GameObject _player;
    private bool decoy;
    private Vector3 destination;

    // health vars
    private float currentHealth = 0F;
    private float hitTimer = 0F;

    private bool dead = false;

    void Start () {
        // get objects and components
        agent = this.GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        rig = this.GetComponent<Rigidbody>();

        // movement
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        agent.enabled = false;
        animalObject = Instantiate(animal, this.transform.position, Quaternion.identity) as GameObject;
        animalObject.transform.SetParent(this.transform);

        // health
        this.setHealth(maxHealth);
	}
	
	void Update () {
        enemyMovement();
	}

    private void enemyMovement() {
        // send ray to check if ground
        float size = this.GetComponent<BoxCollider>().size.x / 2;
        RaycastHit hit;
        Physics.Raycast(this.transform.position, -Vector3.up, out hit, size + 0.1f);

        if (hit.collider != null){
            // set rotation
            agent.enabled = true;
            if (decoy) {
                agent.SetDestination(destination);
            } else {
                agent.SetDestination(_player.transform.position);
            }
            this.transform.LookAt(agent.destination);

            // do jump
            agent.enabled = false;
            rig.velocity = (this.transform.up + this.transform.forward) * jumpHeight;
        }else if (!agent.enabled && rig.velocity == Vector3.zero){
            // set normal movement
            agent.enabled = true;
             if (decoy) {
                agent.SetDestination(destination);
            } else {
                agent.SetDestination(_player.transform.position);
            }
        }

        // set animal rotation
        animalObject.transform.LookAt(_player.transform.position);
        animalObject.transform.localEulerAngles = new Vector3(0, animalObject.transform.localEulerAngles.y, animalObject.transform.localEulerAngles.z);
    }

    // bullet trigger
    private void OnTriggerEnter(Collider col){
        if (col.CompareTag("Bullet") && hitTimer <= Time.time) {
            hitTimer = hitDelay + Time.time;
            this.removeHealth(col.GetComponent<BulletController>().getDamage());
        }
    }

    // health function
    public void addHealth(float _amount) {
        currentHealth += _amount;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    // set health
    public void setHealth(float _amount){
        currentHealth = _amount;
        if (currentHealth < 0) {
            currentHealth = 0;
        }
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    // remove health
    public void removeHealth(float _amount){
        currentHealth -= _amount;
        
        GameObject[] bodyparts = this.getBodyParts();
        for (int i = 0; i < bodyparts.Length; i++){
            bodyparts[i].GetComponent<MeshRenderer>().material.color = Color.red;
        }

        if (currentHealth < 0) {
            currentHealth = 0;
            this.expload();
        }
    }

    public void setDestination(Vector3 dest) {
        destination = dest;
        decoy = true;
    }

    public void disableDecoy() {
        decoy = false;
    }


    // get current helth
    public float getCurrentHealth() {
        return currentHealth;
    }

    // kill enemy
    public void kill() {
        dead = true;
    }

    // destroy enemy
    public void destroy() {
        Destroy(this.gameObject);
    }

    // get if enemy is dead
    public bool isDead() {
        return dead;
    }

    // make enemy expload
    public void expload() {
        GameObject[] bodyparts = this.getBodyParts(); ;

        for (int i = 0; i < bodyparts.Length; i++){
            bodyparts[i].AddComponent<BoxCollider>();
            bodyparts[i].AddComponent<Rigidbody>();
            bodyparts[i].GetComponent<Rigidbody>().AddExplosionForce(4, this.transform.position, 4, 2, ForceMode.Impulse);
            bodyparts[i].transform.SetParent(this.transform.parent);
        }

        this.kill();
    }

    public GameObject[] getBodyParts() {
        GameObject animal = this.transform.GetChild(0).gameObject;
        int childcount = animal.transform.childCount;
        GameObject[] bodyparts = new GameObject[childcount];
        for (int i = 0; i < childcount; i++){
            bodyparts[i] = animal.transform.GetChild(i).gameObject;
        }
        return bodyparts;
    }

}
