using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


    [Header("Camera Config")]
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumY = -60F;
    public float maximumY = 60F;

    [Header("Movement Config")]
    public float speed = 5F;
    public float jumpSpeed = 20.0F;
    public float gravity = 20.0F;

    [Header("Health Config")]
    public float maxHealth = 100F;
    public float regenAmount = 5F;
    public float regenDelay = 1F;
    public float hitDelay = 0.7F;

    // movement vars
    private Vector3 moveDirection = Vector3.zero;

    // camera vars
    private float rotationY = 0F;
    private float rotationX = 0F;

    // health vars
    private float currentHealth = 0F;
    private float regenTimer = 0F;
    private float hitTimer = 0F;

    Rigidbody rig;
    CharacterController con;
    Camera cam;

    // flip te controlles
    private bool _flip = false;

    void Start() {
        rig = GetComponent<Rigidbody>();
        con = GetComponent<CharacterController>();
        cam = Camera.main;

        // lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        // set health vars
        this.setHealth(maxHealth);
        regenTimer = Time.time + regenDelay;

        // *********** set options ***********

        // set flip
        if (PlayerPrefs.GetInt("_flip") == 1) {
            _flip = true;
        }else {
            _flip = false;
        }

        // set sens
        float newSens = PlayerPrefs.GetFloat("_sensitive");
        sensitivityX = newSens;
        sensitivityY = newSens;

        // set volume
        if (PlayerPrefs.GetInt("_dosound") == 1)
            GameObject.FindGameObjectWithTag("AUDIOCONTROLLER").GetComponent<AudioController>().setVolume(PlayerPrefs.GetFloat("_volume") / 10); // do
        else
            GameObject.FindGameObjectWithTag("AUDIOCONTROLLER").GetComponent<AudioController>().setVolume(0); // dont


        // play sound
        GameObject.FindGameObjectWithTag("AUDIOCONTROLLER").GetComponent<AudioController>().playAudio("start");
    }

    void Update() {
        playerMovement();
        cameraRotation();
        healthRegen();


        if (dead && deadTimer < Time.time)
        {
            // load dead scene and set playerpref
            PlayerPrefs.SetFloat("_timeplayed", Time.time);
            SceneManager.LoadScene(2);
        }
    }

    // move player
    private void playerMovement() {
        if (con.isGrounded) {
            /// teensy controller
            moveDirection = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));

            /// mouse & keyboard controller
            //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            //if (Input.GetButton("Jump"))
                //moveDirection.y = jumpSpeed;
        }

        // add movement
        moveDirection.y -= gravity * Time.deltaTime;
        con.Move(moveDirection * Time.deltaTime);
        
    }

    // rotate camera
    private void cameraRotation() {
        /// teensy controller
        rotationX += -Input.GetAxis("Horizontal_2") * sensitivityX;
        rotationY += -Input.GetAxis("Vertical_2") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        /// mouse & keyboard controller
		//rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        // add rotation
        transform.localEulerAngles = new Vector3(transform.rotation.y, rotationX, 0);
        cam.transform.localEulerAngles = new Vector3(-rotationY, cam.transform.rotation.x, 0);
    }

    // regen health
    private void healthRegen() {
        if (regenTimer <= Time.time && !dead) {
            regenTimer = Time.time + regenDelay;
            this.addHealth(regenAmount);
        }
    }

    // add health
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
        if (hitTimer <= Time.time && !dead) {
            // play sound
            GameObject.FindGameObjectWithTag("AUDIOCONTROLLER").GetComponent<AudioController>().playAudio("damage");

            currentHealth -= _amount;
            hitTimer = hitDelay + Time.time;
            if (currentHealth < 0) {
                currentHealth = 0;
                this.kill();
            }
        }
        
    }

    // get current health
    public float getCurrentHealth() {
        return currentHealth;
    }

    private bool dead = false;
    private float deadTimer = 0;

    // kill the player
    public void kill() {

        // play sound
        GameObject.FindGameObjectWithTag("AUDIOCONTROLLER").GetComponent<AudioController>().playAudio("dood");
        dead = true;
        deadTimer = Time.time + 3;
    }

    // set sens
    public void setSensitive(float sens) {
        this.rotationX += sens;
        this.rotationY += sens;

        print("x: " + this.rotationX + " sens: " + sens);

        if (this.rotationX < 2) {
            this.rotationX = 2;
            this.rotationY = 2;
        }

        if (this.rotationX > 6) {
            this.rotationX = 6;
            this.rotationY = 6;
        }
    }

}
