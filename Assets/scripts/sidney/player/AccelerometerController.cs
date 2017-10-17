using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

public class AccelerometerController : MonoBehaviour {

    SerialPort port;

    private float x = 0;
    private float y = 0;
    private float z = 0;

    private float xOffset = 0;
    private float yOffset = 0;
    private float zOffset = 0;

    private float gx = 0;
    private float gy = 0;
    private float gz = 0;

    private float lastForwardAcc = 0;
    private float lastAfalAcc = 0;
    private float lastBfalAcc = 0;

    public bool canForward = true;

    void Start () {
        port = new SerialPort("COM5", 9600);    
        port.ReadTimeout = 2000;
        port.Open();
	}
	
	void Update () {
        if (!port.IsOpen) {
            print("port [" + port.PortName + "] is not open");
            return;
        }


        string[] str = port.ReadLine().Split(","[0]);
        if (str.Length != 6) {
            print(str.Length);
            return;
        }

        float ax = float.Parse(str[0]) * 0.00025f;
        float ay = float.Parse(str[1]) * 0.00025f;
        float az = float.Parse(str[2]) * 0.00025f;

        gx = float.Parse(str[3]) * (1.0f / 32768.0f);
        gy = float.Parse(str[4]) * (1.0f / 32768.0f);
        gz = float.Parse(str[5]) * (1.0f / 32768.0f);
        
        if (Mathf.Abs(ax) - 1 < 0) ax = 0;
        if (Mathf.Abs(ay) - 1 < 0) ay = 0;
        if (Mathf.Abs(az) - 1 < 0) az = 0;

        if (Mathf.Abs(gx) < 0.025f) gx = 0f;
        if (Mathf.Abs(gy) < 0.025f) gy = 0f;
        if (Mathf.Abs(gz) < 0.025f) gz = 0f;

        x += gx;
        y += gy;
        z += gz;

        //print("a: " + new Vector3(ax, ay, az).ToString() + " g: " + new Vector3(gx, gy, gz).ToString());
    }

    // get rotation
    public Vector3 getRotation() {
        return new Vector3(x - xOffset,y - yOffset ,z - zOffset);
    }

    // check if forward acceleration
    public bool isForwardAcceleration() {
        if (lastForwardAcc - gz >= 0.6f && canForward){
            lastForwardAcc = gz;
            //print("forward");
            return true;
        }
        lastForwardAcc = gz;
        return false;
    }

    // check if backward acceleration
    public bool isForwardAndLeftAcceleration() {
        bool returning = false;
        if (lastAfalAcc - gz >= 0.6f){
            lastAfalAcc = gz;
            returning = true;
        }
        else {
            lastAfalAcc = gz;
            returning = false;
        }

        if (lastBfalAcc - gx <= -0.6f){
            lastBfalAcc = gx;
            returning = true;
        }
        else {
            lastBfalAcc = gx;
            returning = false;
        }

        if (returning) {
            //print("forwardandleft");
        }

        return returning;
    }
}
