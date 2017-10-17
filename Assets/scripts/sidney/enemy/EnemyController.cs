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



    // movement vars
    private GameObject animalObject = null;
    private NavMeshAgent agent;
    private Rigidbody rig;
    private GameObject _player;

    // health vars
    private float currentHealth = 0F;
    private float hitTimer = 0F;

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
            agent.SetDestination(_player.transform.position);
            this.transform.LookAt(agent.destination);

            // do jump
            agent.enabled = false;
            rig.velocity = (this.transform.up + this.transform.forward) * jumpHeight;
        }else if (!agent.enabled && rig.velocity == Vector3.zero){
            // set normal movement
            agent.enabled = true;
            agent.SetDestination(_player.transform.position);
        }

        // set animal rotation
        animalObject.transform.LookAt(_player.transform.position);
        animalObject.transform.localEulerAngles = new Vector3(0, animalObject.transform.localEulerAngles.y, animalObject.transform.localEulerAngles.z);
    }

    // bullet trigger
    private void OnTriggerEnter(Collider col){
        if (col.CompareTag("Bullet") && hitTimer <= Time.time) {
            print("hit");
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

    public void setHealth(float _amount){
        currentHealth = _amount;
        if (currentHealth < 0) {
            currentHealth = 0;
        }
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public void removeHealth(float _amount){
        currentHealth -= _amount;
        if (currentHealth < 0) {
            currentHealth = 0;
            this.kill();
        }
    }

    public float getCurrentHealth() {
        return currentHealth;
    }

    // if enemy dead
    public void kill() {
        Destroy(this.gameObject);
    }

}
