using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {


    [Header("Menu Config")]
    public GameObject gbMain;
    public GameObject gbOptions;
    public GameObject gbCredits;

    [Header("Main Menu Config")]
    public Text btStart;
    public Text btOptions;
    public Text btCredits;
    public Text btExit;

    [Header("Option Menu Config")]
    // window size
    public Text btWindowSize;
    // do sound
    public Text btSound;
    public Toggle btDoSound;
    // volume slider
    public Text btVolume;
    public Slider btVolumeSlider;
    // save options
    public Text btSave;

    // selected menu vars
    private Text[] menuBt;
    private int selection;
    private int currentMenu; // 0 = main, 1 = options, 2 = credits

    // window size vars
    Resolution[] resolutions;
    int currentResolution;

    void Start () {
        // get resolution
        resolutions = Screen.resolutions;
        ArrayList list = new ArrayList();
        Resolution _last = new Resolution();
        for (int i = 0; i < resolutions.Length; i++){
            if (_last.width == resolutions[i].width && _last.height == resolutions[i].height){
                continue;
            }
            list.Add(resolutions[i]);
            _last = resolutions[i];
        }

        // add resolutions to resolution array
        resolutions = new Resolution[list.Count];
        for (int i = 0; i < list.Count; i++){
            resolutions[i] = (Resolution)list[i];
            if (Screen.currentResolution.height == resolutions[i].height && Screen.currentResolution.width == resolutions[i].width) {
                currentResolution = i;
            }
        }

        // set resolution text
        btWindowSize.text = resolutions[currentResolution].width + "x" + resolutions[currentResolution].height;

        // define menu
        menuBt = new Text[0];

        // load menu
        inMainMenu();
    }
	
	void Update () {

        // navigate up
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            selection--;
            if (selection < 0) {
                selection = menuBt.Length - 1;
            }

            changeSelected();
        }

        // navigate down
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            selection++;
            if (selection >= menuBt.Length) {
                selection = 0;
            }

            changeSelected();
        }

        bool key = Input.GetKeyDown(KeyCode.Space); // key to click
        // on main menu
        if (currentMenu == 0) {
            // start
            if (selection == 0 && key) {
                SceneManager.LoadScene(1);
            }
            // options
            if (selection == 1 && key) {
                inOptionMenu();
                return;
            }
            // credits
            if (selection == 2 && key) {
                gbCredits.SetActive(!gbCredits.activeSelf);
            }
            // exit
            if (selection == 3 && key) {

            }
        }

        // on option menu
        if (currentMenu == 1){
            // window size
            if (selection == 0){
                if (Input.GetKeyDown(KeyCode.LeftArrow)){
                    currentResolution--;
                    if (currentResolution < 0)
                        currentResolution = resolutions.Length - 1;
                    btWindowSize.text = resolutions[currentResolution].width + "x" + resolutions[currentResolution].height;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow)){
                    currentResolution++;
                    if (currentResolution > resolutions.Length - 1)
                        currentResolution = 0;
                    btWindowSize.text = resolutions[currentResolution].width + "x" + resolutions[currentResolution].height;
                    currentResolution++;
                }
            }

            // do sound
            if (selection == 1 && key) {
                btDoSound.isOn = !btDoSound.isOn;
            }
            // volume slider
            if (selection == 2) {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    btVolumeSlider.value--;
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    btVolumeSlider.value++;
            }
            // save
            if (selection == 3 && key) {

            }

            // exit menu
            if (Input.GetKeyDown(KeyCode.Escape)) {
                inMainMenu();
            }
        }


	}
    
    // set select color
    private void changeSelected() {
        for (int i = 0; i < menuBt.Length; i++){
            menuBt[i].color = Color.white;
        }
        menuBt[selection].color = Color.gray;
    }

    // set main menu
    private void inMainMenu() {
        // set last selected back
        if (menuBt.Length > 0)
            menuBt[selection].color = Color.white;

        // set selected and menu
        selection = 0;
        currentMenu = 0;

        // disable all menus
        gbOptions.SetActive(false);

        menuBt = new Text[4];
        menuBt[0] = btStart;
        menuBt[1] = btOptions;
        menuBt[2] = btCredits;
        menuBt[3] = btExit;

        changeSelected();
    }

    // set option menu
    private void inOptionMenu() {
        // set last selected back
        if (menuBt.Length > 0)
            menuBt[selection].color = Color.white;

        // set selected and menu
        selection = 0;
        currentMenu = 1;

        // disable credits menu and activate option menu
        gbOptions.SetActive(true);
        gbCredits.SetActive(false);

        menuBt = new Text[4];
        menuBt[0] = btWindowSize;
        menuBt[1] = btSound;
        menuBt[2] = btVolume;
        menuBt[3] = btSave;

        changeSelected();
    }
}
