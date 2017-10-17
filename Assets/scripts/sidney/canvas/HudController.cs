using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

    [Header("Health Config")]
    public Slider healthBar;
    public Text healthText;

    private GameObject _player;
    private PlayerController _pController;

    // Use this for initialization
    void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        _pController = _player.GetComponent<PlayerController>();

    }
	
	// Update is called once per frame
	void Update () {
        healthBar.value = 0.01f * _pController.getCurrentHealth();
        healthText.text = _pController.getCurrentHealth() + "%";
    }
}
