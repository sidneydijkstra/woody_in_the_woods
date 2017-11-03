using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Raycaster : MonoBehaviour {
	[System.Serializable]
	 public class trap
	 {
	     public GameObject[] objects;
	 }
	public trap[] traps;
	public GameObject goTerrain;
	public bool mousedown;
	private GameObject lastObj;
	public Material transMat;
	private Material objMat;
	public int currentObjHor;
	private int currentObjVert;
	public GameObject buildmenu;
	private BuildmenuScript buildmenuscript;
	// Use this for initialization
	void Start () {
		mousedown = false;
		currentObjHor = 0;
		buildmenuscript = buildmenu.GetComponent<BuildmenuScript>();
		//print("fuck");
	}
	
	// Update is called once per frame
	void Update () {
		print(currentObjVert);
	   if (Input.GetMouseButtonUp(0)) {
	   	mousedown = false;
	   }
	   if (Input.GetMouseButtonDown(0)) {
	             currentObjHor = 0;
	             currentObjVert = 0;
	             RaycastHit hit;
	             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	             if (goTerrain.GetComponent<Collider>().Raycast (ray, out hit, 100)) {
	                  lastObj = Instantiate(traps[currentObjHor].objects[currentObjVert], new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), Quaternion.identity);
	                  if (lastObj.GetComponent<Trap>() != null) {
	                  	lastObj.GetComponent<Trap>().difficulty = currentObjVert;
	                  } else if (lastObj.GetComponent<TurrentController>() != null) {
	                  	lastObj.GetComponent<TurrentController>().difficulty = currentObjVert;
	                  } else if (lastObj.GetComponent<LandMineTrap>() != null) {
	                  	lastObj.GetComponent<LandMineTrap>().difficulty = currentObjVert;
	                  }  else if (lastObj.GetComponent<DecoyScript>() != null) {
	                  	lastObj.GetComponent<DecoyScript>().difficulty = currentObjVert;
	                  }  
	                  objMat = lastObj.GetComponent<Renderer>().material;
		    lastObj.GetComponent<Renderer>().material = transMat;
	             	    mousedown = true;
	             	}
	       }

	       if (mousedown) {
	             RaycastHit hit;
	             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	             if (goTerrain.GetComponent<Collider>().Raycast (ray, out hit, 100)) {
	                 lastObj .transform.position = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
	             }
	  	 if (Input.GetKeyDown(KeyCode.RightArrow)) {
	  	 	if (currentObjHor < traps.Length - 1) {
	  			buildmenuscript.currentObjVert = 0;
	  	  		currentObjVert = buildmenuscript.currentObjVert;
	  	 		Destroy(lastObj);
	  	 		currentObjHor++;
				lastObj = Instantiate(traps[currentObjHor].objects[currentObjVert], new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), Quaternion.identity);
				objMat = lastObj.GetComponent<Renderer>().material;
				lastObj.GetComponent<Renderer>().material = transMat;
	  	 	} 
	  	 }
	  	 if (Input.GetKeyDown(KeyCode.LeftArrow)) {
	  	 	if (currentObjHor > 0) {
	  	 		buildmenuscript.currentObjVert = 0;
	  	  		currentObjVert = buildmenuscript.currentObjVert;
	  	 		Destroy(lastObj);
	  	 		currentObjHor--;
				lastObj = Instantiate(traps[currentObjHor].objects[currentObjVert], new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), Quaternion.identity);
				objMat = lastObj.GetComponent<Renderer>().material;
				lastObj.GetComponent<Renderer>().material = transMat;
	  	 	} 
	  	 }
	  	 if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) {
	  	  		currentObjVert = buildmenuscript.currentObjVert;
	  	 		Destroy(lastObj);
				lastObj = Instantiate(traps[currentObjHor].objects[currentObjVert], new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), Quaternion.identity);
				objMat = lastObj.GetComponent<Renderer>().material;
				lastObj.GetComponent<Renderer>().material = transMat;
	  	 }



	       } else if (lastObj != null) {
	       	//buildmenuscript.currentObjVert = 0;
	       	lastObj.GetComponent<Renderer>().material = objMat;
	       }
	}
}
