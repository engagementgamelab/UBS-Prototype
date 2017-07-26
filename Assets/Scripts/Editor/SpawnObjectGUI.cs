
using NUnit.Framework.Constraints;
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
	
	string[] movementDirections = new string[]
	{
		"down",
		"up",
		"left",
		"right"
	};

  public override void OnInspectorGUI()
  {
  
  	SpawnObject _spawnObject = (SpawnObject)target;
    
    // Draw the default inspector
    DrawDefaultInspector();
    
	  GUILayout.Label ("Movement Direction:");

	  _spawnObject._direction = EditorGUILayout.Popup(_spawnObject._direction, movementDirections);
    
	  // // Update the selected choice in the underlying object
	  _spawnObject.movementDir = movementDirections[_spawnObject._direction];
    
    GUILayout.Label ("Type:");

    _spawnObject._spawnTypeIndex = EditorGUILayout.Popup(_spawnObject._spawnTypeIndex, types);
    
    // // Update the selected choice in the underlying object
    _spawnObject.spawnType = types[_spawnObject._spawnTypeIndex];

    // Save the changes back to the object
    EditorUtility.SetDirty(target);

  }
    
}