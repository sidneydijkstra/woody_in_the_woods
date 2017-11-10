using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenuController : MonoBehaviour {

    [Header("Menu Config")]
    public GameObject opMenu;
    public Text txtTrap_1;
    public Text txtTrap_2;
    public Text txtTrap_3;
    
    public Text txtTurret_1;
    public Text txtTurret_2;
    
    public Text txtMine_1;

    public WaveController _cWave;


    // menu vars
    private bool isOpen = false;

    private Text[] menu;
    private int currentSelected;

    // input
    private InputController input;

    void Start () {
        input = this.GetComponent<InputController>();
        menu = new Text[6];

        menu[0] = txtTrap_1;
        menu[1] = txtTrap_2;
        menu[2] = txtTrap_3;

        menu[3] = txtTurret_1;
        menu[4] = txtTurret_2;

        menu[5] = txtMine_1;

        this.closeMenu();

    }
	
	// Update is called once per frame
	void Update () { 
        input.updateInput();

        // open menu
        if (input.getbuttonOne() || Input.GetKeyDown(KeyCode.M)) {
            if (!_cWave.running()) {
                if (this.isOpen) { 
                    Time.timeScale = 1;
                    this.closeMenu();
                }
                else { 
                    Time.timeScale = 0;
                    this.openMenu();
                }
            }
        }

        // if is open
        if (this.isOpen) {

            if (Input.GetKeyDown(KeyCode.UpArrow) || input.getButtonUp()) {
                currentSelected--;
                if (currentSelected < 0) {
                    currentSelected = menu.Length;
                }
                this.changeSelected();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || input.getButtonDown()) {
                currentSelected++;
                if (currentSelected > menu.Length - 1) {
                    currentSelected = 0;
                }
                this.changeSelected();
            }

            if (input.getbuttonTwo() || Input.GetKeyDown(KeyCode.Space)) {

                if (currentSelected == 0) {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BuildController>().spawnTrap(0);
                }

                if (currentSelected == 1) {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BuildController>().spawnTrap(1);
                }

                if (currentSelected == 2) {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BuildController>().spawnTrap(2);
                }

                if (currentSelected == 3) {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BuildController>().spawnTurrent(0);
                }

                if (currentSelected == 4) {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BuildController>().spawnTurrent(1);
                }

                if (currentSelected == 5) {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BuildController>().spawnMine();
                }

            }
        }

	}

    // change current menu
    private void changeSelected() {
        for (int i = 0; i < menu.Length; i++){
            menu[i].color = Color.white;
        }

        menu[currentSelected].color = Color.black;
    }

    // open menu
    public void openMenu() {
        isOpen = true;
        opMenu.SetActive(true);

        currentSelected = 0;
        this.changeSelected();
    }

    // close menu
    public void closeMenu() {
        isOpen = false;
        opMenu.SetActive(false);
    }

}
