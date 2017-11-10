using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO.Ports;

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
    // sensitive
    public Text btSensitive;

    [Header("Menu Message Config")]
    public GameObject gbMessage;
    public Text txtMessage;

    // message vars
    private bool _messageCanOverwrite = false;
    private float _messageTimer = 0f;
    private bool _messageDisplay = false;

    // selected menu vars
    private Text[] menuBt;
    private int selection;
    private int currentMenu; // 0 = main, 1 = options, 2 = credits

    // window size vars
    Resolution[] resolutions;
    int currentResolution;

    // sensitive vars
    private float currentSensitive = 4.0f;

    // ############## tutorial vars ##############
    private float _tutorialTimer = 0f;
    private bool _tutorialDone = false;

    void Start() {
        // get resolution
        resolutions = Screen.resolutions;
        ArrayList list = new ArrayList();
        Resolution _last = new Resolution();
        for (int i = 0; i < resolutions.Length; i++) {
            if (_last.width == resolutions[i].width && _last.height == resolutions[i].height) {
                continue;
            }
            list.Add(resolutions[i]);
            _last = resolutions[i];
        }

        // add resolutions to resolution array
        resolutions = new Resolution[list.Count];
        for (int i = 0; i < list.Count; i++) {
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

    void Update() {
        // update display message
        updateMessage();
        // connect to controller
        conectToController();
        if (!_conectionDone)
            return;

        // reconect to controller
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Joystick1Button2)){ 
            _conectionDone = false;
            return;
        }

        // get controller input
        //getControllerInput();

        // tutorial display message on timer
        if (_tutorialTimer < Time.time && !_tutorialDone) {
            _tutorialDone = true;
            this.displayMessage("Use 'Arrow Keys' and 'SPACE' to navigate in the menu.", 4f);
        }

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
        //bool key = _controllerClickTwo; // key to click
        // on main menu
        if (currentMenu == 0) {
            // start
            if (selection == 0 && key) {
                // save settings

                // set controlles
                PlayerPrefs.SetFloat("_sensitive", currentSensitive);

                // set sound
                int doSound = 0;
                if (btDoSound.isOn)
                    doSound = 1;
                PlayerPrefs.SetInt("_dosound", doSound);
                PlayerPrefs.SetFloat("_volume", btVolumeSlider.value);

                // load game
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
        if (currentMenu == 1) {
            // window size
            if (selection == 0) {
                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    currentResolution--;
                    if (currentResolution < 0)
                        currentResolution = resolutions.Length - 1;
                    btWindowSize.text = resolutions[currentResolution].width + "x" + resolutions[currentResolution].height;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow)) {
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
            // sensitive
            if (selection == 3) {
                if (Input.GetKeyDown(KeyCode.LeftArrow)) { 
                    currentSensitive -= 0.1f;
                    if (currentSensitive < 2)
                        currentSensitive = 2;

                    btSensitive.text = "Sensitive: " + System.Math.Round(currentSensitive, 2);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow)) { 
                    currentSensitive += 0.1f;
                    if (currentSensitive > 6)
                        currentSensitive = 6;

                    btSensitive.text = "Sensitive: " + System.Math.Round(currentSensitive, 2);
                }
            }

            // exit menu
            if (Input.GetKeyDown(KeyCode.Escape)) {
                // set new res
                if (Screen.width != resolutions[currentResolution].width || Screen.height != resolutions[currentResolution].height) {
                    Screen.SetResolution(resolutions[currentResolution].width, resolutions[currentResolution].height, true);
                }

                inMainMenu();
            }
        }


    }

    // set select color
    private void changeSelected() {
        for (int i = 0; i < menuBt.Length; i++) {
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
        menuBt[3] = btSensitive;

        changeSelected();
    }

    // message board functions
    private void updateMessage() {
        if (_messageDisplay && _messageTimer < Time.time) {
            _messageCanOverwrite = false;
            _messageDisplay = false;
            gbMessage.SetActive(false);
        }
    }

    private void displayMessage(string message, float displayTime, bool canOverwrite = false) {
        if (!_messageCanOverwrite) {
            gbMessage.SetActive(true);
            txtMessage.text = message;

            _messageTimer = Time.time + displayTime;
            _messageCanOverwrite = canOverwrite;
            _messageDisplay = true;
        }
    }

    // conection vars
    private float _conectionTimer = 0f;
    private bool _conectionFound = false;
    private bool _conectionDone = false;

    private string _conectionPortName;

    // controller conection functions
    private void conectToController() {
        if (_conectionDone) {
            return;
        }

        // check open ports
        if (_conectionTimer < Time.time) {
            _conectionTimer = Time.time + 1f;

            string[] openPorts = this.getOpenPorts();

            if (openPorts.Length <= 0) { // on no open ports
                this.displayMessage("There are no open ports! Pleas connect the controller.", 1f);
            } else { // on open ports
                _conectionFound = true;
                this.displayMessage("Current open ports: " + openPorts.Length + "!", 10f);
                _conectionTimer = Time.time + 10f;
            }
        }
        // look for port with controller conected
        else if (_conectionFound) {
            string[] openPorts = this.getOpenPorts();
            for (int i = 0; i < openPorts.Length; i++){
                // create and open port
                SerialPort port = new SerialPort(openPorts[i], 9600);
                port.Open();

                // check if port is teensy controller
                string[] str = port.ReadLine().Split(","[0]);
                if (str.Length == 6) { // found port
                    this.displayMessage("Controller found! Port name: " + openPorts[i] + ".", 4f);
                    _conectionPortName = openPorts[i];
                    _conectionDone = true;

                    // tutorial set timer
                    _tutorialTimer = Time.time + 5f;
                    return;
                }

                // close port
                port.Close();
            }
        }
        
    }

    private string[] getOpenPorts(int searchSize = 10) {
        // load ports to search
        string[] ports = new string[searchSize];
        for (int i = 0; i < searchSize; i++) {
            ports[i] = "COM" + i;
        }

        // array of open ports
        ArrayList _openPorts = new ArrayList();

        // search ports
        for (int i = 0; i < ports.Length; i++){
            SerialPort testport = new SerialPort(ports[i], 9600);
            try{
                // on port open
                testport.ReadTimeout = 2000;
                testport.Open();
                print("opend port: '" + testport.PortName + "'");
                _openPorts.Add(testport.PortName); // add open ports to array
                testport.Close();
            }catch (System.Exception e){
                // on error
                //print("could not open port: '" + testport.PortName + "'" + " error: " + e);
                continue;
            }
        }

        // add new found open ports to public open ports array
        string[] openPorts =  new string[_openPorts.Count];
        for (int i = 0; i < _openPorts.Count; i++){
            openPorts[i] = (string)_openPorts[i];
        }

        // return open ports
        return openPorts;
    }
}
