using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    /* TODO
     * 
     * ADD [ESC] MENU!!!! ( back, some options (sound, senc), exit)
     * 
     * remove enemy body expload
     * add boss wave
     * add boss
     * 
     * extra:
     *  add stats in menu ( also save stats )
     *  save settings
     * 
     */

    [Header("Spawn Config")]
    public int SpawnMaxEntitys = 10;
    public float spawnDelay = 0.7f;
    public bool spawnOnVision = false;
    [Range(7.0f, 20.0f)]
    public float spawnMinDistance;
    [Range(20.0f, 100.0f)]
    public float spawnMaxDistance;
    
    // spawn vars
    private GameObject[] spawnPoints;
    private GameObject target;
    private float spawnTimer;


    [Header("Entity Config")]
    public GameObject[] entitys;
    public GameObject[] bosses;

    private ArrayList currentSpawnedEntitys;

    [Header("Wave Config")]
    public float newWaveDelay = 10f;
    public int startMaxEntitys = 10;
    public int addPerWave = 15;
    public int bossWavesAt = 5;

    // wave vars
    private int currentWave = 0;
    private int spawnedEntitys = 0;
    private int maxEntitys;
    private GameObject[] spawnList;
    private float waveTimer = 0f;
    private bool isWaveTimer = false;

    private bool isRunning = false;

    // hud controller
    private HudController _hController;

    void Start () {
        // set target
        target = GameObject.FindGameObjectWithTag("Player");

        // get hud controller
        _hController = GameObject.FindGameObjectWithTag("HUD").GetComponent<HudController>();

        // search spawn points
        searchSpawnPoints();

        currentSpawnedEntitys = new ArrayList();

        // start new wave
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
                            ///print("SPAWNPOINT [ERROR] ( spawnpoint is in vision )");
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
                    ///print("SPAWNPOINT [ERROR] ( spawnpoint not in range )");
                }

                // check if best spawnpoint is not null then spawn a entity or display error
                if (bestSpawnPoint != null){
                    GameObject newEntity = Instantiate(spawnList[spawnedEntitys], bestSpawnPoint.transform.position, Quaternion.Euler(bestSpawnPoint.transform.TransformDirection(target.transform.position))) as GameObject;
                    currentSpawnedEntitys.Add(newEntity);
                    spawnedEntitys++;
                    ///print("spawned new entity. current entitiys: " + spawnedEntitys);
                }
                else {
                    ///print("no best spawn point! spawnpoint count: '" + spawnPoints.Length + "'"); // ERROR MESSAGE
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

        if (spawnedEntitys >= maxEntitys && currentSpawnedEntitys.Count <= 0 && !isWaveTimer) {
            isWaveTimer = true;
            waveTimer = newWaveDelay + Time.time;

            isRunning = false;
        }

        if (isWaveTimer) {
            _hController.displayMessage("Wave " + (currentWave + 1) + " starts in " + (Mathf.Round(waveTimer - Time.time)) + " seconds", 1f);
            if (waveTimer < Time.time) {
                isWaveTimer = false;
                startNewWave();
            }
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
        isRunning = true;


        // set variables
        currentWave++;
        maxEntitys = startMaxEntitys + (addPerWave * currentWave);

        // set current wave in playerpref
        PlayerPrefs.SetInt("_currentwave", currentWave);

        spawnedEntitys = 0;

        // spawn boss
        if (currentWave % bossWavesAt == 0) {

            int enemysToSpawn = currentWave / bossWavesAt * 5;
            int bossesToSpawn = currentWave / bossWavesAt;

            maxEntitys = enemysToSpawn;

            // display boss wave message
            _hController.displayMessage("Boss Wave " + currentWave + " started! info: maxent: " + enemysToSpawn + " maxboss: " + bossesToSpawn, 3);

            // spawn list
            spawnList = new GameObject[enemysToSpawn];
            // spawn random enemys
            for (int i = 0; i < enemysToSpawn; i++){
                // get random enemy
                for (int e = 0; e < entitys.Length; e++){
                    if (randBool(entitys[(entitys.Length - 1) - e].GetComponent<EnemyController>().spawnChange)) {
                        spawnList[i] = entitys[(entitys.Length - 1) - e];
                        break;
                    }
                }
            }

            // spawn bosses
            for (int i = 0; i < bossesToSpawn; i++){
                int index = (int)Mathf.Floor(Random.Range(0, enemysToSpawn));
                int boss = (int)Mathf.Floor(Random.Range(0, bosses.Length));
                spawnList[index] = bosses[boss];
            }

            // play sound
            GameObject.FindGameObjectWithTag("AUDIOCONTROLLER").GetComponent<AudioController>().playAudio("boss");
            return;
        }


        // display normal wave message
        _hController.displayMessage("Wave " + currentWave + " started!", 3);

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

        // play sound
        GameObject.FindGameObjectWithTag("AUDIOCONTROLLER").GetComponent<AudioController>().playAudio("wave");
    }

    // get a random bool with a change
    private bool randBool(float change) {
        if (Random.Range(0, 100) < change) {
            return true;
        }
        return false;
    }

    // get a random int with a max
    private int randInt(int max) {
        return (int)Mathf.Floor(Random.Range(0, max));
    }

    // get current wave
    public int getCurrentWave() {
        return currentWave;
    }

    // get entitys alive count
    public int getEntitysAlive() {
        return currentSpawnedEntitys.Count;
    }

    public bool running() {
        return isRunning;
    }
}
