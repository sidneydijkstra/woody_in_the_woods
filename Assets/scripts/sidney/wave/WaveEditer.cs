/*
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveController))]
public class WaveEditer : Editor {

    private Wave[] waves;
    WaveController waveController;

    void OnEnable(){
        waves = new Wave[0];
        WaveController waveController = (WaveController)target;
    }

    public override void OnInspectorGUI(){

        // display class variables
        base.OnInspectorGUI();

        // load all the old waves
        if (GUILayout.Button("Load Old Waves") && waveController.waves != null) {
            waves = waveController.waves;
        }

        // create new wave and add it to array
        if (GUILayout.Button("Create New Wave")) {
            addNewWave();
        }

        // display waves
        if (waves.Length > 0) {
            for (int i = 0; i < waves.Length; i++) {
                Wave wave = waves[i];
                // line
                EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);

                // remove this wave
                if (GUILayout.Button("Remove Wave")) {
                    removeWave(i);
                    break;
                }

                // display wave info
                GUILayout.Label("Wave: " + wave.name);
                GUILayout.Label("Max Entitys: " + wave.maxEntitys);
            }
        }

        // upload waves to wave controller
        if (GUILayout.Button("Upload All Waves")) {
            waveController.waves = waves;
        }

    }

    // add a new wave
    private void addNewWave() {
        Wave[] _waves = waves;
        waves = new Wave[waves.Length + 1];
        for (int i = 0; i < waves.Length - 1; i++) {
            waves[i] = _waves[i];
        }
        Wave w = new Wave();
        w.name = "wave-" + (waves.Length - 1);
        waves[waves.Length - 1] = w;
    }

    // remove a wave from array
    private void removeWave(int index) {
        Wave[] _waves = waves;
        waves = new Wave[waves.Length - 1];
        int min = 0;
        for (int i = 0; i < _waves.Length; i++) {
            if (i == index) {
                min = 1;
                continue;
            }
            waves[i - min] = _waves[i];
        }
    }
}
*/
