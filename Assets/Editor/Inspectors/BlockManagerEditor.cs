using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UserCode
{
    [CustomEditor(typeof(BlockManager))]
    public class BlockManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BlockManager blockManager = (BlockManager)target;

            if (GUILayout.Button("Generate Row"))
            {
                blockManager.GenerateRow(1);
            }

            if (GUILayout.Button("Generate Theme Test Row"))
            {
                blockManager.GenerateThemeRow();
            }
        }
    }
}