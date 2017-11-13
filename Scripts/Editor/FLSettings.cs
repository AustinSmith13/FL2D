using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FL2D
{

    public class FLSettings : EditorWindow
    {
        [MenuItem("Window/FL2D Settings")]
        static void Init()
        {
            Globals.LoadEditorPrefs();
            EditorWindow.GetWindow<FLSettings>("FL2D Globals");
        }

        void OnGUI()
        {
            int value = 0;
            System.Collections.Generic.List<string> layerOptions = new System.Collections.Generic.List<string>();
            for (int i = 0; i < 31; i++)
            {
                string layerName = LayerMask.LayerToName(i);
                if (layerName.Length > 0)
                    layerOptions.Add(layerName);
            }

            EditorGUI.indentLevel++;
            {
                EditorGUILayout.LabelField("Default Settings");
                EditorGUI.indentLevel++;
                {
                    Globals.DEFAULT_FL_LAYER = EditorGUILayout.LayerField("FL Layer", Globals.DEFAULT_FL_LAYER);
                    Globals.DEFAULT_LIGHT_SIZE = EditorGUILayout.FloatField("Size", Globals.DEFAULT_LIGHT_SIZE);
                }
                EditorGUI.indentLevel--;

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Debuging");
                EditorGUI.indentLevel++;
                {
                    //   value += (EditorGUILayout.Toggle("Geometry", VLSDebug.IsModeActive(VLSDebugMode.Geometry))) ? 1 : 0;
                    // value += (EditorGUILayout.Toggle("Bounds", VLSDebug.IsModeActive(VLSDebugMode.Bounds))) ? 2 : 0;
                    //  value += (EditorGUILayout.Toggle("Raycasting", VLSDebug.IsModeActive(VLSDebugMode.Raycasting))) ? 4 : 0;
                }
                EditorGUI.indentLevel--;
            }
        }

    }
}