 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildmenuScript : MonoBehaviour {
	private GameObject player;
	private Raycaster playerRaycaster;
	private int currentObj;
	private int children;
	public int currentObjVert;
	int currentObjHor;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerRaycaster = player.GetComponent<Raycaster>();
		currentObjVert = 0;
		currentObjHor = 0;
		this.gameObject.transform.position = new Vector3(Screen.width/2, Screen.height - Screen.height/10, 0 );
	}
	// Update is called once per frame
	void Update () {
		if (playerRaycaster.mousedown) {
			if (currentObjHor != playerRaycaster.currentObjHor) {
				currentObjHor = playerRaycaster.currentObjHor;
				currentObjVert = 0;
			}
			showImages(true);
			transform.GetChild(children - currentObjHor - 1).GetComponent<RectTransform>().sizeDelta = new Vector2 (Screen.width/7.5f, Screen.width/7.5f);
			int children2 = transform.GetChild(children - currentObjHor - 1).childCount;
		 		for (int j = 0; j < children2; ++j) {
		 			transform.GetChild(children - currentObjHor - 1).transform.GetChild(j).GetComponent<Image>().enabled = true;
		 		}
			 if (Input.GetKeyDown(KeyCode.DownArrow)) {
				if (currentObjVert < children2 - 1 ) {
					currentObjVert++;
				}
			}
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				if (currentObjVert > 0) {
					currentObjVert--;
				}
			}

            // horizontal
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (currentObjHor > 0) {
                    currentObjHor--;
				}
			}
			if (Input.GetKeyDown(KeyCode.RightArrow)) {
				if (currentObjHor < 3) {
                    currentObjHor++;
				}
			}
		transform.GetChild(children - currentObjHor - 1).transform.GetChild(currentObjVert).GetComponent<RectTransform>().sizeDelta = new Vector2 (Screen.width/7.5f, Screen.width/7.5f);
		} else {
			showImages(false);
		}
		print(this.gameObject.transform.position);
		if (currentObjHor == 0) {
			if (this.gameObject.transform.position.x < Screen.width/2 + Screen.width/10)
				this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x  + 5, this.gameObject.transform.position.y, 0 );
		} else if (currentObjHor == 1) {
			if (this.gameObject.transform.position.x > Screen.width/2)
				this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x  - 5, this.gameObject.transform.position.y, 0 );
			else if (this.gameObject.transform.position.x < Screen.width/2)
				this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x  + 5, this.gameObject.transform.position.y, 0 );
		} else {
			if (this.gameObject.transform.position.x > Screen.width/2 - Screen.width/10)
				this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x  - 5, this.gameObject.transform.position.y, 0 );
		}
	}
	void showImages(bool enable) {
		children = transform.childCount;
		for (int i = 0; i < children; ++i) {
		 	transform.GetChild(i).GetComponent<Image>().enabled = enable;
		 	transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2 (Screen.width/7.5f, Screen.width/7.5f);
		 	int children2 = transform.GetChild(i).childCount;
		 		for (int j = 0; j < children2; ++j) {
		 			transform.GetChild(i).transform.GetChild(j).GetComponent<Image>().enabled = false;
		 			transform.GetChild(i).transform.GetChild(j).GetComponent<RectTransform>().sizeDelta = new Vector2 (Screen.width/10, Screen.width/10);
		 		}
		 }
	}

}
