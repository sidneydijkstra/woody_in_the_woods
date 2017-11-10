using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

    [Header("Menu Config")]
    public GameObject gbMenu;
    public Text txtResume;
    public Text txtOptions;
    public Text txtExit;

    // menu vars
    private Text[] selections;
    private int selected = 0;
    private bool menuIsOpen = false;

    // input handeler
    private InputController input;

	// Use this for initialization
	void Start () {
        selections = new Text[3];

        selections[0] = txtResume;
        selections[1] = txtOptions;
        selections[2] = txtExit;

        this.closeMenu();

        // get input handeler
        input = this.GetComponent<InputController>();

    }
	
	// Update is called once per frame
	void Update () {
        // update input
        //input.updateInput();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (menuIsOpen)
                closeMenu();
            else if (!menuIsOpen)
                openMenu();

        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || input.getButtonUp()) {
            selected--;
            if (selected < 0) {
                selected = selections.Length - 1;
            }
            this.changeSelected();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || input.getButtonDown()) {
            selected++;
            if (selected >= selections.Length) {
                selected = 0;
            }
            this.changeSelected();
        }


        // check for key press
        if (menuIsOpen){

            if (selected == 0 && Input.GetKeyDown(KeyCode.Space) || input.getbuttonOne() && selected == 0) {
                this.closeMenu();
            }

            if (selected == 1) {

                if (input.getButtonLeft()){
                    PlayerController _pController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                    _pController.setSensitive(-0.1f);
                    txtOptions.text = "Sensitive: " + _pController.sensitivityX;
                }

                if (input.getButtonRight()) {
                    PlayerController _pController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                    _pController.setSensitive(0.1f);
                    txtOptions.text = "Sensitive: " + _pController.sensitivityX;
                }
            }

            if (selected == 2 && Input.GetKeyDown(KeyCode.Space) || input.getbuttonOne() && selected == 2) {
                SceneManager.LoadScene(0);
            }

        }

    }

    // update the selected text
    private void changeSelected() {
        for (int i = 0; i < selections.Length; i++){
            selections[i].color = Color.white;
        }

        selections[selected].color = Color.black;

    }

    // open main menu
    private void openMenu() {
        gbMenu.SetActive(true);
        Time.timeScale = 0f;
        selected = 0;

        menuIsOpen = true;
        changeSelected();
    }

    // close main menu
    private void closeMenu() {
        gbMenu.SetActive(false);
        Time.timeScale = 1f;

        menuIsOpen = false;
    }

    // get if menu is open
    public bool isOpen() {
        return menuIsOpen;
    }
}
