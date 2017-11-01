using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    [Header("Spawn Config")]
    public int SpawnMaxEntitys = 10;
    public float spawnDelay = 0.7f;
    public bool spawnOnVision = false;
    [Range(7.0f, 20.0f)]
    public float spawnMinDistance;
    [Range(20.0f, 100.0f)]
    public float spawnMaxDistance;
    
    private GameObject[] spawnPoints;
    private GameObject target;


    [Header("Entity Config")]
    public GameObject[] entitys;
    public GameObject[] bosses;

    private ArrayList currentSpawnedEntitys;

    [Header("Wave Config")]
    public int startMaxEntitys = 10;
    public int addPerWave = 15;
    public int bossWavesAt = 5;

    private int currentWave = 0;
    private int spawnedEntitys = 0;
    private int maxEntitys;
    private GameObject[] spawnList;

    private float spawnTimer;

    void Start () {
        // set target
        target = GameObject.FindGameObjectWithTag("Player");

        // search spawn points
        searchSpawnPoints();

        currentSpawnedEntitys = new ArrayList();

        startNewWave();
    }
	
	void Update () {
        // if there is a spawn point
            if (spawnPoints.Length != 0 && spawnTimer < Time.time) {
            // set spawn timer
            spawnTimer = spawnDelay + Time.time;

            // if not max entitiys
            if (currentSpawnedEntitys.Count < startMaxEntitys && spawnedEntitys < maxEntitys) {

                // distance to spawnpoint and best spawnpoint
                float bestDistance = float.MaxValue;
                GameObject bestSpawnPoint = null;

                // spawn entitys
                for (int i = 0; i < spawnPoints.Length; i++){

                    // check if spawnpoint is in vision
                    if (spawnOnVision) {
                        RaycastHit hit;
                        Physics.Raycast(spawnPoints[i].transform.position, spawnPoints[i].transform.TransformDirection(target.transform.position), out hit);
                        if (hit.collider == null || hit.collider != null && hit.collider.CompareTag(target.tag)) {
                            print("SPAWNPOINT [ERROR] ( spawnpoint is in vision )");
                            continue;
                        }
                    }

                    // check if spawnpoins is in distance and is best spawnpoint (clossest to target)
                    float distance = Vector3.Distance(spawnPoints[i].transform.position, target.transform.position);
                    if (distance > spawnMinDistance && distance < spawnMaxDistance && distance < bestDistance) {
                        bestDistance = distance;
                        bestSpawnPoint = spawnPoints[i];
                        continue;
                    }
                    print("SPAWNPOINT [ERROR] ( spawnpoint not in range )");
                }

                // check if best spawnpoint is not null then spawn a entity or display error
                if (bestSpawnPoint != null){
                    GameObject newEntity = Instantiate(spawnList[spawnedEntitys], bestSpawnPoint.transform.position, Quaternion.Euler(bestSpawnPoint.transform.TransformDirection(target.transform.position))) as GameObject;
                    currentSpawnedEntitys.Add(newEntity);
                    spawnedEntitys++;
                    print("spawned new entity. current entitiys: " + spawnedEntitys);
                }
                else {
                    print("no best spawn point! spawnpoint count: '" + spawnPoints.Length + "'"); // ERROR MESSAGE
                }
            }

        }else { 
            // search new spawn points
            searchSpawnPoints();
        }

        // check if a entity is dead
        for (int i = 0; i < currentSpawnedEntitys.Count; i++){
            // if entity is dead remove it
            if (((GameObject)currentSpawnedEntitys[i]).GetComponent<EnemyController>().isDead()) {
                ((GameObject)currentSpawnedEntitys[i]).GetComponent<EnemyController>().destroy();
                currentSpawnedEntitys.Remove(currentSpawnedEntitys[i]);
            }
        }

        if (spawnedEntitys >= SpawnMaxEntitys && currentSpawnedEntitys.Count <= 0) {
            startNewWave();
        }

	}

    // get spawn points
    private void searchSpawnPoints() {
        int maxchilderen = this.transform.childCount;
        spawnPoints = new GameObject[maxchilderen];
        for (int i = 0; i < maxchilderen; i++){
            spawnPoints[i] = this.transform.GetChild(i).gameObject;
        }
    }

    // start new wave
    public void startNewWave() {
        // set variables
        currentWave++;
        maxEntitys = startMaxEntitys + (addPerWave * currentWave);

        spawnedEntitys = 0;

        // generate spawn list
        spawnList = new GameObject[maxEntitys];
        for (int i = 0; i < maxEntitys; i++){

            // get random enemy
            for (int e = 0; e < entitys.Length; e++){
                if (randBool(entitys[(entitys.Length - 1) - e].GetComponent<EnemyController>().spawnChange)) {
                    spawnList[i] = entitys[(entitys.Length - 1) - e];
                    break;
                }
            }

        }
    }

    // get a random bool with a change
    public bool randBool(float change) {
        if (Random.Range(0, 100) < change) {
            return true;
        }
        return false;
    }

    // get a random int with a max
    public int randInt(int max) {
        return (int)Mathf.Floor(Random.Range(0, max));
    }
}
