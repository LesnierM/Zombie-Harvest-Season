using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(actionableObjects))]
public class doorsEditorController : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        actionableObjects _object = (actionableObjects)target;
        if (GUILayout.Button("Abrir"))
        {
            _object.startAction();
        }
        if (GUILayout.Button("Cerrar"))
        {
            _object.finishAction();
        }
    }
}
