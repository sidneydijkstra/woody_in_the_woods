using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public string[] names;
    public AudioClip[] clips;

    public struct Sound{
        public string name;
        public AudioClip clip;
    }

    private Sound[] sounds;
    private AudioSource audio;

	void Start () {
        audio = this.GetComponent<AudioSource>();


        sounds = new Sound[8];

        for (int i = 0; i < sounds.Length; i++){
            sounds[i].name = names[i];
            sounds[i].clip = clips[i];
        }
	}
	
	void Update () {
		
	}

    // play a audio clip
    public void playAudio(string _name) {
        for (int i = 0; i < sounds.Length; i++){
            if (sounds[i].name == _name) {
                audio.clip = sounds[i].clip;
                audio.Play();
                return;
            }
        }
    }

    // set volume
    public void setVolume(float _volume) {
        _volume = Mathf.Clamp(_volume, 0, 1);
        audio.volume = _volume;
    }

    // play random audio
    public void playRandomSound(string name1 = "", string name2 = "", string name3 = "", string name4 = "", string name5 = "", string name6 = "") {
        ArrayList names = new ArrayList();
        if (name1 != "") {
            names.Add(name1);
        }
        if (name1 != "") {
            names.Add(name1);
        }
        if (name2 != "") {
            names.Add(name2);
        }
        if (name3 != "") {
            names.Add(name3);
        }
        if (name4 != "") {
            names.Add(name4);
        }
        if (name5 != "") {
            names.Add(name5);
        }
        if (name6 != "") {
            names.Add(name6);
        }

        playAudio(((string)names[(int)Mathf.Floor(Random.Range(0, names.Count))]));
    }

}
