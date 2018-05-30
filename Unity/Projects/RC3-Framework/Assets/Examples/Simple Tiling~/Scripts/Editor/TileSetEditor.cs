
/*
 * Notes
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace RC3.Unity.SimpleTiling
{
    /// <summary>
    /// 
    /// </summary>
    [CustomEditor(typeof(TileSet))]
    public class TileSetEditor : Editor
    {
        private ReorderableList _list;


        /// <summary>
        /// 
        /// </summary>
        private void OnEnable()
        {
            _list = new ReorderableList(serializedObject, serializedObject.FindProperty("_tiles"), true, true, true, true);

            _list.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Tiles");
            _list.drawElementCallback = OnDrawElement;
        }


        /// <summary>
        /// 
        /// </summary>
        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var elem = _list.serializedProperty.GetArrayElementAtIndex(index);
            rect = new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(rect, elem, new GUIContent($"Tile {index}"));
        }


        /// <summary>
        /// 
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();
            _list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
