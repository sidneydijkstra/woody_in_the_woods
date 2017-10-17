using UnityEngine;
using System.Collections;
using UnityEditor;

// Creates a custom Label on the inspector for all the scripts named ScriptName
// Make sure you have a ScriptName script in your
// project, else this will not work.
[CustomEditor(typeof(WaveController))]
public class WaveControllerEditer : Editor{
    
    public GameObject gang;

    public override void OnInspectorGUI(){

        base.OnInspectorGUI();
        WaveController script = (WaveController)target;


        GUILayout.Label("WaveController Spawns Editer");
        if (GUILayout.Button("Show Spawn Points"))
        {
            Debug.Log("rutgers");
            
        }

        GameObject[] spawnPoints = script.spawnPoints;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
        }

    }

}
