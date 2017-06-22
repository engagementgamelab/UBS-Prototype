
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnObject), true)]
public class TestOnInspector : Editor
{
	string[] types = new string[] 
											 { 
											 	 "enemy", 
											 	 "friend", 
									       "villager",
									       "fly",
									       "wizard"
									     };

  public override void OnInspectorGUI()
  {
  
  	SpawnObject _spawnObject = (SpawnObject)target;
    
    // Draw the default inspector
    DrawDefaultInspector();
    
    GUILayout.Label ("Type:");

    _spawnObject._spawnTypeIndex = EditorGUILayout.Popup(_spawnObject._spawnTypeIndex, types);
    
    // // Update the selected choice in the underlying object
    _spawnObject.spawnType = types[_spawnObject._spawnTypeIndex];

    // Save the changes back to the object
    EditorUtility.SetDirty(target);

  }
    
}