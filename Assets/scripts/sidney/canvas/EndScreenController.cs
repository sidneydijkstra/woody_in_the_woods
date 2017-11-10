using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour {

    [Header("Text Config")]
    public TextMesh txtTime;
    public TextMesh txtWave;

    void Start () {
        // set time text
        float time = Mathf.Floor(PlayerPrefs.GetFloat("_timeplayed"));
        txtTime.text = Mathf.Floor(time / 60) + " min and " + (time % 60) + " sec";

        // set wave text
        float wave = PlayerPrefs.GetInt("_currentwave");
        txtWave.text = "Wave " + wave;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(0);
        }
    }
}
