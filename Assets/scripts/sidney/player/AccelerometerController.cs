using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System;


public class AccelerometerController : MonoBehaviour {

    SerialPort port;
    string[] openPorts;

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
        // load ports to search
        int maxPorts = 10; // number of ports search
        string[] ports = new string[10];
        for (int i = 0; i < maxPorts; i++) {
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
                print("opend port: '" + testport.PortName + "'" + " name: " + testport.ToString());
                _openPorts.Add(testport.PortName); // add open ports to array
                testport.Close();
            }catch (System.Exception e){
                // on error
                print("could not open port: '" + testport.PortName + "'" + " error: " + e);
                continue;
            }
        }

        // add new found open ports to public open ports array
        openPorts =  new string[_openPorts.Count];
        for (int i = 0; i < _openPorts.Count; i++){
            openPorts[i] = (string)_openPorts[i];
        }

        port = new SerialPort(openPorts[0], 9600);
        port.Open();


    }
	
	void Update () {
        if (!port.IsOpen) {
            //print("port [" + port.PortName + "] is not open");
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
