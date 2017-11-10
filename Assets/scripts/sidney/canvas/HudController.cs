using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

    public WaveController _wController;

    [Header("Health Config")]
    public Slider healthBar;
    public Text healthText;

    [Header("GameInfo Config")]
    public Text txtWave;
    public Text txtAlive;
    public Text txtTimePlayed;
    public Text txtWood;

    [Header("GameMessage Config")]
    public Text txtMessage;

    // game message vars
    private float messageTimer = 0;
    private bool messageOverwrite = false;

    // player and playerController
    private GameObject _player;
    private PlayerController _pController;
    private PlayerAxeController _pAxeController;

    // Use this for initialization
    void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        _pController = _player.GetComponent<PlayerController>();
        _pAxeController = _player.GetComponent<PlayerAxeController>();
    }
	
	// Update is called once per frame
	void Update () {
        updateHealthBar();
        updateGameInfo();

        updateGameMessage();
    }

    // update health bar
    private void updateHealthBar() {
        healthBar.value = 0.01f * _pController.getCurrentHealth();
        healthText.text = _pController.getCurrentHealth() + "%";
    }

    // update game info
    private void updateGameInfo() {
        // update wood amount
        txtWood.text = "Wood " + _pAxeController.getCurrentResources();

        // update time played
        float time = Mathf.Floor(Time.time);
        txtTimePlayed.text = "Time Played " + Mathf.Floor(time / 60) + ":" + (time % 60);

        // update wave
        txtWave.text = "Wave " + _wController.getCurrentWave();

        // update alive entitys
        txtAlive.text = "Alive " + _wController.getEntitysAlive();
    }

    // update game message
    private void updateGameMessage() {
        if (messageTimer <= Time.time) {
            messageOverwrite = false;
            txtMessage.text = "";
        }
    }

    // set game message
    public void displayMessage(string message, float displayTime, bool canOverwrite = false) {
        if (!messageOverwrite) {
            messageOverwrite = canOverwrite;
            txtMessage.text = message;
            messageTimer = Time.time + displayTime;
        }else{
            print("Cant display message: '" + message + "' [messageOverwrite == TRUE]");
        }
    }

}
