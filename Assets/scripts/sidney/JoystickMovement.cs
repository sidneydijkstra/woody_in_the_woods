using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour {

    private float speed = 6;
    private CharacterController con;

	void Start () {
        con = this.GetComponent<CharacterController>();
	}

    Vector3 dir;
    void Update () {
        print(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).ToString() + "");

        if (con.isGrounded) { 
            dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            dir *= speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton0)) {
            dir.y = 6;
        }

        dir.y -= 12 * Time.deltaTime;

        con.Move(dir);
	}
}
