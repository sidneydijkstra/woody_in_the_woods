using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour{

    // vars up
    private bool upWasDown;
    private bool upIsDown;

    // vars down
    private bool downWasDown;
    private bool downIsDown;

    // vars left
    private bool leftWasDown;
    private bool leftIsDown;

    // vars right
    private bool rightWasDown;
    private bool rightIsDown;

    public InputController() {
        upWasDown = false;
        downWasDown = false;
        leftWasDown = false;
        rightWasDown = false;
    }

    // update input
    public void updateInput() {
        // get input
        float y = Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");

        //Debug.Log("x: " + x + "y: " + y);

        // set all down false
        upIsDown = false;
        downIsDown = false;
        leftIsDown = false;
        rightIsDown = false;

        // check if button is up or normal again up/down
        if (y >= -0.2f && y <= 0.2f){
            upWasDown = false;
            downWasDown = false;
            //Debug.Log("no up/down");
        }

        // check if button is up or normal again left/right
        if (x >= -0.2f && x <= 0.2f) {
            leftWasDown = false;
            rightWasDown = false;
            //Debug.Log("no left/right");
        }

        // check for input up
        if (y <= -0.6 && !upWasDown) {
            upWasDown = true;
            upIsDown = true;
        }

        // check for input down
        if (y >= 0.6 && !downWasDown) {
            downWasDown = true;
            downIsDown = true;
        }

        // check for input left
        if (x <= -0.6 && !leftWasDown) {
            leftWasDown = true;
            leftIsDown = true;
        }

        // check for input right
        if (x >= 0.6 && !rightWasDown) {
            rightWasDown = true;
            rightIsDown = true;
        }
    }

    // get button down up
    public bool getButtonUp() {
        return upIsDown;
    }

    // get button down down
    public bool getButtonDown() {
        return downIsDown;
    }

    // get button down left
    public bool getButtonLeft() {
        return leftIsDown;
    }

    // get button down right
    public bool getButtonRight() {
        return rightIsDown;
    }

    // get button one
    public bool getbuttonOne() {
        return Input.GetKeyDown(KeyCode.Joystick1Button1);
    }

    // get button one
    public bool getbuttonTwo() {
        return Input.GetKeyDown(KeyCode.Joystick1Button0);
    }

}
