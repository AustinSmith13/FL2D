using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FL2D
{
    [System.Serializable]
    public class Globals
    {
        [SerializeField]
        public static LayerMask DEFAULT_FL_LAYER = 0;

        [SerializeField]
        public static float     DEFAULT_LIGHT_SIZE = 8;

       // [SerializeField]
       // public static DEFAULT_AMBIENT_COLOR

        public static void SaveEditorPrefs()
        {
#if UNITY_EDITOR
            UnityEditor.EditorPrefs.SetInt("DEFAULT_FL_LAYER", DEFAULT_FL_LAYER);
            UnityEditor.EditorPrefs.SetFloat("DEFAULT_LIGHT_SIZE", DEFAULT_LIGHT_SIZE);
#endif
        }

        public static void LoadEditorPrefs()
        {
#if UNITY_EDITOR
            DEFAULT_FL_LAYER = UnityEditor.EditorPrefs.GetInt("DEFAULT_FL_LAYER", 0);
            DEFAULT_LIGHT_SIZE = UnityEditor.EditorPrefs.GetFloat("DEFAULT_LIGHT_SIZE", 8);
#endif
        }
    }
}