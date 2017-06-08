/*
  This class draws the handles for the diferent nodes the particles are going to follow.
  created by:
  Juan Sebastian Munoz Arango
  2015
 */
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Follower))]
public class FollowerCustomEditor : Editor {


    private Follower myTarget = null;
    private const int offset = 50;//offset when creating a new node.

    void OnEnable() {
        myTarget = (Follower) target;
        if(myTarget.nodes == null) {
            myTarget.nodes = new List<Vector3>();
            myTarget.nodes.Add(myTarget.transform.position + Random.insideUnitSphere*offset);
        }
    }

    void OnSceneGUI() {
        if(myTarget != null) {
            for(int i = 0; i < myTarget.nodes.Count; i++) {
                myTarget.nodes[i] = Handles.PositionHandle(myTarget.nodes[i],
                                                           Quaternion.identity);
            }
        }
    }

    public override void OnInspectorGUI() {
        for(int i = 0; i < myTarget.nodes.Count; i++) {
            GUILayout.BeginHorizontal();
            myTarget.nodes[i] = EditorGUILayout.Vector3Field("", myTarget.nodes[i]);
            if(GUILayout.Button("X")) {
                if(myTarget.nodes.Count > 1)
                    myTarget.nodes.RemoveAt(i);
            }
            GUILayout.EndHorizontal();
        }
        if(GUILayout.Button("Add")) {
            Vector3 newPos = myTarget.nodes[myTarget.nodes.Count-1] +
                Random.insideUnitSphere*offset;
            myTarget.nodes.Add(newPos);
        }
    }
}